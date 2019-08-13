using System;
using System.Collections.Generic;

namespace FFXIV.Packets
{
    public class LobbyC2SConnect : SubPacket
    {
        public PacketId PacketType = PacketId.LobbyC2SConnect;
        public List<Byte> Zeros = new List<Byte>(new Byte[0x24]);
        public String Seed = "115d12394e13cf63de08a8eccb0cc97e";
        public List<Byte> Zeros2 = new List<Byte>(new Byte[0x20]);
        public UInt32 Time = (UInt32)Utils.GetUnixTime();
        public List<Byte> Zeros3 = new List<Byte>(new Byte[0x200]);
    }
}
