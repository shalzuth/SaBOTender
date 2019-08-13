using System;

namespace FFXIV.Packets
{
    public class WorldS2CInventoryActionAck : OpcodePacket
    {
        public UInt32 sequence;
        public UInt16 type;
        public UInt16 Padding;
        public UInt32 Padding2;
        public UInt32 Padding3;
    }
}
