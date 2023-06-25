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
    public class InvisibilityBuff : Buff
    {
        public InvisibilityBuff(int id, Fighter target, SpellEffectHandler effectHandler, FightDispellableEnum dispellable, short? customActionId = null) : base(id, target, effectHandler, dispellable, customActionId)
        {
        }

        public override void Execute()
        {
            Target.SetInvisiblityState(GameActionFightInvisibilityStateEnum.INVISIBLE, Cast.Source);
        }

        public override void Dispell()
        {
            Target.SetInvisiblityState(GameActionFightInvisibilityStateEnum.VISIBLE, Cast.Source);
        }

        public override short GetDelta()
        {
            return 0;
        }
    }
}
