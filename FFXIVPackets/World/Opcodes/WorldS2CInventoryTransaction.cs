using System;

namespace FFXIV.Packets
{
    public class WorldS2CInventoryTransaction : OpcodePacket
    {
        public UInt32 sequence;
        public Byte type;
        public Byte padding;
        public UInt16 padding1;
        public UInt32 ownerId;
        public UInt32 storageId;
        public UInt16 slotId;
        public UInt16 padding2;
        public UInt32 stackSize;
        public UInt32 catalogId;
        public UInt32 someActorId;
        public UInt32 targetStorageId;
        public UInt32 padding3;
        public UInt32 padding4;
        public UInt32 padding5;
    }
}
