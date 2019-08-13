using FFXIV.Packets;
using System;
using System.Collections.Generic;
using System.Net;
using System.Threading;

namespace FFXIV.Network
{
    public class WorldClient : NetworkClient
    {
        public String Password;
        public UInt64 ContentId;
        public String EntityIdString;
        public WorldClient(String ipAddr, UInt32 port, UInt32 entityId, UInt64 contentId, String password, Blowfish key)
        {
            Debug = true;
            EntityIdString = entityId.ToString();
            ContentId = contentId;
            Password = password;
            Key = key;
            PacketHeaderEnum = typeof(WorldOpcode);
            Connect(IPAddress.Parse(ipAddr), (Int32)port);
        }

        public override void InitSequence()
        {
            var connect = new WorldC2SConnect { EntityId = EntityIdString, Zeros2 = new List<Byte>(new Byte[36 - EntityIdString.Length]) };
            SendPacket(connect, 1);
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
        void WorldS2CAuth(WorldS2CAuth auth)
        {
            EntityId = auth.EntityId;
            SendPacket(Packets.WorldInit.ClientVersion());
        }
        void WorldInit(WorldInit init) { }
        void WorldS2CActorControl142(WorldS2CActorControl142 control) { }
        void WorldS2CActorControl143(WorldS2CActorControl143 control) { }
        void WorldS2CContainerInfo(WorldS2CContainerInfo info) { }
        void WorldS2CItemInfo(WorldS2CItemInfo info) { }
        void WorldS2CInventoryActionAck(WorldS2CInventoryActionAck ack) { }
        void WorldS2CInventoryTransaction(WorldS2CInventoryTransaction transaction) { }
        void WorldS2CInventoryTransactionFinish(WorldS2CInventoryTransactionFinish transaction) { }
        void WorldS2CPlayerStateFlags(WorldS2CPlayerStateFlags transaction) { }
        void WorldS2CHuntingLogEntry(WorldS2CHuntingLogEntry transaction) { }
        void WorldS2CSetOnlineStatus(WorldS2CSetOnlineStatus transaction) { }
        void WorldS2CPlayerSetup(WorldS2CPlayerSetup player)
        {
            Console.WriteLine("WorldS2CPlayerSetup : " + player.name);
        }
        WorldS2CPlayerSpawn shalz;
        void WorldS2CPlayerSpawn(WorldS2CPlayerSpawn player)
        {
            Console.WriteLine("WorldS2CPlayerSpawn : " + player.Name + " : " + player.pos.X + ", " + player.pos.Y + ", " + player.pos.Z);
            if (player.Name.Contains("Shalzuth"))
            {
                shalz = player;
                SendPacket(new WorldC2SUpdatePosition{ Position = player.pos, AnimationType = 228, AnimationState = 1});
            }
        }
        void WorldS2CPlayerStats(WorldS2CPlayerStats stats) { }
        void WorldS2CActorMove(WorldS2CActorMove actorMove)
        {
            if (shalz != null && shalz.Source == actorMove.Source)
            {
                Console.Write("move");
                SendPacket(new WorldC2SUpdatePosition { Position = actorMove.Position.ToVectorSingle(), AnimationType = 228, AnimationState = 1 });
            }
        }
        void WorldS2CUpdateHpMpTp(WorldS2CUpdateHpMpTp hpmptp) { }
        void WorldS2CPersistantEffect(WorldS2CPersistantEffect effect) { }
        void WorldS2CPlayerClassInfo(WorldS2CPlayerClassInfo info) { }
        void WorldS2CUpdateClassInfo(WorldS2CUpdateClassInfo info) { }
        void WorldS2CHousingLandFlags(WorldS2CHousingLandFlags housing) { }
        void WorldS2CInitZone(WorldS2CInitZone init) { }
        void WorldS2CQuestActiveList(WorldS2CQuestActiveList quests) { }
        void WorldS2CQuestCompleteList(WorldS2CQuestCompleteList quests) { }
        void WorldS2CQuestTracker(WorldS2CQuestTracker quest) { }
        void WorldS2CGCAffiliation(WorldS2CGCAffiliation affiliation) { }
        void WorldC2SHousingUpdateObjectPosition(WorldC2SHousingUpdateObjectPosition pos) { }
        void WorldC2SLandRenameHandler(WorldC2SLandRenameHandler name) { }
        void WorldS2CCurrencyCrystalInfo(WorldS2CCurrencyCrystalInfo info) { }
        void WorldC2SClientTrigger(WorldC2SClientTrigger trigger) { }
    }
}
