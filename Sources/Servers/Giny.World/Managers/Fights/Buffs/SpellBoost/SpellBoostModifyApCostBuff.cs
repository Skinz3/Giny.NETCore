using Giny.Protocol.Enums;
using Giny.Protocol.Types;
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
        private short EffectiveDelta
        {
            get;
            set;
        }
        public SpellBoostModifyApCostBuff(int id, short spellId, short delta, Fighter target, SpellEffectHandler effectHandler, FightDispellableEnum dispellable, bool substract, short? customActionId = null) : base(id, spellId, delta, target, effectHandler, dispellable, customActionId)
        {
            this.EffectiveDelta = substract ? (short)-GetDelta() : GetDelta();
        }
        public override void Execute()
        {
            Target.SpellModifiers.ApplySpellModification(SpellId, SpellModifierTypeEnum.AP_COST, (short)-EffectiveDelta);
            base.Execute();
        }
        public override void Dispell()
        {
            Target.SpellModifiers.ApplySpellModification(SpellId, SpellModifierTypeEnum.AP_COST, EffectiveDelta);
            base.Dispell();
        }
    }
}
