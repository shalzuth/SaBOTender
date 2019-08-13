using System;
using System.Collections.Generic;

namespace FFXIV.Packets
{
    public class WorldS2CPlayerSpawn : OpcodePacket
    {
        public UInt16 title;
        public UInt16 u1b;
        public UInt16 currentWorldId;
        public UInt16 homeWorldId;

        public Byte gmRank;
        public Byte u3c;
        public Byte u4;
        public Byte onlineStatus;

        public Byte pose;
        public Byte u5a;
        public Byte u5b;
        public Byte u5c;

        public UInt64 targetId;
        public UInt32 u6;
        public UInt32 u7;
        public UInt64 mainWeaponModel;
        public UInt64 secWeaponModel;
        public UInt64 craftToolModel;

        public UInt32 u14;
        public UInt32 u15;
        public UInt32 bNPCBase;
        public UInt32 bNPCName;
        public UInt32 u18;
        public UInt32 u19;
        public UInt32 directorId;
        public UInt32 ownerId;
        public UInt32 u22;
        public UInt32 hPMax;
        public UInt32 hPCurr;
        public UInt32 displayFlags;
        public UInt16 fateID;
        public UInt16 mPCurr;
        public UInt16 tPCurr;
        public UInt16 mPMax;
        public UInt16 tPMax;
        public UInt16 modelChara;
        public UInt16 rotation;
        public UInt16 activeMinion;
        public Byte spawnIndex;
        public Byte state;
        public Byte persistentEmote;
        public Byte modelType;
        public Byte subtype;
        public Byte voice;
        public UInt16 u25c;
        public Byte enemyType;
        public Byte level;
        public Byte classJob;
        public Byte u26d;
        public UInt16 u27a;
        public Byte currentMount;
        public Byte mountHead;
        public Byte mountBody;
        public Byte mountFeet;
        public Byte mountColor;
        public Byte scale;
        public UInt32 u29b;
        public UInt32 u30b;
        public List<StatusEffect> effect = new List<StatusEffect>(new StatusEffect[30]);
        public VectorSingle pos;
        public List<UInt32> models = new List<UInt32>(new UInt32[10]);
        public String Name = new String(' ', 32);
        public List<Byte> look = new List<Byte>(new Byte[26]);
        public List<Byte> fcTag = new List<Byte>(new Byte[6]);
        public UInt32 unk30;
    }
}
