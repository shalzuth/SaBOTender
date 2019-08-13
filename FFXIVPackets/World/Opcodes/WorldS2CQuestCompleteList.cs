using System;
using System.Collections.Generic;

namespace FFXIV.Packets
{
    public class WorldS2CQuestCompleteList : OpcodePacket
    {
        public List<Byte> questCompleteMask = new List<Byte>(new Byte[480]);
        public List<Byte> unknownCompleteMask = new List<Byte>(new Byte[32]);
    }
}
