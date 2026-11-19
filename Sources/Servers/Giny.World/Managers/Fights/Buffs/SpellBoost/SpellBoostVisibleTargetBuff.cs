using Giny.Protocol.Enums;
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
    public class SpellBoostVisibleTargetBuff : SpellBoostBuff
    {
        public SpellBoostVisibleTargetBuff(int id, short spellId, short delta, Fighter target, SpellEffectHandler effectHandler, FightDispellableEnum dispellable, short? customActionId = null) : base(id, spellId, delta, target, effectHandler, dispellable, customActionId)
        {

        }
        public override void Execute()
        {
            Target.SpellModifiers.ApplySpellModification(SpellId, SpellModifierTypeEnum.VISIBLE_TARGET, SpellModifierActionTypeEnum.ACTION_BOOST, GetDelta());
            base.Execute();
        }
        public override void Dispell()
        {
            Target.SpellModifiers.ApplySpellModification(SpellId, SpellModifierTypeEnum.VISIBLE_TARGET, SpellModifierActionTypeEnum.ACTION_BOOST, (short)-GetDelta());
            base.Dispell();
        }
    }
}
