using System;

namespace FFXIV.Packets
{
    public class LobbyS2CAuth : SubPacket
    {
        public PacketId PacketType = PacketId.LobbyS2CAuth;
        public UInt32 EntityId;
    }
}
