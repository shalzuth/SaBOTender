using FFXIV.Packets;
using System;
using System.Collections.Generic;
using System.Net;
using System.Threading;

namespace FFXIV.Network
{
    public class ChatClient : NetworkClient
    {
        public String Password;
        public UInt64 ContentId;
        public String EntityIdString;
        public ChatClient(String ipAddr, UInt32 port, UInt32 entityId, UInt64 contentId, String password, Blowfish key)
        {
            //Debug = true;
            EntityIdString = entityId.ToString();
            ContentId = contentId;
            Password = password;
            Key = key;
            PacketHeaderEnum = typeof(ChatOpcode);
            Connect(IPAddress.Parse(ipAddr), (Int32)port);
        }

        public override void InitSequence()
        {
            var connect = new WorldC2SConnect { EntityId = EntityIdString, Zeros2 = new List<Byte>(new Byte[36 - EntityIdString.Length]) };
            SendPacket(connect, 2);
        }
        void Ping(Ping ping)
        {
            Console.WriteLine(ping.Time);
            var pongThread = new Thread(() =>
            {
                while (EntityId == 0) Thread.Sleep(500);
                SendPacket(new Pong { EntityId = EntityId });
            });
            pongThread.IsBackground = false;
            pongThread.Start();
        }
        void ChatS2CAuth(ChatS2CAuth auth)
        {
        }
        /*
        void Ping(Ping ping)
        {
            Console.WriteLine(ping.Time);
            if (EntityId > 0)
                SendPacket(new Pong { EntityId = EntityId });
        }
        void ServerAuth(ServerAuth auth)
        {
            EntityId = auth.EntityId;
            var encryptedZero = BitConverter.GetBytes(((UInt64)0));
            Key.Decipher(encryptedZero, 0, 8);
            SendPacket(new ClientAuth { SessionId = SessionId, ReverseEncryption = BitConverter.ToUInt64(encryptedZero, 0) });
            SendPacket(new Pong { EntityId = EntityId });
        }
        void ServerAccountList(ServerAccountList list)
        {
            var serviceId = BitConverter.GetBytes(((UInt64)list.ServiceId));
            Key.Decipher(serviceId, 0, 8);
            SendPacket(new ClientCharList { ServiceId = BitConverter.ToUInt64(serviceId, 0) });
        }
        List<ServerWorldList.WorldInfo> Worlds = new List<ServerWorldList.WorldInfo>();
        void ServerWorldList(ServerWorldList list)
        {
            Worlds.AddRange(list.Worlds);
        }
        void ServerWorldOpen(ServerWorldOpen list)
        {
        }
        void ServerCharList(ServerCharList list)
        {
            var id = list.ContentId;
            var encryptedZero = BitConverter.GetBytes(((UInt64)0));
            Key.Decipher(encryptedZero, 0, 8);
            SendPacket(new ClientCharConnect { Session = id, ReverseEncryption = BitConverter.ToUInt64(encryptedZero, 0), ReverseEncryption2 = BitConverter.ToUInt64(encryptedZero, 0) });
        }
        void ServerWorldEnter(ServerWorldEnter enter)
        {

            TcpClient.Close();
        }*/
    }
}
