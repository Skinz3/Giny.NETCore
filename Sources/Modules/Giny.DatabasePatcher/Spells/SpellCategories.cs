using Giny.Core;
using Giny.Core.DesignPattern;
using Giny.Core.Extensions;
using Giny.Core.Misc;
using Giny.ORM;
using Giny.Protocol.Enums;
using Giny.World.Records.Spells;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Giny.DatabasePatcher.Spells
{
    public class SpellCategories
    {
        public static readonly EffectsEnum[] AgressiveEffects = new EffectsEnum[]
        {
            EffectsEnum.Effect_DamageFire,
            EffectsEnum.Effect_DamageWater,
            EffectsEnum.Effect_DamageNeutral,
            EffectsEnum.Effect_DamageAir,
            EffectsEnum.Effect_DamageEarth,
            EffectsEnum.Effect_StealHPAir,
            EffectsEnum.Effect_StealHPEarth,
            EffectsEnum.Effect_StealHPFire,
            EffectsEnum.Effect_StealHPWater,
            EffectsEnum.Effect_StealHPFix,
            EffectsEnum.Effect_StealHPNeutral,
            EffectsEnum.Effect_DamageFirePerAP,
            EffectsEnum.Effect_DamageWaterPerAP,
            EffectsEnum.Effect_SwitchPosition,
            EffectsEnum.Effect_SwitchPosition_1023,
            EffectsEnum.Effect_SymetricPointTeleport,
            EffectsEnum.Effect_PushBack,
            EffectsEnum.Effect_PullForward,
            EffectsEnum.Effect_Kill,
            EffectsEnum.Effect_KillAndSummon,
            EffectsEnum.Effect_DamagePercentWater,
            EffectsEnum.Effect_DamagePercentEarth,
            EffectsEnum.Effect_DamagePercentAir,
            EffectsEnum.Effect_DamagePercentFire,
            EffectsEnum.Effect_DamagePercentNeutral,
            EffectsEnum.Effect_LoseHPByUsingAP,
            EffectsEnum.Effect_DamageNeutralFix,
            EffectsEnum.Effect_DamageWaterPerHPLost,
            EffectsEnum.Effect_DamageEarthPerHPLost,
            EffectsEnum.Effect_DamageAirPerHPLost,
            EffectsEnum.Effect_DamageFirePerHPLost,
            EffectsEnum.Effect_DamageNeutralPerHPLost,
            EffectsEnum.Effect_DamagePercentNeutral_671,
            EffectsEnum.Effect_Punishment_Damage,
            EffectsEnum.Effect_CastSpell_792,
            EffectsEnum.Effect_CastSpell_793,
            EffectsEnum.Effect_CastSpell_1017,
            EffectsEnum.Effect_CastSpell_1018,
            EffectsEnum.Effect_CastSpell_1019,
            EffectsEnum.Effect_CastSpell_1160,
            EffectsEnum.Effect_CastSpell2017,
            EffectsEnum.Effect_DamageNeutralRemainingMP,
            EffectsEnum.Effect_DamageAirRemainingMP,
            EffectsEnum.Effect_DamageWaterRemainingMP,
            EffectsEnum.Effect_DamageFireRemainingMP,
            EffectsEnum.Effect_DamageEarthRemainingMP,
            EffectsEnum.Effect_PushBack_1021,
            EffectsEnum.Effect_PullForward_1022,
            EffectsEnum.Effect_Retreat,
            EffectsEnum.Effect_Advance,
            EffectsEnum.Effect_Attract,
            EffectsEnum.Effect_DamageEarthFix,
            EffectsEnum.Effect_DamageAirFix,
            EffectsEnum.Effect_DamageWaterFix,
            EffectsEnum.Effect_DamageFireFix,
            EffectsEnum.Effect_DamageAirPerHpPercent,
            EffectsEnum.Effect_DamageWaterPerHpPercent,
            EffectsEnum.Effect_DamageFirePerHpPercent,
            EffectsEnum.Effect_DamageEarthPerHpPercent,
            EffectsEnum.Effect_DamageNeutralPerHpPercent,
            EffectsEnum.Effect_DamageNeutralPerHPEroded,
            EffectsEnum.Effect_DamageAirPerHPEroded,
            EffectsEnum.Effect_DamageFirePerHPEroded,
            EffectsEnum.Effect_DamageWaterPerHPEroded,
            EffectsEnum.Effect_DamageEarthPerHPEroded,
            EffectsEnum.Effect_PushBack_1103,
            EffectsEnum.Effect_SymetricTargetTeleport,
            EffectsEnum.Effect_SymetricCasterTeleport,
            EffectsEnum.Effect_DamageNeutralPerCasterHPEroded,
            EffectsEnum.Effect_DamageAirPerCasterHPEroded,
            EffectsEnum.Effect_DamageFirePerCasterHPEroded,
            EffectsEnum.Effect_DamageWaterPerCasterHPEroded,
            EffectsEnum.Effect_DamageEarthPerCasterHPEroded,
            EffectsEnum.Effect_DamageNeutralPerCasterHPEroded,
            EffectsEnum.Effect_DamageAirPerCasterHPEroded,
            EffectsEnum.Effect_DamageFirePerCasterHPEroded,
            EffectsEnum.Effect_DamageWaterPerCasterHPEroded,
            EffectsEnum.Effect_DamageEarthPerCasterHPEroded,
            EffectsEnum.Effect_DamageAirPerAP,
            EffectsEnum.Effect_DamageNeutralPerAP,
            EffectsEnum.Effect_DamageEarthPerAP,
            EffectsEnum.Effect_DamageAirPerMP,
            EffectsEnum.Effect_DamageWaterPerMP,
            EffectsEnum.Effect_DamageFirePerMP,
            EffectsEnum.Effect_DamageNeutralPerMP,
            EffectsEnum.Effect_CastSpell_1175,
            EffectsEnum.Effect_DamageEarthPerMP,
            EffectsEnum.Effect_CastSpell_2160,
            EffectsEnum.Effect_CastSpell_2792,
            EffectsEnum.Effect_CastSpell_2793,
            EffectsEnum.Effect_CastSpell_2794,
            EffectsEnum.Effect_CastSpell_2795,
            EffectsEnum.Effect_DamageBestElement,
            EffectsEnum.Effect_DamageBestElement_2828,
            EffectsEnum.Effect_DamageBestElement_2829,
        };
        private static readonly EffectsEnum[] DebuffEffects = new EffectsEnum[]
        {
            EffectsEnum.Effect_SubAP,
            EffectsEnum.Effect_AddState,
            EffectsEnum.Effect_SubMP,
            EffectsEnum.Effect_RemoveAP,
            EffectsEnum.Effect_StealAP_440,
            EffectsEnum.Effect_StealAP_84,
            EffectsEnum.Effect_LostAP,
            EffectsEnum.Effect_LostMP,
            EffectsEnum.Effect_SubChance,
            EffectsEnum.Effect_SubVitality,
            EffectsEnum.Effect_SubAgility,
            EffectsEnum.Effect_SubIntelligence,
            EffectsEnum.Effect_SubWisdom,
            EffectsEnum.Effect_SubStrength,
            EffectsEnum.Effect_SubRange,
            EffectsEnum.Effect_SubRange_135,
            EffectsEnum.Effect_StealMP_77,
            EffectsEnum.Effect_SubAP_Roll,
            EffectsEnum.Effect_SubAirResistPercent,
            EffectsEnum.Effect_SubWaterResistPercent,
            EffectsEnum.Effect_SubFireResistPercent,
            EffectsEnum.Effect_SubEarthResistPercent,
            EffectsEnum.Effect_AddDamageMultiplicator,
            EffectsEnum.Effect_StealKamas,
            EffectsEnum.Effect_DispelMagicEffects,
            EffectsEnum.Effect_RevealsInvisible,
            EffectsEnum.Effect_LosingAP,
            EffectsEnum.Effect_LosingMP,
            EffectsEnum.Effect_SkipTurn,
            EffectsEnum.Effect_SubDamageBonus,
            EffectsEnum.Effect_SubCriticalHit,
            EffectsEnum.Effect_SubMagicDamageReduction,
            EffectsEnum.Effect_SubPhysicalDamageReduction,
            EffectsEnum.Effect_SubProspecting,
            EffectsEnum.Effect_SubHealBonus,
            EffectsEnum.Effect_SubDamageBonusPercent,
            EffectsEnum.Effect_SubNeutralResistPercent,
            EffectsEnum.Effect_SubEarthElementReduction,
            EffectsEnum.Effect_SubWaterElementReduction,
            EffectsEnum.Effect_SubAirElementReduction,
            EffectsEnum.Effect_SubFireElementReduction,
            EffectsEnum.Effect_SubNeutralElementReduction,
            EffectsEnum.Effect_StealChance,
            EffectsEnum.Effect_StealVitality,
            EffectsEnum.Effect_StealAgility,
            EffectsEnum.Effect_StealIntelligence,
            EffectsEnum.Effect_StealWisdom,
            EffectsEnum.Effect_StealStrength,
            EffectsEnum.Effect_StealRange,
            EffectsEnum.Effect_RemoveSpellEffects,
            EffectsEnum.Effect_SubAPAttack,
            EffectsEnum.Effect_SubMPAttack,
            EffectsEnum.Effect_SubPushDamageBonus,
            EffectsEnum.Effect_SubPushDamageReduction,
            EffectsEnum.Effect_SubCriticalDamageBonus,
            EffectsEnum.Effect_SubCriticalDamageReduction,
            EffectsEnum.Effect_SubEarthDamageBonus,
            EffectsEnum.Effect_SubFireDamageBonus,
            EffectsEnum.Effect_SubWaterDamageBonus,
            EffectsEnum.Effect_SubAirDamageBonus,
            EffectsEnum.Effect_SubNeutralDamageBonus,
            EffectsEnum.Effect_StealMP_441,
            EffectsEnum.Effect_SubEvade,
            EffectsEnum.Effect_SubLock,
            EffectsEnum.Effect_AddErosion,
            EffectsEnum.Effect_RandDownModifier,
            EffectsEnum.Effect_ReturnToOriginalPos,
            EffectsEnum.Effect_DispelState,
            EffectsEnum.Effect_DisableState,
            EffectsEnum.Effect_SkipTurn_1031,
            EffectsEnum.Effect_SubVitalityPercent,
            EffectsEnum.Effect_SubVitality_1047,
            EffectsEnum.Effect_SubVitalityPercent_1048,
            EffectsEnum.Effect_ReduceEffectsDuration,
            EffectsEnum.Effect_SubResistances,
            EffectsEnum.Effect_SubMP_Roll,
            EffectsEnum.Effect_Rewind,
            EffectsEnum.Effect_ReturnToLastPos,
            EffectsEnum.Effect_ReduceFinalDamages,
            EffectsEnum.Effect_SubMeleeDamageDonePercent,
            EffectsEnum.Effect_SubMeleeResistance,
            EffectsEnum.Effect_SubRangedDamageDonePercent,
            EffectsEnum.Effect_SubRangedResistance,
            EffectsEnum.Effect_SubWeaponDamageDonePercent,
            EffectsEnum.Effect_SubWeaponResistance,
            EffectsEnum.Effect_SubSpellDamageDonePercent,
            EffectsEnum.Effect_SubSpellResistance,
            EffectsEnum.Effect_SubStrengthPercent,
            EffectsEnum.Effect_SubAgilityPercent,
            EffectsEnum.Effect_SubIntelligencePercent,
            EffectsEnum.Effect_SubChancePercent,
            EffectsEnum.Effect_SubWisdomPercent,
            EffectsEnum.Effect_SubVitalityPercent_2845,
            EffectsEnum.Effect_SubAPPercent,
            EffectsEnum.Effect_SubMPPercent,
            EffectsEnum.Effect_SubDodgeMPPercent,
            EffectsEnum.Effect_ReduceSize,

        };
        private static readonly EffectsEnum[] HealingEffects = new EffectsEnum[]
        {
            EffectsEnum.Effect_HealHP_108,
            EffectsEnum.Effect_HealHP_143,
            EffectsEnum.Effect_HealHP_81,
            EffectsEnum.Effect_AddVitality,
            EffectsEnum.Effect_AddVitalityPercent,
            EffectsEnum.Effect_HealHP_407,
            EffectsEnum.Effect_RestoreHPPercent,
            EffectsEnum.Effect_DamageIntercept,

        };
        private static readonly EffectsEnum[] BuffEffects = new EffectsEnum[]
        {
            EffectsEnum.Effect_AddAP_111,
            EffectsEnum.Effect_AddMP_128,
            EffectsEnum.Effect_AddMP,
            EffectsEnum.Effect_AddEarthElementReduction,
            EffectsEnum.Effect_AddWaterElementReduction,
            EffectsEnum.Effect_AddAirElementReduction,
            EffectsEnum.Effect_AddFireElementReduction,
            EffectsEnum.Effect_AddNeutralElementReduction,
            EffectsEnum.Effect_HealOrMultiply,
            EffectsEnum.Effect_AddTrapBonus,
            EffectsEnum.Effect_AddTrapBonusPercent,
            EffectsEnum.Effect_AddNeutralResistPercent,
            EffectsEnum.Effect_AddShieldPercent,
            EffectsEnum.Effect_AddShield,
            EffectsEnum.Effect_AddDamageReflection_220,
            EffectsEnum.Effect_Invisibility,
            EffectsEnum.Effect_AddGlobalDamageReduction,
            EffectsEnum.Effect_AddGlobalDamageReduction_105,
            EffectsEnum.Effect_ReflectDamage,
            EffectsEnum.Effect_AddAirResistPercent,
            EffectsEnum.Effect_AddWaterResistPercent,
            EffectsEnum.Effect_AddFireResistPercent,
            EffectsEnum.Effect_AddEarthResistPercent,
            EffectsEnum.Effect_AddDamageBonus,
            EffectsEnum.Effect_AddDamageBonusPercent,
            EffectsEnum.Effect_AddCriticalHit,
            EffectsEnum.Effect_AddDamageBonus_121,
            EffectsEnum.Effect_ReflectSpell,
            EffectsEnum.Effect_AddHealth,
            EffectsEnum.Effect_ChangeAppearance,
            EffectsEnum.Effect_AddRange,
            EffectsEnum.Effect_AddStrength,
            EffectsEnum.Effect_AddAgility,
            EffectsEnum.Effect_RegainAP,
            EffectsEnum.Effect_AddChance,
            EffectsEnum.Effect_AddWisdom,
            EffectsEnum.Effect_AddIntelligence,
            EffectsEnum.Effect_AddRange_136,
            EffectsEnum.Effect_AddPhysicalDamage_137,
            EffectsEnum.Effect_IncreaseDamage_138,
            EffectsEnum.Effect_AddDodgeAPProbability,
            EffectsEnum.Effect_AddDodgeMPProbability,
            EffectsEnum.Effect_SubDodgeAPProbability,
            EffectsEnum.Effect_SubDodgeMPProbability,
            EffectsEnum.Effect_AddProspecting,
            EffectsEnum.Effect_AddHealBonus,
            EffectsEnum.Effect_AddSummonLimit,
            EffectsEnum.Effect_AddMagicDamageReduction,
            EffectsEnum.Effect_AddPhysicalDamageReduction,
            EffectsEnum.Effect_SpellBoostMinimalRange,
            EffectsEnum.Effect_SpellBoostRange,
            EffectsEnum.Effect_IncreaseDamageOfTheSpell,
            EffectsEnum.Effect_AddHealBonusOnSpell,
            EffectsEnum.Effect_SpellReduceApCost,
            EffectsEnum.Effect_ReduceSpellCooldownDelay,
            EffectsEnum.Effect_AddCriticalForSpell,
            EffectsEnum.Effect_DisableLOS,
            EffectsEnum.Effect_IncreaseMaxNumberOfCastPerTurn,
            EffectsEnum.Effect_IncreaseMaxNumberOfCastPerTarget,
            EffectsEnum.Effect_CooldownSet_292,
            EffectsEnum.Effect_SpellBoostBaseDamage,
            EffectsEnum.Effect_ReduceSpellRange,
            EffectsEnum.Effect_ReduceSpellMinimalRange,
            EffectsEnum.Effect_IncreaseSpellAPCost,
            EffectsEnum.Effect_ChangeColor,
            EffectsEnum.Effect_ChangeAppearance_335,
            EffectsEnum.Effect_AddAPAttack,
            EffectsEnum.Effect_AddEarthDamageBonus,
            EffectsEnum.Effect_AddFireDamageBonus,
            EffectsEnum.Effect_AddWaterDamageBonus,
            EffectsEnum.Effect_AddAirDamageBonus,
            EffectsEnum.Effect_AddNeutralDamageBonus,
            EffectsEnum.Effect_AddMPAttack,
            EffectsEnum.Effect_AddPushDamageBonus,
            EffectsEnum.Effect_AddPushDamageReduction,
            EffectsEnum.Effect_AddCriticalDamageBonus,
            EffectsEnum.Effect_AddCriticalDamageReduction,
            EffectsEnum.Effect_AddEvade,
            EffectsEnum.Effect_AddLock,
            EffectsEnum.Effect_RandUpModifier,
            EffectsEnum.Effect_GiveHpPercentWhenAttack,
            EffectsEnum.Effect_Punishment,
            EffectsEnum.Effect_AddShieldPercentLevel,
            EffectsEnum.Effect_ReduceSpellCooldown,
            EffectsEnum.Effect_SpellImmunity,
            EffectsEnum.Effect_CooldownSet,
            EffectsEnum.Effect_IncreaseDamage_1054,
            EffectsEnum.Effect_IncreaseSize,
            EffectsEnum.Effect_DamageSharing,
            EffectsEnum.Effect_AddResistances,
            EffectsEnum.Effect_DispatchDamages,
            EffectsEnum.Effect_HealBuff,
            EffectsEnum.Effect_DamageMultiplier,
            EffectsEnum.Effect_HealWhenAttack,
            EffectsEnum.Effect_IncreaseGlyphDamages,
            EffectsEnum.Effect_IncreaseFinalDamages,
            EffectsEnum.Effect_DispatchDamage_1223,
            EffectsEnum.Effect_HealReceivedDamages,
            EffectsEnum.Effect_TransmitCharacteristic,
            EffectsEnum.Effect_MeleeDamageDonePercent,
            EffectsEnum.Effect_AddMeleeResistance,
            EffectsEnum.Effect_RangedDamageDonePercent,
            EffectsEnum.Effect_AddRangedResistance,
            EffectsEnum.Effect_WeaponDamageDonePercent,
            EffectsEnum.Effect_WeaponResistance,
            EffectsEnum.Effect_SpellDamageDonePercent,
            EffectsEnum.Effect_AddSpellResistance,
            EffectsEnum.Effect_AddStrengthPercent,
            EffectsEnum.Effect_AddAgilityPercent,
            EffectsEnum.Effect_AddIntelligencePercent,
            EffectsEnum.Effect_AddChancePercent,
            EffectsEnum.Effect_AddWisdomPercent,
            EffectsEnum.Effect_AddVitalityPercent_2844,
            EffectsEnum.Effect_AddAPPercent,
            EffectsEnum.Effect_AddMPPercent,
            EffectsEnum.Effect_AddTacklePercent,
EffectsEnum.Effect_AddEvadePercent,
EffectsEnum.Effect_AddDodgeAPPercent,
EffectsEnum.Effect_AddDodgeMPPercent,
EffectsEnum.Effect_AddAPAttackPercent,
EffectsEnum.Effect_AddMPAttackPercent,
EffectsEnum.Effect_IncreaseSize_2868,
        };
        private static readonly EffectsEnum[] MarkEffects = new EffectsEnum[]
        {
            EffectsEnum.Effect_Trap,
            EffectsEnum.Effect_Glyph_1165,
            EffectsEnum.Effect_GlyphAura,
            EffectsEnum.Effect_TurnBeginGlyph,
            EffectsEnum.Effect_TurnEndGlyph,
        };
        private static readonly EffectsEnum[] SummonEffects = new EffectsEnum[]
        {
            EffectsEnum.Effect_Summon,
            EffectsEnum.Effect_ReviveAndGiveHPToLastDiedAlly,
            EffectsEnum.Effect_ReviveAlly,
            EffectsEnum.Effect_Double,
            EffectsEnum.Effect_ReviveAlly_1034,
            EffectsEnum.Effect_SummonsBomb,
        };
        private static readonly EffectsEnum[] TeleportEffects = new EffectsEnum[]
        {
            EffectsEnum.Effect_Teleport,
        };

        public static void Patch()
        {
            Logger.Write("Assigning spell categories ...");

            foreach (var spell in SpellRecord.GetSpellRecords())
            {
                AssignCategory(spell);
            }
        }

        private static SpellCategoryEnum GetEffectCategory(EffectsEnum effect)
        {
            if (AgressiveEffects.Contains(effect))
            {
                return SpellCategoryEnum.Agressive;
            }
            if (DebuffEffects.Contains(effect))
            {
                return SpellCategoryEnum.Debuff;
            }
            if (SummonEffects.Contains(effect))
            {
                return SpellCategoryEnum.Summon;
            }
            if (HealingEffects.Contains(effect))
            {
                return SpellCategoryEnum.Healing;
            }
            if (BuffEffects.Contains(effect))
            {
                return SpellCategoryEnum.Buff;
            }
            if (TeleportEffects.Contains(effect))
            {
                return SpellCategoryEnum.Teleport;
            }
            if (MarkEffects.Contains(effect))
            {
                return SpellCategoryEnum.Mark;
            }

            return SpellCategoryEnum.None;
        }
        private static void AssignCategory(SpellRecord record)
        {
            SpellLevelRecord level = record.Levels.LastOrDefault();

            Dictionary<SpellCategoryEnum, int> categories = new Dictionary<SpellCategoryEnum, int>();

            foreach (var effect in level.Effects)
            {
                var effectCategory = GetEffectCategory(effect.EffectEnum);

                if (categories.ContainsKey(effectCategory))
                {
                    categories[effectCategory] += 1;
                }
                else
                {
                    categories.Add(effectCategory, 1);
                }
            }

            if (categories.Count > 0)
                record.Category = categories.MaxBy(x => x.Value).Key;
            else
                record.Category = SpellCategoryEnum.None;

            record.UpdateInstantElement();



        }
    }
}
