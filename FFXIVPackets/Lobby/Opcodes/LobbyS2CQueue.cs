using System;
namespace FFXIV.Packets
{
    public class LobbyS2CError : OpcodePacket
    {
        public UInt32 Three;
        public UInt32 Zero3;
        public UInt32 Unk;
        public UInt32 Queue;
        public UInt32 Unk2;
    }
}
