using System;

namespace FFXIV.Packets
{
    public class WorldS2CActorMove : OpcodePacket
    {
        public Byte headRotation;
        public Byte rotation;
        public Byte animationType;
        public Byte animationState;
        public Byte animationSpeed;
        public Byte unknownRotation;
        public VectorUInt16 Position;
        public UInt32 Unk12;
    }
}
