using System;
namespace FFXIV.Packets
{
    public class WorldS2CAuth : SubPacket
    {
        public PacketId PacketType = PacketId.WorldS2CAuth;
        public UInt32 EntityId;
        public UInt32 Zero1;
    }
}
