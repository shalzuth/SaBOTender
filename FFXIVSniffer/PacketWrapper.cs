using System;

namespace FFXIVSniffer
{
    public class PacketWrapper
    {
        public Boolean S2C { get; set; }
        public String PacketType { get; set; }
        public String Channel { get; set; }
        public String OpCode { get { return BitConverter.ToString(Bytes, 40, 10); } }
        public FFXIV.Packets.Packet Packet { get; set; }
        public Byte[] Bytes;
    }
}
