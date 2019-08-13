using System;
using System.Collections.Generic;

namespace FFXIV.Packets
{
    public class WorldS2CPlayerStateFlags : OpcodePacket
    {
        public List<Byte> flags = new List<Byte>(new Byte[12]);
        public UInt32 padding;
    }
}
