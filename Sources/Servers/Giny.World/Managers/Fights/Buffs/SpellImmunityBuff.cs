using Giny.Protocol.Enums;
using Giny.World.Managers.Fights.Cast;
using Giny.World.Managers.Fights.Fighters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Giny.World.Managers.Fights.Buffs
{
    public class SpellImmunityBuff : Buff
    {
        public short SpellId
        {
            get;
            private set;
        }
        public SpellImmunityBuff(int id, Fighter target, SpellEffectHandler effectHandler, FightDispellableEnum dispellable, short spellId, short? customActionId = null) : base(id, target, effectHandler, dispellable, customActionId)
        {
            this.SpellId = spellId;
        }

        public override void Dispell()
        {

        }

        public override void Execute()
        {

        }

        public override short GetDelta()
        {
            return SpellId;
        }
    }
}
