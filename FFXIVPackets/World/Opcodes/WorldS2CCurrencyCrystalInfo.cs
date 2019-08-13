using System;

namespace FFXIV.Packets
{
    public class WorldS2CCurrencyCrystalInfo : OpcodePacket
    {
        public UInt32 containerSequence;
        public UInt16 containerId;
        public UInt16 slot;
        public UInt32 quantity;
        public UInt32 unknown;
        public UInt32 catalogId;
        public UInt32 unknown1;
        public UInt32 unknown2;
        public UInt32 unknown3;
    }
}
