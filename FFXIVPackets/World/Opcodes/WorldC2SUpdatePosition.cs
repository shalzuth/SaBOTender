using System;
namespace FFXIV.Packets
{
    public class WorldC2SUpdatePosition : OpcodePacket
    {
        public Single Rotation;
        public Byte unk1 = 0;
        public Byte unk2 = 0;
        public Byte unk3 = 0;
        public Byte HeadPosition = 0;
        public VectorSingle Position;
        public Byte AnimationType;
        public Byte AnimationState;
        public Byte ClientAnimationType = 0;
        public Byte unk4 = 0;
    }
}
