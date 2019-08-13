using System;

namespace FFXIV.Packets
{
    public class WorldS2CUpdateClassInfo : OpcodePacket
    {
        public Byte classId;
        public Byte level1;
        public UInt16 level;
        public UInt32 nextLevelIndex;
        public UInt32 currentExp;
        public UInt32 restedExp;
    }
}
