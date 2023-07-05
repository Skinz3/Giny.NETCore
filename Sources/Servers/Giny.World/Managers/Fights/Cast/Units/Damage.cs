using Giny.Core.DesignPattern;
using Giny.Core.Time;
using Giny.IO.D2OClasses;
using Giny.Protocol.Custom.Enums;
using Giny.Protocol.Enums;
using Giny.World.Managers.Fights.Buffs.SpellBoost;
using Giny.World.Managers.Fights.Fighters;
using Giny.World.Managers.Fights.Triggers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Intrinsics.X86;
using System.Text;
using System.Threading.Tasks;

namespace Giny.World.Managers.Fights.Cast.Units
{
    public class Damage : ITriggerToken
    {
        public Fighter Source
        {
            get;
            private set;
        }
        public Fighter Target
        {
            get;
            private set;
        }
        public double BaseMinDamages
        {
            get;
            set;
        }
        public double BaseMaxDamages
        {
            get;
            set;
        }
        public short Delta
        {
            get;
            private set;
        }
        public short? Computed
        {
            get;
            set;
        }
        public EffectSchoolEnum EffectSchool
        {
            get;
            private set;
        }
        private SpellEffectHandler EffectHandler
        {
            get;
            set;
        }
        public bool IgnoreBoost
        {
            get;
            set;
        }
        public bool IgnoreResistances
        {
            get;
            set;
        }
        public bool IgnoreShield
        {
            get;
            set;
        }
        public bool WontTriggerBuffs
        {
            get;
            set;
        }

        public event Action<DamageResult> Applied;

        public Damage(Fighter source, Fighter target, EffectSchoolEnum school, double min, double max, SpellEffectHandler effectHandler = null)
        {
            this.Source = source;
            this.Target = target;
            this.BaseMaxDamages = max;
            this.BaseMinDamages = min;
            this.EffectSchool = school;
            this.EffectHandler = effectHandler;
            this.IgnoreBoost = false;
            this.IgnoreResistances = false;
            this.WontTriggerBuffs = false;
        }

        public void OnApplied(DamageResult applied)
        {
            this.Applied?.Invoke(applied);
        }
        public void Compute()
        {
            if (Computed.HasValue)
            {
                return;
            }
            if (EffectSchool == EffectSchoolEnum.Unknown)
            {
                throw new Exception("Unknown Effect school. Cannot compute damages.");
            }

            if (EffectSchool == EffectSchoolEnum.Fix)
            {
                Computed = new Jet(BaseMinDamages, BaseMaxDamages).Generate(Source.Random, Source.HasRandDownModifier(), Source.HasRandUpModifier());
                return;
            }
            if (EffectSchool == EffectSchoolEnum.Pushback)
            {
                if (BaseMinDamages != BaseMaxDamages)
                {
                    throw new Exception("Invalid push damages.");
                }

                Computed = (short)BaseMaxDamages;
                return;
            }

            if (BaseMinDamages <= 0)
            {
                Computed = 0;
                return;
            }

            Jet jet = EvaluateConcreteJet();

            if (!IgnoreBoost)
                ComputeCriticalDamageBonus(jet);

            ComputeShapeEfficiencyModifiers(jet);

            if (!IgnoreResistances)
                ComputeDamageResistances(jet);

            if (!IgnoreResistances)
                ComputeCriticalDamageReduction(jet);

            if (!IgnoreBoost)
                ComputeDamageDone(jet);

            if (!IgnoreBoost)
                ComputeFinalDamageBoost(jet);

            jet.ValidateBounds();

            //Source.Fight.Reply("Min:" + jet.Min + " Max:" + jet.Max, System.Drawing.Color.Red);

            Computed = jet.Generate(Source.Random, Source.HasRandDownModifier(), Source.HasRandUpModifier());

        }

        private void ComputeFinalDamageBoost(Jet jet)
        {
            jet.Min += jet.Min * (Source.Stats.FinalDamagePercent / 100d);
            jet.Max += jet.Max * (Source.Stats.FinalDamagePercent / 100d);
        }

