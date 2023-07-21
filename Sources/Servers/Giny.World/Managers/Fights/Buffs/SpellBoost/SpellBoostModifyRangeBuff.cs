using Giny.Core.DesignPattern;
using Giny.Protocol.Enums;
using Giny.Protocol.Types;
using Giny.World.Managers.Fights.Buffs.SpellModification;
using Giny.World.Managers.Fights.Cast;
using Giny.World.Managers.Fights.Fighters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Giny.World.Managers.Fights.Buffs.SpellBoost
{
    public class SpellBoostModifyRangeBuff : SpellBoostBuff
    {
        private SpellModifierActionTypeEnum ActionType
        {
            get;
            set;
        }
        public SpellBoostModifyRangeBuff(int id, short spellId, short delta, Fighter target, SpellEffectHandler effectHandler, FightDispellableEnum dispellable, SpellModifierActionTypeEnum actionType, short? customActionId = null) : base(id, spellId, delta, target, effectHandler, dispellable, customActionId)
        {
            this.ActionType = actionType;
        }
        public override void Execute()
        {
            Target.SpellModifiers.ApplySpellModification(SpellId, SpellModifierTypeEnum.RANGE_MAX, ActionType, GetDelta());
            base.Execute();
        }
        public override void Dispell()
        {
            Target.SpellModifiers.ApplySpellModification(SpellId, SpellModifierTypeEnum.RANGE_MAX, ActionType, (short)-GetDelta());
            base.Dispell();
        }
    }
}
