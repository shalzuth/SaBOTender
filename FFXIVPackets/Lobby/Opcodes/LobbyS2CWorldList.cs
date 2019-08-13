using System;
using System.Collections.Generic;

namespace FFXIV.Packets
{
    public class LobbyS2CWorldList : OpcodePacket
    {
        public List<Byte> Zero2 = new List<Byte>(new Byte[12]);
        public UInt32 NumWorlds;
        public UInt64 Zero3;
        public List<WorldInfo> Worlds;
        public class WorldInfo : Packet
        {
            public UInt16 Something;
            public UInt16 WorldId;
            public List<Byte> Zero4 = new List<Byte>(new Byte[16]);
            public String WorldName = new String(' ', 64);
            //public List<Byte> WorldNameBytes = new List<Byte>(new Byte[64]);
            //public String WorldName { get { return Encoding.UTF8.GetString(WorldNameBytes.ToArray()); } }
        }
    }
}
