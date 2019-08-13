using System;
using System.Collections.Generic;

namespace FFXIV.Packets
{
    public class LobbyS2CWorldEnter : OpcodePacket
    {
        public UInt32 Three;
        public UInt32 Zero2;
        public UInt32 EntityId;
        public UInt32 Zero3;
        public UInt64 ContentId;
        public UInt32 Zero4;
        public String Password = new String(' ', 32);
        public List<Byte> Zero5 = new List<Byte>(new Byte[34]);
        public UInt16 Port;
        public String IpAddr = new String(' ', 16);
    }
}