        private void ComputeCriticalDamageBonus(Jet jet)
        {
            if (this.EffectHandler.CastHandler.Cast.IsCriticalHit)
            {
                jet.Min += this.Source.Stats[CharacteristicEnum.CRITICAL_DAMAGE_BONUS].TotalInContext();
                jet.Max += this.Source.Stats[CharacteristicEnum.CRITICAL_DAMAGE_BONUS].TotalInContext();
            }
        }
        private void ComputeCriticalDamageReduction(Jet jet)
        {
            if (this.EffectHandler.CastHandler.Cast.IsCriticalHit)
            {
                jet.Min -= this.Target.Stats[CharacteristicEnum.CRITICAL_DAMAGE_REDUCTION].TotalInContext();
                jet.Max -= this.Target.Stats[CharacteristicEnum.CRITICAL_DAMAGE_REDUCTION].TotalInContext();
            }
        }

        private void ComputeDamageDone(Jet jet)
        {
            if (Source.IsMeleeWith(Target))
            {
                jet.ApplyMultiplicator(Source.Stats[CharacteristicEnum.DEALT_DAMAGE_MULTIPLIER_MELEE].TotalInContext());
                jet.ApplyMultiplicator(Target.Stats[CharacteristicEnum.RECEIVED_DAMAGE_MULTIPLIER_MELEE].TotalInContext());

            }
            else
            {
                jet.ApplyMultiplicator(Source.Stats[CharacteristicEnum.DEALT_DAMAGE_MULTIPLIER_DISTANCE].TotalInContext());
                jet.ApplyMultiplicator(Target.Stats[CharacteristicEnum.RECEIVED_DAMAGE_MULTIPLIER_DISTANCE].TotalInContext());

            }

            if (this.EffectHandler.CastHandler.Cast.Weapon)
            {
                jet.ApplyMultiplicator(Source.Stats[CharacteristicEnum.DEALT_DAMAGE_MULTIPLIER_WEAPON].TotalInContext());
                jet.ApplyMultiplicator(Target.Stats[CharacteristicEnum.RECEIVED_DAMAGE_MULTIPLIER_WEAPON].TotalInContext());

            }

            if (this.IsSpellDamage())
            {
                jet.ApplyMultiplicator(Source.Stats[CharacteristicEnum.DEALT_DAMAGE_MULTIPLIER_SPELLS].TotalInContext());
                jet.ApplyMultiplicator(Target.Stats[CharacteristicEnum.RECEIVED_DAMAGE_MULTIPLIER_SPELLS].TotalInContext());
            }


            jet.ApplyMultiplicator(Source.Stats[CharacteristicEnum.DEALT_DAMAGE_MULTIPLIER].TotalInContext());

        }
        private void ComputeShapeEfficiencyModifiers(Jet jet)
        {
            double efficiency = EffectHandler.Zone.GetShapeEfficiency(Target.Cell, EffectHandler.CastHandler.Cast.TargetCell);
            jet.Min = (short)(jet.Min * efficiency);
            jet.Max = (short)(jet.Max * efficiency);
        }

        private void ComputeDamageResistances(Jet jet)
        {
            int resistPercent = 0;
            int reduction = 0;

            switch (EffectSchool)
            {
                case EffectSchoolEnum.Earth:
                    resistPercent = Target.Stats[CharacteristicEnum.EARTH_ELEMENT_RESIST_PERCENT].TotalInContext();
                    reduction = Target.Stats[CharacteristicEnum.EARTH_ELEMENT_REDUCTION].TotalInContext();
                    break;
                case EffectSchoolEnum.Air:
                    resistPercent = Target.Stats[CharacteristicEnum.AIR_ELEMENT_RESIST_PERCENT].TotalInContext();
                    reduction = Target.Stats[CharacteristicEnum.AIR_ELEMENT_REDUCTION].TotalInContext();
                    break;
                case EffectSchoolEnum.Water:
                    resistPercent = Target.Stats[CharacteristicEnum.WATER_ELEMENT_RESIST_PERCENT].TotalInContext();
                    reduction = Target.Stats[CharacteristicEnum.WATER_ELEMENT_REDUCTION].TotalInContext();
                    break;
                case EffectSchoolEnum.Fire:
                    resistPercent = Target.Stats[CharacteristicEnum.FIRE_ELEMENT_RESIST_PERCENT].TotalInContext();
                    reduction = Target.Stats[CharacteristicEnum.FIRE_ELEMENT_REDUCTION].TotalInContext();
                    break;
                case EffectSchoolEnum.Neutral:
                    resistPercent = Target.Stats[CharacteristicEnum.NEUTRAL_ELEMENT_RESIST_PERCENT].TotalInContext();
                    reduction = Target.Stats[CharacteristicEnum.NEUTRAL_ELEMENT_REDUCTION].TotalInContext();
                    break;
            }

            jet.Min = (1.0d - (resistPercent / 100.0d)) * (jet.Min - reduction);
            jet.Max = (1.0d - (resistPercent / 100.0d)) * (jet.Max - reduction);
        }

