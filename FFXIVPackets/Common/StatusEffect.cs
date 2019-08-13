using System;
namespace FFXIV.Packets
{
    public class StatusEffect : Packet
    {
        public UInt16 EffectId;
        public UInt16 Unk;
        public Single Duration;
        public UInt32 SourceActorId;
    }
}
