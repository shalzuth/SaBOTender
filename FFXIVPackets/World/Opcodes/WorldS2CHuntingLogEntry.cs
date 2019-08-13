using System;
using System.Collections.Generic;

namespace FFXIV.Packets
{
    public class WorldS2CHuntingLogEntry : OpcodePacket
    {
        public UInt32 u0; // -1 for all normal classes
        public Byte rank; // starting from 0
        public Byte index; // classes and gcs
        public List<Byte> entries = new List<Byte>(new Byte[40]);
        public UInt16 pad;
        public UInt64 completeFlags; // 4 bit for each potential entry and the 5th bit for completion of the section
        public UInt64 pad1;
    }
}
