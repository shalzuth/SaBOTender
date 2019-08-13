using System;

namespace FFXIV.Packets
{
    public class WorldS2CInitZone : OpcodePacket
    {
        public UInt16 serverId;
        public UInt16 zoneId;
        public UInt16 unknown1;
        public UInt16 contentfinderConditionId;
        public UInt32 unknown3;
        public UInt32 unknown4;
        public Byte weatherId;
        public Byte bitmask;
        public Byte bitmask1;
        // bitmask1 findings
        //0 = unknown ( 7B F8 69 )
        //1 = show playguide window ( 7B 69 )
        //2 = unknown ( 7B 69 )
        //4 = disables record ready check ( 7B DF DF F8 F0 E4 110 (all sorts of social packets) )
        //8 = hide server icon ( 7B 69 )
        //16 = enable flight ( 7B F8 69 )
        //32 = unknown ( 7B F8 69 )
        //64 = unknown ( 7B F8 69 )
        //128 = shows message "You are now in the instanced area XX A.
        //Current instance can be confirmed at any time using the /instance text command." ( 7B F8 69 )

        public Byte unknown5;
        public UInt32 unknown8;
        public UInt16 festivalId;
        public UInt16 additionalFestivalId;
        public UInt32 unknown9;
        public UInt32 unknown10;
        public UInt32 unknown11;
        public UInt32 unknown12;
        public UInt32 unknown12a;
        public UInt32 unknown12b;
        public UInt32 unknown12c;
        public VectorSingle pos;
        public UInt32 unknown13;
    }
}
