using Giny.Protocol.Enums;
using Giny.World.Managers.Effects;
using Giny.World.Managers.Fights.Cast;
using Giny.World.Managers.Fights.Fighters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Giny.World.Managers.Fights.Buffs
{
    public class RescaleSkinBuff : Buff
    {
        public double Delta
        {
            get;
            set;
        }
        public RescaleSkinBuff(int id, double delta,  Fighter target, SpellEffectHandler effectHandler, FightDispellableEnum dispellable, short? customActionId = null) : base(id, target, effectHandler, dispellable, customActionId)
        {
            this.Delta = delta;
        }

        public override void Execute()
        {
            Target.UpdateLook(Cast.Source);
        }

        public override void Dispell()
        {
            Target.UpdateLook(Cast.Source);
        }

        public override short GetDelta()
        {
            return (short)(Delta * 100d);
        }
    }
}
