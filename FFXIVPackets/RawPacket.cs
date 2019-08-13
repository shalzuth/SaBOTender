using System;
using System.Collections.Generic;

namespace FFXIV.Packets
{
    public class RawPacket : Packet
    {
        public List<Byte> Magic = new List<Byte>(new Byte[16]);
        public UInt64 TimeStamp;
        public UInt32 Length;
        public UInt16 ConnectionType;
        public UInt16 NumSubPackets;
        public Byte ExpectsReply;
        public Byte Compressed;
        public List<Byte> Pad = new List<Byte>(new Byte[6]);
        public List<SubPacket> SubPackets = new List<SubPacket>();
    }
}
