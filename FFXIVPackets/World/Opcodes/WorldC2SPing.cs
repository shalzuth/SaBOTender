using System;
using System.Collections.Generic;
namespace FFXIV.Packets
{
    public class WorldC2SPing : OpcodePacket
    {
        public UInt32 Timestamp;
        public List<Byte> pad0 = new List<byte>(new Byte[20]);
    }
}
