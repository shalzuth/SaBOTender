using System;

namespace FFXIV.Packets
{
    public class WorldS2CUpdateHpMpTp : OpcodePacket
    {
        public UInt32 hp;
        public UInt16 mp;
        public UInt16 tp;
        public UInt32 unknown_8;
        public UInt32 unknown_12;
    }
}
