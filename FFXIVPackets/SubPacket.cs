using System;

namespace FFXIV.Packets
{
    public class SubPacket : Packet
    {
        public UInt32 Length;
        public UInt32 EntityIdSource;
        public UInt32 EntityIdTarget;

        public Blowfish Key { get; set; }
        public override byte[] ToArray()
        {
            var data = base.ToArray();
            if (Key != null)
                Key.Encipher(data, 16, data.Length - 16);
            return data;
        }
    }
}
