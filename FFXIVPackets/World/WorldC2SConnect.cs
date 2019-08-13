using System;
using System.Collections.Generic;
namespace FFXIV.Packets
{
    public class WorldC2SConnect : SubPacket
    {
        public PacketId PacketType = PacketId.WorldC2SConnect;
        public UInt32 Zero = 0;
        public String EntityId;
        public List<Byte> Zeros2;
    }
}
