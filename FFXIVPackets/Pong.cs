using System;

namespace FFXIV.Packets
{
    public class Pong : SubPacket
    {
        public PacketId PacketType = PacketId.Pong;
        public UInt32 EntityId;
        public DateTime Time = DateTime.Now;
    }
}
