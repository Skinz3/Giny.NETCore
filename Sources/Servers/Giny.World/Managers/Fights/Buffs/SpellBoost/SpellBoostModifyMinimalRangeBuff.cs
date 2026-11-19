using Giny.Protocol.Enums;
using Giny.World.Managers.Fights.Cast;
using Giny.World.Managers.Fights.Fighters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Giny.World.Managers.Fights.Buffs.SpellBoost
{
    public class SpellBoostModifyMinimalRangeBuff : SpellBoostBuff
    {
        private SpellModifierActionTypeEnum ActionType
        {
            get;
            set;
        }
        public SpellBoostModifyMinimalRangeBuff(int id, short spellId, short delta, Fighter target, SpellEffectHandler effectHandler, FightDispellableEnum dispellable, SpellModifierActionTypeEnum actionType = SpellModifierActionTypeEnum.ACTION_BOOST, short? customActionId = null) : base(id, spellId, delta, target, effectHandler, dispellable, customActionId)
        {
            this.ActionType = actionType;
        }
        public override void Execute()
        {
            Target.SpellModifiers.ApplySpellModification(SpellId, SpellModifierTypeEnum.RANGE_MIN, ActionType, GetDelta());
            base.Execute();
        }
        public override void Dispell()
        {
            Target.SpellModifiers.ApplySpellModification(SpellId, SpellModifierTypeEnum.RANGE_MIN, ActionType, (short)-GetDelta());
            base.Dispell();
        }
    }
}
