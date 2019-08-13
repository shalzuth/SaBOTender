using System;

namespace FFXIV.Packets
{
    public class Ping : SubPacket
    {
        public PacketId PacketType = PacketId.Ping;
        public UInt32 EntityId;
        public DateTime Time = DateTime.Now;
    }
}
