using System;

namespace FFXIV.Packets
{
    public class WorldS2CItemInfo : OpcodePacket
    {
        public UInt32 containerSequence;
        public UInt32 unknown;
        public UInt16 containerId;
        public UInt16 slot;
        public UInt32 quantity;
        public UInt32 catalogId;
        public UInt32 reservedFlag;
        public UInt64 signatureId;
        public Byte hqFlag;
        public Byte unknown2;
        public UInt16 condition;
        public UInt16 spiritBond;
        public UInt16 stain;
        public UInt32 glamourCatalogId;
        public UInt16 materia1;
        public UInt16 materia2;
        public UInt16 materia3;
        public UInt16 materia4;
        public UInt16 materia5;
        public Byte buffer1;
        public Byte buffer2;
        public Byte buffer3;
        public Byte buffer4;
        public Byte buffer5;
        public Byte padding;
        public UInt32 unknown10;
    }
}
