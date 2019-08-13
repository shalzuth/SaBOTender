using System;
using System.Collections.Generic;

namespace FFXIV.Packets
{
    public class LobbyC2SAuth : OpcodePacket
    {
        public UInt32 One = 1;
        public UInt32 Zero3 = 0;
        //public UInt16 XPackMaybe = 0x26;
        public UInt16 XPackMaybe = 0x35;
        public UInt16 XPack = 5000;
        public UInt16 XPack2 = 4944;
        public UInt32 Zero4 = 0;
        public String SessionId;
        public List<Byte> ZeroPad = new List<Byte>(new Byte[32]);
        //public String Hash = "ffxiv.exe/20795944/a7b261d90d00b84a727ab22f1eae064ea038bc39,ffxiv_dx11.exe/30715432/7654b7024dbe8a96a9ef715aee4b4f31193e83cb";
        public String Hash = "ffxiv.exe/20796456/1d01c47bec6185e8a11c1526108524c6ea97d133,ffxiv_dx11.exe/30716456/35d8b92e2aff273a26d843c0b06ee21e248bfac9";
        public List<Byte> ZeroPad2 = new List<Byte>(new Byte[930]);
        public UInt64 ReverseEncryption = 0;
    }
}
