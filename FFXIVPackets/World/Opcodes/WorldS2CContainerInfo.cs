using System;

namespace FFXIV.Packets
{
    public class WorldS2CContainerInfo : OpcodePacket
    {
        public UInt32 Sequence;
        public UInt32 NumItems;
        public UInt32 ContainerId;
        public UInt32 Unk;
    }
}
