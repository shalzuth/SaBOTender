using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using FFXIV.Packets;

namespace FFXIVSniffer
{
    public class DummyClient : FFXIV.Network.NetworkClient
    {
        public Dictionary<UInt32, MethodInfo> WorldOpcodes = new Dictionary<UInt32, MethodInfo>();
        public Dictionary<UInt32, MethodInfo> ChatOcodes = new Dictionary<UInt32, MethodInfo>();
        public Dictionary<UInt32, MethodInfo> LobbyOpcodes = new Dictionary<UInt32, MethodInfo>();
        public override void InitSequence()
        {
            RegisterPackets<UInt32>(typeof(PacketId), PacketHandler);
            RegisterPackets<UInt16>(typeof(WorldOpcode), WorldOpcodes);
            RegisterPackets<UInt16>(typeof(ChatOpcode), ChatOcodes);
            RegisterPackets<UInt16>(typeof(LobbyOpcode), LobbyOpcodes);
        }
        public List<Packet> Test(PacketWrapper packetWrapper)
        {
            var packetBytes = ReadPacket();

            var packets = new List<Packet>();
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
                var packetEnum = typeof(PacketId);
                /*try
                {
                    var packetName = typeof(PacketId).AssemblyQualifiedName.Replace(typeof(PacketId).Name, ((PacketId)packetType).ToString());
                    Packet packet = (Packet)Activator.CreateInstance(Type.GetType(packetName));
                    packet.Bytes = messageBytes;
                    packets.Add(packet);
                }
                catch
                {
                    packets.Add(new Packet { Bytes = packetBytes.Skip(offset).Take(messageLength).ToArray() });
                }*/
                if ((packetType == PacketId.Opcode || packetType == PacketId.LobbyS2CAuth))
                {
                    if (packetWrapper.Channel == "Lobby")
                        Key.Decipher(messageBytes, 4 + 12, messageBytes.Length - 8 - 16);
                    if (packetType == PacketId.Opcode)
                        packetEnum = typeof(WorldOpcode);
                    if (packetType != PacketId.LobbyS2CAuth)
                    {
                        messageBytes = messageBytes.Skip(4 + 12).ToArray();
                        packetType = (PacketId)BitConverter.ToUInt32(messageBytes, 2);
                    }
                }
                var data = BitConverter.ToString(messageBytes);
                //Packet packet = new Packet();
                //Console.WriteLine("s " + GetType().Name + " : " + Type.GetType(packetName).Name);
                try
                {
                    var packetName = packetType.ToString();
                    if (packetEnum == typeof(WorldOpcode))
                        packetName = ((WorldOpcode)packetType).ToString();
                    else
                        Console.WriteLine("rawr");
                    packetName = packetEnum.AssemblyQualifiedName.Replace(packetEnum.Name, packetName);
                    Packet packet = (Packet)Activator.CreateInstance(Type.GetType(packetName));
                    packet.Bytes = messageBytes;
                    packets.Add(packet);
                }catch{
                    packets.Add(new Packet { Bytes = packetBytes.Skip(offset).Take(messageLength).ToArray() });
                }
                offset += messageLength;
            }
            return packets;
        }
    }
}
