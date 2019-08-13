using System;
using System.Collections.Generic;

namespace FFXIV.Packets
{
    public class LobbyC2SCharList : OpcodePacket
    {
        public UInt32 Two = 2;
        public UInt32 Zero3 = 0;
        public UInt16 One = 0x0100;
        public List<Byte> Pad = new List<byte>(new Byte[6]);
        public UInt64 ServiceId;
    }
}
