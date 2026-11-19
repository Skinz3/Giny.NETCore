using Giny.Protocol.Enums;
using Giny.World.Managers.Effects;
using Giny.World.Managers.Fights.Buffs;
using Giny.World.Managers.Fights.Buffs.SpellBoost;
using Giny.World.Managers.Fights.Cast;
using Giny.World.Managers.Fights.Fighters;
using Giny.World.Records.Maps;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Giny.World.Managers.Fights.Effects.Buffs.Spells
{
    /*
     * Forgelance (Désarmé)
     */
    [SpellEffectHandler(EffectsEnum.Effect_SetSpellRangeMin)]
    public class SpellSetMinRange : SpellEffectHandler
    {
        public SpellSetMinRange(EffectDice effect, SpellCastHandler castHandler) : base(effect, castHandler)
        {

        }

        protected override void Apply(IEnumerable<Fighter> targets)
        {
            short spellId = (short)Effect.Min;
            short value = (short)Effect.Value;

            foreach (var target in targets)
            {
                int id = target.BuffIdProvider.Pop();
                Buff buff = new SpellBoostModifyMinimalRangeBuff(id, spellId, value, target, this, Effect.DispellableEnum, SpellModifierActionTypeEnum.ACTION_SET);
                target.AddBuff(buff);
            }
        }
    }
}
