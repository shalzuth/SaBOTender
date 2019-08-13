using FFXIV.Packets;
using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Reflection;
using System.Threading;

namespace FFXIV.Network
{
    public abstract class NetworkClient
    {
        public Boolean Debug = false;
        public String SessionId;
        public Blowfish Key;
        public UInt32 EntityId;
        public TcpClient TcpClient = new TcpClient();
        public Stream Stream;
        public Thread PacketHandlerThread;
        public virtual Int32 Port { get; set; }
        public Boolean HandlePackets = true;
        public void Connect(IPAddress server, Int32 port = 0)
        {
            RegisterPackets<UInt32>(typeof(PacketId), PacketHandler);
            RegisterPackets<UInt16>(PacketHeaderEnum, OpcodeHandler);
            Port = port;
            if (Port == 0)
                throw new Exception("");
            TcpClient.Connect(server, Port);
            Stream = TcpClient.GetStream();
            InitSequence();
            PacketHandlerThread = new Thread(() =>
            {
                while (HandlePackets)
                {
                    HandlePacket();
                    if (!TcpClient.Connected)
                        break;
                }
            });
            PacketHandlerThread.IsBackground = true;
            PacketHandlerThread.Name = "Packet Handler : " + ToString();
            PacketHandlerThread.Start();
        }
        public abstract void InitSequence();
        public Dictionary<UInt32, MethodInfo> PacketHandler = new Dictionary<UInt32, MethodInfo>();
        public Dictionary<UInt32, MethodInfo> OpcodeHandler = new Dictionary<UInt32, MethodInfo>();
        public Type PacketHeaderEnum;
        public void RegisterPackets<T>(Type packetEnum, Dictionary<UInt32, MethodInfo> handler)
        {
            var names = Enum.GetNames(packetEnum);
            List<UInt32> values;
            if (((UInt32)0) is T)
                values = Enum.GetValues(packetEnum).Cast<UInt32>().ToList();
            else
                values = Enum.GetValues(packetEnum).Cast<UInt16>().ToList().Select(v => (UInt32)v).ToList();
            var maxValue = values.Max();
            var undefined = GetType().GetMethod("Undefined", BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.FlattenHierarchy);
            for (var i = 0u; i <= maxValue; i++)
            {
                var index = values.IndexOf(i);
                if (index > -1)
                {
                    var method = GetType().GetMethod(names[index], BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.FlattenHierarchy);
                    if (method != null)
                        handler.Add(i, method);
                    else
                        handler.Add(i, undefined);
                }
            }
        }
        public Int32 PacketHeaderIndex = 0;
        public Int32 Encrypted = 0;
        public Boolean Logging = false;

