using System;

namespace FFXIV.Packets
{
    public class LobbyC2SCharConnect : OpcodePacket
    {
        public UInt32 Three = 3;
        public UInt32 Zero3 = 0;
        public UInt64 Session;
        public UInt64 ReverseEncryption;
        public UInt64 ReverseEncryption2;
    }
}
