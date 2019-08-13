using System;

namespace FFXIV.Packets
{
    public class LobbyS2CAccountList : OpcodePacket
    {
        public UInt32 One;
        public UInt32 Zero2;
        public UInt32 Unk1;
        public UInt32 Zero3;
        public UInt32 ServiceId;
        public UInt64 Zero4;
        public String Token = new String(' ', 32);
    }
}
