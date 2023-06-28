using Giny.Core.Time;
using Giny.Protocol.Custom.Enums;
using Giny.Protocol.Enums;
using Giny.World.Managers.Effects;
using Giny.World.Managers.Fights.Buffs;
using Giny.World.Managers.Fights.Cast;
using Giny.World.Managers.Fights.Fighters;
using Giny.World.Managers.Stats;
using Giny.World.Records.Maps;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Giny.World.Managers.Fights.Effects.Buffs
{
    [SpellEffectHandler(EffectsEnum.Effect_AddDamageBonus)]
    [SpellEffectHandler(EffectsEnum.Effect_AddRange_136)]
    [SpellEffectHandler(EffectsEnum.Effect_AddWisdom)]
    [SpellEffectHandler(EffectsEnum.Effect_AddAgility)]
    [SpellEffectHandler(EffectsEnum.Effect_IncreaseDamage_138)]
    [SpellEffectHandler(EffectsEnum.Effect_AddStrength)]
    [SpellEffectHandler(EffectsEnum.Effect_AddChance)]
    [SpellEffectHandler(EffectsEnum.Effect_AddIntelligence)]
    [SpellEffectHandler(EffectsEnum.Effect_AddRange)]
    [SpellEffectHandler(EffectsEnum.Effect_AddWeaponDamageBonus)]
    [SpellEffectHandler(EffectsEnum.Effect_MeleeDamageDonePercent)]
    [SpellEffectHandler(EffectsEnum.Effect_AddCriticalHit)]
    [SpellEffectHandler(EffectsEnum.Effect_RangedDamageDonePercent)]
    [SpellEffectHandler(EffectsEnum.Effect_AddDodgeMPProbability)]
    [SpellEffectHandler(EffectsEnum.Effect_AddDodgeAPProbability)]
    [SpellEffectHandler(EffectsEnum.Effect_AddMeleeResistance)]
    [SpellEffectHandler(EffectsEnum.Effect_AddRangedResistance)]
    [SpellEffectHandler(EffectsEnum.Effect_AddSpellResistance)]
    [SpellEffectHandler(EffectsEnum.Effect_AddLock)]
    [SpellEffectHandler(EffectsEnum.Effect_AddWaterDamageBonus)]
    [SpellEffectHandler(EffectsEnum.Effect_AddFireDamageBonus)]
    [SpellEffectHandler(EffectsEnum.Effect_AddAirDamageBonus)]
    [SpellEffectHandler(EffectsEnum.Effect_AddEarthDamageBonus)]
    [SpellEffectHandler(EffectsEnum.Effect_AddNeutralDamageBonus)]
    [SpellEffectHandler(EffectsEnum.Effect_AddAPAttack)]
    [SpellEffectHandler(EffectsEnum.Effect_AddMPAttack)]
    [SpellEffectHandler(EffectsEnum.Effect_AddFireResistPercent)]
    [SpellEffectHandler(EffectsEnum.Effect_AddWaterResistPercent)]
    [SpellEffectHandler(EffectsEnum.Effect_AddEarthResistPercent)]
    [SpellEffectHandler(EffectsEnum.Effect_AddAirResistPercent)]
    [SpellEffectHandler(EffectsEnum.Effect_AddNeutralResistPercent)]
    [SpellEffectHandler(EffectsEnum.Effect_AddEvade)]
    [SpellEffectHandler(EffectsEnum.Effect_AddDamageBonusPercent)]
    [SpellEffectHandler(EffectsEnum.Effect_AddNeutralElementReduction)]
    [SpellEffectHandler(EffectsEnum.Effect_AddFireElementReduction)]
    [SpellEffectHandler(EffectsEnum.Effect_AddAirElementReduction)]
    [SpellEffectHandler(EffectsEnum.Effect_AddEarthElementReduction)]
    [SpellEffectHandler(EffectsEnum.Effect_AddWaterElementReduction)]
    [SpellEffectHandler(EffectsEnum.Effect_AddTrapBonusPercent)]
    [SpellEffectHandler(EffectsEnum.Effect_WeaponDamageDonePercent)]
    [SpellEffectHandler(EffectsEnum.Effect_AddPushDamageReduction)]
    [SpellEffectHandler(EffectsEnum.Effect_AddTrapBonus)]
    [SpellEffectHandler(EffectsEnum.Effect_AddHealBonus)]
    [SpellEffectHandler(EffectsEnum.Effect_AddPushDamageBonus)]
    [SpellEffectHandler(EffectsEnum.Effect_AddCriticalDamageReduction)]
    [SpellEffectHandler(EffectsEnum.Effect_AddCriticalDamageBonus)]
    [SpellEffectHandler(EffectsEnum.Effect_SpellDamageDonePercent)]
    [SpellEffectHandler(EffectsEnum.Effect_IncreaseSpellDamage)]
    public class StatsBuff : SpellEffectHandler
    {
        public const FightDispellableEnum Dispellable = FightDispellableEnum.DISPELLABLE;

        public StatsBuff(EffectDice effect, SpellCastHandler castHandler) :
            base(effect, castHandler)
        {

        }

        protected override void Apply(IEnumerable<Fighter> targets)
        {
            short delta = Effect.GetDelta();

            foreach (var target in targets)
            {
                Characteristic characteristic = target.Stats[GetAssociatedCharacteristicEnum()];
                AddStatBuff(target, delta, characteristic, Dispellable);
            }

        }


    }
}
