using Giny.Protocol.Enums;
using Giny.World.Managers.Effects;
using Giny.World.Managers.Fights.Buffs.SpellBoost;
using Giny.World.Managers.Fights.Buffs;
using Giny.World.Managers.Fights.Cast;
using Giny.World.Managers.Fights.Fighters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Giny.World.Managers.Fights.Effects.Buffs.Spells
{
    [SpellEffectHandler(EffectsEnum.Effect_ReduceSpellRangeMax)]
    [SpellEffectHandler(EffectsEnum.Effect_AddSpellRangeMax)]
    [SpellEffectHandler(EffectsEnum.Effect_SetSpellRangeMax)]
    public class SpellModifyMaxRange : SpellEffectHandler
    {
        public SpellModifyMaxRange(EffectDice effect, SpellCastHandler castHandler) : base(effect, castHandler)
        {

        }

        protected override void Apply(IEnumerable<Fighter> targets)
        {
            short spellId = (short)Effect.Min;
            short delta = (short)Effect.Value;

            foreach (var target in targets)
            {
                int id = target.BuffIdProvider.Pop();
                Buff buff = new SpellBoostModifyRangeBuff(id, spellId, delta, target, this, Effect.DispellableEnum, GetModifierAction());
                target.AddBuff(buff);
            }
        }



        private SpellModifierActionTypeEnum GetModifierAction()
        {
            switch (Effect.EffectEnum)
            {
                case EffectsEnum.Effect_ReduceSpellRangeMax:
                    return SpellModifierActionTypeEnum.ACTION_DEBOOST;
                case EffectsEnum.Effect_AddSpellRangeMax:
                    return SpellModifierActionTypeEnum.ACTION_BOOST;
                case EffectsEnum.Effect_SetSpellRangeMax:
                    return SpellModifierActionTypeEnum.ACTION_SET;
            }

            throw new InvalidOperationException("Unable to compute spell modifier action from effect.");
        }
    }
}
