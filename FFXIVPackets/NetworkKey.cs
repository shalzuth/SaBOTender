using System;
using System.Security.Cryptography;

namespace FFXIV.Packets
{
    public class NetworkKey : Packet
    {
        public UInt32 Magic = 0x12345678;
        public UInt32 Time;
        public UInt32 Version = 5000;
        public String Seed;
        public override Byte[] ToArray()
        {
            return MD5.Create().ComputeHash(base.ToArray());
        }
    }
}
