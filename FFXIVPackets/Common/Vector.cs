using System;
namespace FFXIV.Packets
{
    public class VectorSingle : Packet
    {
        public Single X;
        public Single Y;
        public Single Z;
    }
    public class VectorUInt16 : Packet
    {
        public UInt16 X;
        public UInt16 Y;
        public UInt16 Z;
        public VectorSingle ToVectorSingle()
        {
            return new VectorSingle { X = X.ToSingle(), Y = Y.ToSingle(), Z = Z.ToSingle() };
        }
    }
}