        Object Read(Byte size)
        {
            var bytes = new Byte[size];
            Stream.Read(bytes, 0, size);
            if (size == 8)
                return BitConverter.ToUInt64(bytes, 0);
            else if (size == 4)
                return BitConverter.ToUInt32(bytes, 0);
            else if (size == 2)
                return BitConverter.ToUInt16(bytes, 0);
            return BitConverter.ToString(bytes, 0);
        }
        public Byte[] ReadPacket(Boolean hack = false)
        {
            var magicLen = (Byte)16;
            if (hack)
                magicLen = 15;
            var magic = Read(magicLen);
            var timeStamp = Read(8);
            var length = (UInt32)Read(4);
            var connectionType = Read(2);
            var messageCount = (UInt16)Read(2);
            var expectsReply = Stream.ReadByte();
            var compressed = Stream.ReadByte() == 1 ? true : false;
            var unk2 = Read(6);
            if (length == 0)
            {
                int i = 0;
                var q = Stream.ReadByte();
                while (q == 0 || q == -1)
                {
                    if (i++ == 1000)
                        throw new Exception("bad data");
                    q = Stream.ReadByte();
                }
                Console.WriteLine("fixed data : " + i + " :" + q);
                return ReadPacket(true);
            }
            if (length > 10000)
                throw new Exception();
            var packetBytesList = new List<Byte>();
            for (int i = 0; i < length - 40; i++)
                packetBytesList.Add((Byte)Stream.ReadByte());

            if (compressed)
            {
                using (var compressedStream = new MemoryStream(packetBytesList.Skip(2).ToArray()))
                {
                    using (var deflateStream = new DeflateStream(compressedStream, CompressionMode.Decompress))
                    {
                        using (var outputStream = new MemoryStream())
                        {
                            deflateStream.CopyTo(outputStream);
                            packetBytesList = outputStream.ToArray().ToList();
                        }
                    }
                }
            }
            return packetBytesList.ToArray();
        }
        public void HandlePacket(Boolean hack = false)
        {
            //try
            {
                var packetBytes = ReadPacket();
                var offset = 0;
                while (offset < packetBytes.Length - 3)
                //for (int i = 0; i < messageCount; i++)
                {
                    var messageLength = BitConverter.ToInt32(packetBytes, offset);
                    var source = BitConverter.ToUInt32(packetBytes, offset + 4);
                    var dest = BitConverter.ToUInt32(packetBytes, offset + 8);
                    var packetType = (PacketId)BitConverter.ToUInt32(packetBytes, offset + 12);
                    //var messageBytes = packetBytes.Skip(offset + 12).Take(messageLength - 12).ToArray();
                    var messageBytes = packetBytes.Skip(offset).Take(messageLength).ToArray();
                    var origMessageBytes = messageBytes.ToList().ToArray();
                    var origpacketType = packetType;
                    var packetHandler = PacketHandler;
                    if ((packetType == PacketId.Opcode || packetType == PacketId.LobbyS2CAuth))
                    {
                        if (GetType() == typeof(LobbyClient))
                            Key.Decipher(messageBytes, 4 + 12, messageBytes.Length - 8 - 16);
                        if (packetType == PacketId.Opcode)
                            packetHandler = OpcodeHandler;
                        if (packetType != PacketId.LobbyS2CAuth)
                        {
                            messageBytes = messageBytes.Skip(4 + 12).ToArray();
                            packetType = (PacketId)BitConverter.ToUInt32(messageBytes, 2);
                        }
                    }
                    var data = BitConverter.ToString(messageBytes);
                    if (!packetHandler.ContainsKey((UInt32)packetType))
                    {
                        Console.WriteLine("unk packet : " + GetType().Name + " : " + packetType.ToString("X") + " : " + BitConverter.ToString(messageBytes));
                    }
                    else
                    {
                        var method = packetHandler[(UInt32)packetType];
                        if (method != null)
                        {
                            var packetName = PacketHeaderEnum.AssemblyQualifiedName.Replace(PacketHeaderEnum.Name, method.Name);
                            if (Debug)
                                Console.WriteLine("s " + GetType().Name + " : " + Type.GetType(packetName).Name);
                            Packet packet = (Packet)Activator.CreateInstance(Type.GetType(packetName));
                            packet.Bytes = messageBytes;
                            packet.Source = source;
                            if (waitingForPacket != null && waitingForPacket(packet))
                                waitingForPacket = null;

                            //var t = new Thread(() =>
                            {
                                method.Invoke(this, new object[] { packet });
                            }
                            //);
                            //t.IsBackground = false;
                            //t.Start();
                        }
                        else
                        {
                            Console.WriteLine("no method s " + GetType().Name + " : " + (WorldOpcode)packetType + " : " + packetType.ToString("X") + " : " + BitConverter.ToString(messageBytes));
                        }
                    }
                    offset += messageLength;
                }
            }
            //catch (Exception e)
            {
                //     Console.WriteLine(e.Message);
            }
        }
        public Random Random = new Random();
        public delegate Boolean WaitingForPacketDelegate(Packet data);
        public WaitingForPacketDelegate waitingForPacket;
        public void WaitForPacket(WaitingForPacketDelegate waitUntilReceivedPacket)
        {
            waitingForPacket = waitUntilReceivedPacket;
            while (waitingForPacket != null) Thread.Sleep(50);
        }
        public void SendPacket(Packet packet, UInt16 minSleep, UInt16 maxSleep)
        {
            SendPacket(packet);
            Thread.Sleep(Random.Next(minSleep, maxSleep));
        }
        public Boolean SendPacket(Packet packet, WaitingForPacketDelegate waitUntilReceivedPacket)
        {
            waitingForPacket = waitUntilReceivedPacket;
            SendPacket(packet);
            var i = 0;
            while (waitingForPacket != null)
            {
                Thread.Sleep(50);
                if (i++ > 100) return false;
            }
            return true;
        }
        public Boolean IsEncrypted = false;

        public void SendPacket(Packet packet, Byte ConnectionType = 0)
        {
            SendPackets(new List<Packet> { packet }, ConnectionType);
        }

        public Byte[] BuildPacket(List<Packet> packets, Byte ConnectionType = 0)
        {
            var rawPacket = new RawPacket();
            rawPacket.Magic = packets.First() is OpcodePacket ? Utils.StringToByteArray("5252a041ff5d46e27f2a644d7b99c475").ToList() : new List<Byte>(new Byte[16]);
            rawPacket.TimeStamp = packets.First() is OpcodePacket ? (UInt64)Utils.GetUnixTime() * 1000 : 0;
            rawPacket.ConnectionType = ConnectionType;
            rawPacket.NumSubPackets = (UInt16)packets.Count;
            rawPacket.ExpectsReply = (Byte)(packets.First() is Pong ? 0 : 1);
            rawPacket.Compressed = 0;
            foreach (var packet in packets)
            {
                if (packet is OpcodePacket) ((OpcodePacket)packet).OpcodeType = (UInt16)Enum.Parse(PacketHeaderEnum, packet.GetType().Name);
                var sub = packet is OpcodePacket ? new Opcode { Data = (OpcodePacket)packet } : (SubPacket)packet;

                sub.Length = (UInt32)sub.ToArray().Length;
                if (IsEncrypted) sub.Key = Key;
                sub.EntityIdSource = sub.EntityIdTarget = packets.First() is OpcodePacket ? EntityId : 0;
                rawPacket.SubPackets.Add(sub);
            }
            var rawData = rawPacket.ToArray();
            rawPacket.Length = (UInt32)rawData.Length;
            rawData = rawPacket.ToArray();
            return rawData;
        }
        public void SendPackets(List<Packet> packets, Byte ConnectionType = 0)
        {
            if (!TcpClient.Connected) return;
            var data = BuildPacket(packets, ConnectionType);
            Stream.Write(data, 0, data.Length);
        }
        void Undefined(List<Byte> data)
        {
            //this.Log("UNKNOWN GAME PACKET : " + Enum.Parse(typeof(GameServerPacket), data[0].ToString()) + " | " + BitConverter.ToString(data.ToArray()));
        }
        public void Log(String msg)
        {

        }
    }
}
