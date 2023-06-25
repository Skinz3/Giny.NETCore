using Giny.Core.DesignPattern;
using Giny.Protocol.Custom.Enums;
using Giny.Protocol.Enums;
using Giny.World.Managers.Effects;
using Giny.World.Managers.Fights.Cast;
using Giny.World.Managers.Fights.Fighters;
using Giny.World.Managers.Stats;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Giny.World.Managers.Fights.Effects.Steal
{
    [WIP("Weird client behaviour for steal range ? maybe we also should override client buff effect.id (and not only customActionId) ? GetBuffDisplayedEffect()")]
    [SpellEffectHandler(EffectsEnum.Effect_StealChance)]
    [SpellEffectHandler(EffectsEnum.Effect_StealWisdom)]
    [SpellEffectHandler(EffectsEnum.Effect_StealIntelligence)]
    [SpellEffectHandler(EffectsEnum.Effect_StealAgility)]
    [SpellEffectHandler(EffectsEnum.Effect_StealStrength)]
    [SpellEffectHandler(EffectsEnum.Effect_StealRange)]
    public class StatsSteal : SpellEffectHandler
    {
        public StatsSteal(EffectDice effect, SpellCastHandler castHandler) : base(effect, castHandler)
        {
        }

        protected override void Apply(IEnumerable<Fighter> targets)
        {
            var displayedEffects = GetBuffDisplayedEffect();

            foreach (var target in targets)
            {
                AddStatBuff(target, (short)-Effect.Min, GetEffectCaracteristic(target), FightDispellableEnum.DISPELLABLE, (short)displayedEffects[1]);
                AddStatBuff(Source, (short)Effect.Min, GetEffectCaracteristic(Source), FightDispellableEnum.DISPELLABLE, (short)displayedEffects[0]);
            }
        }

        private Characteristic GetEffectCaracteristic(Fighter target)
        {
            switch (Effect.EffectEnum)
            {
                case EffectsEnum.Effect_StealChance:
                    return target.Stats.Chance;
                case EffectsEnum.Effect_StealWisdom:
                    return target.Stats.Wisdom;
                case EffectsEnum.Effect_StealIntelligence:
                    return target.Stats.Intelligence;
                case EffectsEnum.Effect_StealAgility:
                    return target.Stats.Agility;
                case EffectsEnum.Effect_StealStrength:
                    return target.Stats.Strength;
                case EffectsEnum.Effect_StealRange:
                    return target.Stats[CharacteristicEnum.RANGE];
            }

            return null;
        }
        private EffectsEnum[] GetBuffDisplayedEffect()
        {
            switch (Effect.EffectEnum)
            {
                case EffectsEnum.Effect_StealChance:
                    return new[] { EffectsEnum.Effect_AddChance, EffectsEnum.Effect_SubChance };
                case EffectsEnum.Effect_StealWisdom:
                    return new[] { EffectsEnum.Effect_AddWisdom, EffectsEnum.Effect_SubWisdom };
                case EffectsEnum.Effect_StealIntelligence:
                    return new[] { EffectsEnum.Effect_AddIntelligence, EffectsEnum.Effect_SubIntelligence };
                case EffectsEnum.Effect_StealAgility:
                    return new[] { EffectsEnum.Effect_AddAgility, EffectsEnum.Effect_SubAgility };
                case EffectsEnum.Effect_StealStrength:
                    return new[] { EffectsEnum.Effect_AddStrength, EffectsEnum.Effect_SubStrength };
                case EffectsEnum.Effect_StealRange:
                    return new[] { EffectsEnum.Effect_AddRange, EffectsEnum.Effect_SubRange };
                default:
                    throw new Exception("No associated caracteristic to effect '" + Effect.EffectEnum + "'");
            }
        }
    }
}