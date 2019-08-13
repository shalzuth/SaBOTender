using System;

namespace FFXIV.Packets
{
    public class WorldS2CInventoryTransactionFinish : OpcodePacket
    {
        public UInt32 sequenceId;
        public UInt32 sequenceId1;
        public UInt64 padding;
    }
}
