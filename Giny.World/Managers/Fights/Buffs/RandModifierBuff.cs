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
    public class RandModifierBuff : Buff
    {
        public bool Up
        {
            get;
            private set;
        }
        public RandModifierBuff(int id, bool up,   Fighter target, SpellEffectHandler effectHandler, FightDispellableEnum dispellable, short? customActionId = null) : base(id,   target, effectHandler, dispellable, customActionId)
        {
            this.Up = up;
        }

        public override void Execute()
        {

        }

        public override void Dispell()
        {

        }

        public override short GetDelta()
        {
            return 0;
        }
    }
}
