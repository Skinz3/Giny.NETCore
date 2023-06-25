using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Giny.Core.Time;
using Giny.Protocol.Custom.Enums;
using Giny.Protocol.Enums;
using Giny.Protocol.Types;
using Giny.World.Managers.Effects.Targets;
using Giny.World.Managers.Fights.Cast;
using ProtoBuf;

namespace Giny.World.Managers.Effects
{
    [ProtoContract]
    public class EffectDice : EffectInteger
    {
        [ProtoMember(2)]
        public int Min
        {
            get;
            set;
        }
        [ProtoMember(3)]
        public int Max
        {
            get;
            set;
        }
        [ProtoMember(5)]
        public override int Value
        {
            get => base.Value;
            set => base.Value = value;
        }

        public EffectDice(EffectsEnum effectsEnum, int min, int max, int value) : base((short)effectsEnum, value)
        {
            this.Min = min;
            this.Max = max;
        }

        public EffectDice(short effectId, int min, int max, int value) : base(effectId, value)
        {
            this.Min = min;
            this.Max = max;
        }
        public EffectDice()
        {

        }
        public EffectInteger Generate(AsyncRandom random, bool perfect = false)
        {
            if (Value != 0)
            {
                return new EffectInteger(EffectId, Value);
            }
            if (Min > Max)
            {
                return new EffectInteger(EffectId, Min);
            }
            if (perfect)
            {
                return new EffectInteger(EffectId, Max);
            }
            else
            {
                return new EffectInteger(EffectId, (short)random.Next(Min, Max + 1));
            }
        }

        public short GetDelta()
        {
            return (short)(Min < Max ? new AsyncRandom().Next(Min, Max + 1) : Min);
        }

        public override bool Equals(object obj)
        {
            return obj is EffectDice ? this.Equals(obj as EffectDice) : false;
        }


        public bool Equals(EffectDice effect)
        {
            return this.EffectId == effect.EffectId && effect.Min == Min && effect.Max == Max && effect.Value == Value;
        }
        public override object Clone()
        {
            return new EffectDice(EffectId, Min, Max, Value)
            {
                Delay = Delay,
                Dispellable = Dispellable,
                Duration = Duration,
                Group = Group,
                Modificator = Modificator,
                TargetMask = TargetMask,
                TargetId = TargetId,
                Trigger = Trigger,
                RawTriggers = RawTriggers,
                Order = Order,
                Random = Random,
                RawZone = RawZone,
            };
        }
        public override ObjectEffect GetObjectEffect()
        {
            return new ObjectEffectDice()
            {
                actionId = EffectId,
                diceConst = Value,
                diceNum = Min,
                diceSide = Max,
            };
        }
    }
}
