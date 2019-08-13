using System;

namespace FFXIV.Packets
{
    public class WorldS2CSetOnlineStatus : OpcodePacket
    {
        public UInt64 Flags;
    }
}
