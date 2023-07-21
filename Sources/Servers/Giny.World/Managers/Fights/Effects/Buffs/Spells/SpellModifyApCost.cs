using Giny.Protocol.Enums;
using Giny.World.Managers.Effects;
using Giny.World.Managers.Fights.Buffs.SpellBoost;
using Giny.World.Managers.Fights.Cast;
using Giny.World.Managers.Fights.Fighters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Giny.World.Managers.Fights.Effects.Buffs.Spells
{
    [SpellEffectHandler(EffectsEnum.Effect_IncreaseSpellAPCost)]
    [SpellEffectHandler(EffectsEnum.Effect_ReduceSpellApCost)]
    public class SpellModifyApCost : SpellEffectHandler
    {
        public SpellModifyApCost(EffectDice effect, SpellCastHandler castHandler) : base(effect, castHandler)
        {
        }

        protected override void Apply(IEnumerable<Fighter> targets)
        {
            short spellId = (short)Effect.Min;
            short delta = (short)Effect.Value;

            foreach (var target in targets)
            {
                if (target.HasSpell(spellId))
                {
                    int id = target.BuffIdProvider.Pop();
                    SpellBoostModifyApCostBuff buff = new SpellBoostModifyApCostBuff(id, spellId, delta,
                        target, this, Effect.DispellableEnum, GetModifierAction());
                    target.AddBuff(buff);
                }
            }
        }

        private SpellModifierActionTypeEnum GetModifierAction()
        {
            switch (Effect.EffectEnum)
            {
                case EffectsEnum.Effect_ReduceSpellApCost:
                    return SpellModifierActionTypeEnum.ACTION_DEBOOST;
                case EffectsEnum.Effect_IncreaseSpellAPCost:
                    return SpellModifierActionTypeEnum.ACTION_BOOST;
            }

            throw new InvalidOperationException("Unable to compute spell modifier action from effect.");
        }

    }
}