        public Jet EvaluateConcreteJet()
        {

            short boost = Source.SpellModifiers.GetModifier(EffectHandler.CastHandler.Cast.SpellId, SpellModifierTypeEnum.BASE_DAMAGE);

            if (BaseMaxDamages == 0 || BaseMaxDamages <= BaseMinDamages)
            {
                BaseMinDamages += boost;

                short delta = GetJetDelta(BaseMinDamages);

                return new Jet(delta, delta);
            }
            else
            {
                double jetMin = BaseMinDamages + boost;
                double jetMax = BaseMaxDamages + boost;

                short deltaMin = GetJetDelta(jetMin);
                short deltaMax = GetJetDelta(jetMax);

                return new Jet(deltaMin, deltaMax);
            }
        }
        public bool IsSpellDamage()
        {
            return EffectHandler != null && !EffectHandler.CastHandler.Cast.Weapon;
        }
        private short GetJetDelta(double jet)
        {
            double weaponDamageBonus = 0;
            double spellDamageBonus = 0;
            double damageBonusPercent = 0;
            double elementDamageBonus = 0;
            double allDamageBonus = 0;
            double elementDelta = 0;

            if (this.EffectHandler.CastHandler.Cast.Weapon)
            {
                weaponDamageBonus = Source.Stats[CharacteristicEnum.WEAPON_POWER].TotalInContext();
            }
            else if (IsSpellDamage())
            {
                spellDamageBonus = Source.Stats[CharacteristicEnum.DAMAGE_PERCENT_SPELL].TotalInContext();
            }

            if (!IgnoreBoost)
            {
                allDamageBonus = Source.Stats[CharacteristicEnum.ALL_DAMAGE_BONUS].TotalInContext();
                damageBonusPercent = Source.Stats[CharacteristicEnum.DAMAGE_PERCENT].TotalInContext();
            }

            if (!IgnoreBoost)
            {

                switch (EffectSchool)
                {
                    case EffectSchoolEnum.Neutral:
                        elementDelta = Source.Stats.Strength.TotalInContext();
                        elementDamageBonus = Source.Stats[CharacteristicEnum.NEUTRAL_DAMAGE_BONUS].TotalInContext();
                        break;
                    case EffectSchoolEnum.Earth:
                        elementDelta = Source.Stats.Strength.TotalInContext();
                        elementDamageBonus = Source.Stats[CharacteristicEnum.EARTH_DAMAGE_BONUS].TotalInContext();
                        break;
                    case EffectSchoolEnum.Water:
                        elementDelta = Source.Stats.Chance.TotalInContext();
                        elementDamageBonus = Source.Stats[CharacteristicEnum.WATER_DAMAGE_BONUS].TotalInContext();
                        break;
                    case EffectSchoolEnum.Air:
                        elementDelta = Source.Stats.Agility.TotalInContext();
                        elementDamageBonus = Source.Stats[CharacteristicEnum.AIR_DAMAGE_BONUS].TotalInContext();
                        break;
                    case EffectSchoolEnum.Fire:
                        elementDelta = Source.Stats.Intelligence.TotalInContext();
                        elementDamageBonus = Source.Stats[CharacteristicEnum.FIRE_DAMAGE_BONUS].TotalInContext();
                        break;
                    default:
                        elementDelta = jet;
                        break;
                }
            }




            double result = (double)(jet * (100d + elementDelta + damageBonusPercent + weaponDamageBonus + spellDamageBonus) / 100.0d + (allDamageBonus + elementDamageBonus));


            return (short)result;                        // (short)(result < jet ? jet : result);
        }

        public SpellEffectHandler GetEffectHandler()
        {
            return EffectHandler;
        }
        public Fighter GetSource()
        {
            return Source;
        }
    }
}
