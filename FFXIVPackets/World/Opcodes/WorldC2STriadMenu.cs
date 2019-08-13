using System;

namespace FFXIV.Packets
{
    public class WorldC2STriadMenu : OpcodePacket
    {
        public UInt16 three = 0x3;
        public UInt16 twnerythree = 0x23;
        public UInt16 unk = 0;
        public Byte zero = 0;
        public Byte Stage; // 1, 2, 1, 1
        public UInt32 Stage2; // 1, 2, 3, 6
        public UInt32 Stage3; // 130, 1, 0, 40ed307f
    }
}
