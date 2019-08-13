using FFXIV.Packets;
using System;
using System.Collections.Generic;
using System.Net;
using System.Threading;

namespace FFXIV.Network
{
    public class LobbyClient : NetworkClient
    {
        public enum Datacenter
        {
            JPElemental = 1,
            NAAether,
            JPGaia,
            NAPrimal,
            JPMana,
            EUChaos,
            EULight,
            NACrystal
        }
        public Byte MaxExpansion = 0;
        public UInt64 EncryptedZero = 0;
        public static String GetDatacenterUrl(Datacenter dc)
        {
            return "neolobby0" + (Byte)dc + ".ffxiv.com";
        }
        public LobbyClient(Datacenter dc, String sessionId, UInt32 xPack)
        {
            IsEncrypted = true;
            MaxExpansion = (Byte)xPack;
            SessionId = sessionId;
            PacketHeaderEnum = typeof(LobbyOpcode);
            var server = Dns.GetHostEntry(GetDatacenterUrl(Datacenter.NACrystal));
            Connect(server.AddressList[0], 54994);
        }

        public override void InitSequence()
        {
            var connect = new LobbyC2SConnect();
            SendPacket(connect, 3);
            Key = new Blowfish(new NetworkKey { Time = connect.Time, Seed = connect.Seed }.ToArray());

            var encryptedZero = BitConverter.GetBytes(((UInt64)0));
            Key.Decipher(encryptedZero, 0, 8);
            EncryptedZero = BitConverter.ToUInt64(encryptedZero, 0);
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
        void Pong(Pong ping)
        {
            Console.WriteLine(ping.Time);
        }
        void LobbyS2CAuth(LobbyS2CAuth auth)
        {
            EntityId = auth.EntityId;
            SendPacket(new LobbyC2SAuth { SessionId = SessionId, ReverseEncryption = EncryptedZero });
            while (false && TcpClient.Connected)
            {
                SendPacket(new Ping { EntityId = EntityId });
                Thread.Sleep(5000);
            }
            var pingThread = new Thread(() =>
            {
                while (false && TcpClient.Connected)
                {
                    SendPacket(new Ping { EntityId = EntityId });
                    Thread.Sleep(5000);
                }
            });
            pingThread.IsBackground = false;
            pingThread.Start();
        }
        void LobbyS2CAccountList(LobbyS2CAccountList list)
        {
            var serviceId = BitConverter.GetBytes(((UInt64)list.ServiceId));
            Key.Decipher(serviceId, 0, 8);
            SendPacket(new LobbyC2SCharList { ServiceId = BitConverter.ToUInt64(serviceId, 0) });
        }
        List<LobbyS2CWorldList.WorldInfo> Worlds = new List<LobbyS2CWorldList.WorldInfo>();
        void LobbyS2CWorldList(LobbyS2CWorldList list)
        {
            Worlds.AddRange(list.Worlds);
        }
        void LobbyS2CWorldOpen(LobbyS2CWorldOpen list)
        {
        }
        public Boolean TryLogin = true;
        void LobbyS2CCharList(LobbyS2CCharList list)
        {
            var loginThread = new Thread(() =>
            {
                for (int i = 0; i < 20; i++)
                {
                    if (!TryLogin)
                        return;
                    SendPacket(new LobbyC2SCharConnect { Session = list.ContentId, ReverseEncryption = EncryptedZero, ReverseEncryption2 = EncryptedZero });
                    Thread.Sleep(5000);
                }
            });
            loginThread.IsBackground = false;
            loginThread.Start();
        }
        void LobbyS2CWorldEnter(LobbyS2CWorldEnter enter)
        {
            TryLogin = false;
            var chatThread = new Thread(() =>
            {
                var chat = new ChatClient(enter.IpAddr, enter.Port, enter.EntityId, enter.ContentId, enter.Password, Key);
            });
            chatThread.IsBackground = false;
            chatThread.Start();
            var worldThread = new Thread(() =>
            {
                var world = new WorldClient(enter.IpAddr, enter.Port, enter.EntityId, enter.ContentId, enter.Password, Key);
            });
            worldThread.IsBackground = false;
            worldThread.Start();
            Thread.Sleep(50);
            TcpClient.Close();
        }
        void LobbyS2CError(LobbyS2CError queue)
        {
            Console.WriteLine("in line : " + queue.Queue);
        }
    }
}
