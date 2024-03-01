using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Giny.Core;
using Giny.Core.Extensions;
using Giny.Core.Time;
using Giny.Protocol.Custom.Enums;
using Giny.Protocol.Enums;
using Giny.Protocol.Types;
using Giny.World.Managers.Effects.Targets;
using Giny.World.Managers.Fights.Cast;
using Giny.World.Records.Effects;
using Giny.World.Records.Spells;
using ProtoBuf;

namespace Giny.World.Managers.Effects
{
    [ProtoContract]
    public class EffectDice : EffectInteger
    {
        [ProtoMember(2)]
        public int Min
        {
            get
            {
                if (m_min < 0)
                {
                    Logger.Write("DiceMin < 0 Error on effect " + EffectEnum, Channels.Critical);
                }
                return m_min < 0 ? 0 : m_min;
            }
            set
            {
                m_min = value;
            }
        }
        [ProtoMember(3)]
        public int Max
        {
            get
            {
                if (m_max < 0)
                {
                    Logger.Write("DiceMax < 0 Error on effect " + EffectEnum, Channels.Critical);
                }

                return m_max < 0 ? 0 : m_max;
            }
            set
            {
                m_max = value;
            }
        }
        [ProtoMember(5)]
        public override int Value
        {
            get => base.Value;
            set => base.Value = value;
        }


        private int m_min;

        private int m_max;

        public bool IsDice => Max > Min;

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
        public EffectInteger Generate(Random random, bool perfect = false)
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
                return new EffectInteger(EffectId, random.Next(Min, Max + 1));
            }
        }

        public int GetDelta()
        {
            return Min < Max ? new AsyncRandom().Next(Min, Max + 1) : Min;
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

        public override string GetDescription()
        {
            EffectRecord record = EffectRecord.GetEffectRecord(EffectEnum);
            var result = record.Description;

            var min = Min.ToString();
            var max = Max.ToString();

            if (Max > Min)
            {
                result = result.Replace("#1", min);
                result = result.Replace("#2", max);
                result = result.Replace("{", "");
                result = result.Replace("}", "");
                result = result.Replace("~1", "");
                result = result.Replace("~2", "");
            }
            else
            {
                result = result.Replace("#2", "");
                result = result.Replace("#1", min);

                result = result.Replace("à -", "");
                result = result.Replace("à", "");
                result = result.Replace("{", "");
                result = result.Replace("}", "");
                result = result.Replace("~1", "");
                result = result.Replace("~2", "");

            }
            result = result.Replace("~ps", "");
            result = result.Replace("~zs", "");

            result = result.Replace("#3", Value.ToString());

            if (string.IsNullOrWhiteSpace(result))
            {
                result = EffectEnum.ToString().Replace("Effect_", "");
            }
            return result;
        }

        public Spell GetSpell()
        {
            return new Spell(SpellRecord.GetSpellRecord((short)Min), (byte)Max);
        }
    }
}
