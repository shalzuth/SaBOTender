using System;

namespace FFXIV.Packets
{
    public class WorldInit : OpcodePacket
    {
        //public List<Byte> ZeroPad = new List<Byte>(new Byte[0x20]);
        public UInt64 Zero2;
        public UInt32 EntityId;
        public String Client = new String(' ', 4);
        public static WorldInit ClientVersion()
        {
            return new WorldInit { Client = "\0\0\0\0\0\0\0\0client\0\0RUDP\0\0\0\0RUDP2\0\0\0%u\0\0\0\0\0\0\0\0chs\0ko\0\0%s(%d):Assertion %s %s\0\0at\0\0wt\0\0%s(%d)" };
        }
    }
}
