using System;

namespace FFXIV.Packets
{
    public class WorldC2STriadPlayCard : OpcodePacket
    {
        public UInt16 three = 0x3;
        public UInt16 twnerythree = 0x23;
        public UInt32 unk4 = 0x4000000;
        public UInt32 five = 5;
        public UInt32 RoundNum;
        public UInt32 CardId;
        public UInt32 PosId;
    }
}
