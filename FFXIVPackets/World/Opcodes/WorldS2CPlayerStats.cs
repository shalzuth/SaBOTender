using System;
using System.Collections.Generic;

namespace FFXIV.Packets
{
    public class WorldS2CPlayerStats : OpcodePacket
    {
        public UInt32 strength;
        public UInt32 dexterity;
        public UInt32 vitality;
        public UInt32 intelligence;
        public UInt32 mind;
        public UInt32 piety;
        public UInt32 hp;
        public UInt32 mp;
        public UInt32 tp;
        public UInt32 unknown;
        public UInt32 unknown_1;
        public UInt32 unknown_2;
        public UInt32 tenacity;
        public UInt32 attack;
        public UInt32 defense;
        public UInt32 accuracy;
        public UInt32 spellSpeed;
        public UInt32 magicDefense;
        public UInt32 criticalHitRate;
        public UInt32 resistanceSlashing;
        public UInt32 resistancePiercing;
        public UInt32 resistanceBlunt;
        public UInt32 attackMagicPotency;
        public UInt32 healingMagicPotency;
        public UInt32 fire;
        public UInt32 ice;
        public UInt32 wind;
        public UInt32 earth;
        public UInt32 lightning;
        public UInt32 water;
        public UInt32 determination;
        public UInt32 skillSpeed;
        public UInt32 spellSpeed1;
        public UInt32 spellSpeedMod;
        public UInt32 unknown_6;
        public UInt32 craftsmanship;
        public UInt32 control;
        public UInt32 gathering;
        public UInt32 perception;
        public UInt32 resistanceSlow;
        public UInt32 resistanceSilence;
        public UInt32 resistanceBlind;
        public UInt32 resistancePoison;
        public UInt32 resistanceStun;
        public UInt32 resistanceSleep;
        public UInt32 resistanceBind;
        public UInt32 resistanceHeavy;
        public List<UInt32> unknown_7 = new List<UInt32>(new UInt32[9]); // possibly level sync stats.
    }
}
