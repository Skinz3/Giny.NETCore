using Giny.Protocol.Enums;
using Giny.Protocol.Types;
using Giny.World.Managers.Actions;
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
    public class VitalityBuff : Buff
    {
        private short Delta
        {
            get;
            set;
        }
        public VitalityBuff(int id, short delta, Fighter target, SpellEffectHandler effectHandler, FightDispellableEnum dispellable,
            ActionsEnum actionId) :
            base(id, target, effectHandler, dispellable, (short)actionId)
        {
            this.Delta = delta;
        }

        public override void Execute()
        {
            Target.Stats.AddMaxVitality(GetDelta());
        }

        public override void Dispell()
        {
            Target.Stats.RemoveMaxVitality(GetDelta());
        }

        public override short GetDelta()
        {
            return Math.Abs(Delta);
        }
    }
}
