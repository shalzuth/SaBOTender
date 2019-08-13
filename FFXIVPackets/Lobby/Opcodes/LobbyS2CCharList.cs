using System;
using System.Collections.Generic;

namespace FFXIV.Packets
{
    public class LobbyS2CCharList : OpcodePacket
    {
        public List<Byte> TBD = new List<Byte>(new Byte[88]);
        public UInt64 ContentId;
        public List<Byte> Pad2 = new List<Byte>(new Byte[8]);
        public UInt16 WorldId;
        public UInt16 WorldId2;
        public List<Byte> Pad0 = new List<Byte>(new Byte[9]);
        public String CharName = new String(' ', 32);
        public String WorldName1 = new String(' ', 32);
        public String WorldName2 = new String(' ', 32);
        public String JSON = new String(' ', 1285);
    }
}
