using System;

namespace FFXIV.Packets
{
    public class OpcodePacket : Packet
    {
        public UInt16 fourteen = 0x14;
        public UInt16 OpcodeType;
        public UInt32 unkInHeader;
        public DateTime Time;
        public UInt32 zeroInHeader;
        public LobbyOpcode LobbyOpcode { get { return (LobbyOpcode)OpcodeType; } }
        public WorldOpcode WorldOpcode { get { return (WorldOpcode)OpcodeType; } }
        public ChatOpcode ChatOpcode { get { return (ChatOpcode)OpcodeType; } }
    }
}
