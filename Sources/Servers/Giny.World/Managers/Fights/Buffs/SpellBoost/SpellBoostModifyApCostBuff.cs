using Giny.Protocol.Enums;
using Giny.Protocol.Types;
using Giny.World.Managers.Actions;
using Giny.World.Managers.Effects;
using Giny.World.Managers.Fights.Buffs.SpellBoost;
using Giny.World.Managers.Fights.Cast;
using Giny.World.Managers.Fights.Fighters;
using Giny.World.Records.Spells;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Giny.World.Managers.Fights.Buffs.SpellBoost
{
    public class SpellBoostModifyApCostBuff : SpellBoostBuff
    {
        public SpellBoostModifyApCostBuff(int id, short spellId, short delta, Fighter target, SpellEffectHandler effectHandler, FightDispellableEnum dispellable, short? customActionId = null) : base(id, spellId, delta, target, effectHandler, dispellable, customActionId)
        {
        }
        public override void Execute()
        {
            Target.SpellModifiers.ApplySpellModification(SpellId, CharacterSpellModificationTypeEnum.AP_COST, GetDelta());
            base.Execute();
        }
        public override void Dispell()
        {
            Target.SpellModifiers.ApplySpellModification(SpellId, CharacterSpellModificationTypeEnum.AP_COST, (short)-GetDelta());
            base.Dispell();
        }
    }
}
