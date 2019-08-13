namespace FFXIV.Packets
{
    public class Opcode : SubPacket
    {
        public PacketId PacketType = PacketId.Opcode;
        public OpcodePacket Data;
    }
}
