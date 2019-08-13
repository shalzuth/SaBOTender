using System;

namespace FFXIV.Packets
{
    public class WorldC2STriadPlay : OpcodePacket
    {
        public UInt16 three = 0x3;
        public UInt16 twnerythree = 0x23;
        public UInt32 unk6 = 0x6000000;
        public UInt32 four = 5;
        public UInt32 Card1;
        public UInt32 Card2;
        public UInt32 Card3;
        public UInt32 Card4;
        public UInt32 Card5;
        public UInt32 EntityKindaShift;
        public UInt32 ninec1 = 0x9c1;
    }
}
