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
    public class ShieldBuff : Buff
    {
        private const ActionsEnum ActionId = ActionsEnum.ACTION_CHARACTER_BOOST_SHIELD;

        public short Delta
        {
            get;
            set;
        }
        public ShieldBuff(int id, short delta, Fighter target, SpellEffectHandler effectHandler, FightDispellableEnum dispellable) :
            base(id, target, effectHandler, dispellable, (short)ActionId)
        {
            this.Delta = delta;
        }

        public override void Execute()
        {
            Target.Stats.AddShield(Delta);
        }

        public override void Dispell()
        {
            Target.Stats.RemoveShield(Delta);
        }

        public override short GetDelta()
        {
            return Delta;
        }
    }
}
