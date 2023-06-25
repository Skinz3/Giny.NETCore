using Giny.Protocol.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Giny.World.Managers.Effects
{
    public class EffectString : Effect
    {
        public string Value
        {
            get;
            set;
        }

        public EffectString()
        {
        }

        public EffectString(short effectId, string value) : base(effectId)
        {
            this.Value = value;
        }

        public override ObjectEffect GetObjectEffect()
        {
            return new ObjectEffectString()
            {
                actionId = EffectId,
                value = Value,
            };
        }
        public override bool Equals(object obj)
        {
            return obj is EffectString ? this.Equals(obj as EffectString) : false;
        }
        public bool Equals(EffectString effect)
        {
            return this.EffectId == effect.EffectId && this.Value == effect.Value;
        }
        public override object Clone()
        {
            return new EffectString(EffectId, Value)
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

    }
}
