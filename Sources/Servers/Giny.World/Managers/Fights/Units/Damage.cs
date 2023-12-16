using Giny.Core.DesignPattern;
using Giny.Core.Time;
using Giny.IO.D2OClasses;
using Giny.Protocol.Custom.Enums;
using Giny.Protocol.Enums;
using Giny.World.Managers.Fights.Buffs.SpellBoost;
using Giny.World.Managers.Fights.Cast;
using Giny.World.Managers.Fights.Fighters;
using Giny.World.Managers.Fights.Triggers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Intrinsics.X86;
using System.Text;
using System.Threading.Tasks;

namespace Giny.World.Managers.Fights.Units
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

        public int? Computed
        {
            get;
            set;
        }
        public EffectElementEnum Element
        {
            get;
            private set;
        }
        public SpellEffectHandler Handler
        {
            get;
            private set;
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

        public bool Fix
        {
            get;
            set;
        }
        public bool FromPushback
        {
            get;
            set;
        }

        public event Action<DamageResult> Applied;

        public Damage(Fighter source, Fighter target, EffectElementEnum school, double min, double max, SpellEffectHandler effectHandler = null, bool fix = false)
        {
            this.Source = source;
            this.Target = target;
            this.BaseMaxDamages = max;
            this.BaseMinDamages = min;
            this.Element = school;
            this.Handler = effectHandler;
            this.IgnoreBoost = false;
            this.IgnoreResistances = false;
            this.WontTriggerBuffs = false;
            this.Fix = fix;
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
            if (Element == EffectElementEnum.Undefined)
            {
                throw new Exception("Unknown Effect school. Cannot compute damages.");
            }

            if (Fix)
            {
                Computed = new Jet(BaseMinDamages, BaseMaxDamages).Generate(Source.Random, Source.HasRandDownModifier(), Source.HasRandUpModifier());
                return;
            }
            if (FromPushback)
            {
                if (BaseMinDamages != BaseMaxDamages)
                {
                    throw new Exception("Invalid push damages.");
                }

                Computed = (int)BaseMaxDamages;
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

            jet.ComputeShapeEfficiencyModifiers(Target, Handler);

            if (!IgnoreResistances)
                ComputeDamageResistances(jet);

            if (!IgnoreResistances)
                ComputeCriticalDamageReduction(jet);

            if (!IgnoreBoost)
                ComputeFinalDamages(jet);

            jet.ValidateBounds();

            /*Source.Fight.Reply("Jet <b>(" + Math.Round(jet.Min, 1).ToString().Replace(",", ".") +
                " - " + Math.Round(jet.Max, 1).ToString().Replace(",", ".") + ")</b>", System.Drawing.Color.FromArgb(232, 149, 90)); */

            Computed = jet.Generate(Source.Random, Source.HasRandDownModifier(), Source.HasRandUpModifier());

        }

        private void ComputeCriticalDamageBonus(Jet jet)
        {
            if (this.Handler.CastHandler.Cast.IsCriticalHit)
            {
                jet.Min += this.Source.Stats[CharacteristicEnum.CRITICAL_DAMAGE_BONUS].TotalInContext();
                jet.Max += this.Source.Stats[CharacteristicEnum.CRITICAL_DAMAGE_BONUS].TotalInContext();
            }
        }
        private void ComputeCriticalDamageReduction(Jet jet)
        {
            if (this.Handler.CastHandler.Cast.IsCriticalHit)
            {
                jet.Min -= this.Target.Stats[CharacteristicEnum.CRITICAL_DAMAGE_REDUCTION].TotalInContext();
                jet.Max -= this.Target.Stats[CharacteristicEnum.CRITICAL_DAMAGE_REDUCTION].TotalInContext();
            }
        }

        private void ComputeFinalDamages(Jet jet)
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

            if (this.Handler.CastHandler.Cast.Weapon)
            {
                jet.ApplyMultiplicator(Source.Stats[CharacteristicEnum.DEALT_DAMAGE_MULTIPLIER_WEAPON].TotalInContext());
                jet.ApplyMultiplicator(Target.Stats[CharacteristicEnum.RECEIVED_DAMAGE_MULTIPLIER_WEAPON].TotalInContext());

            }

            if (this.IsSpellDamage())
            {
                jet.ApplyMultiplicator(Source.Stats[CharacteristicEnum.DEALT_DAMAGE_MULTIPLIER_SPELLS].TotalInContext());
                jet.ApplyMultiplicator(Target.Stats[CharacteristicEnum.RECEIVED_DAMAGE_MULTIPLIER_SPELLS].TotalInContext());
            }


            if (Source is SummonedBomb bomb)
            {
                jet.ApplyBonus(bomb.GetTotalComboBonus());
            }

            if (Handler.CastHandler.Cast.ThroughPortal)
            {
                jet.ApplyMultiplicator(Handler.CastHandler.Cast.PortalDamageMultiplier);
            }

            jet.ApplyMultiplicator(Source.Stats[CharacteristicEnum.DEALT_DAMAGE_MULTIPLIER].TotalInContext());

        }

        private void ComputeDamageResistances(Jet jet)
        {
            int resistPercent = 0;
            int reduction = 0;

            switch (Element)
            {
                case EffectElementEnum.Earth:
                    resistPercent = Target.Stats[CharacteristicEnum.EARTH_ELEMENT_RESIST_PERCENT].TotalInContext();
                    reduction = Target.Stats[CharacteristicEnum.EARTH_ELEMENT_REDUCTION].TotalInContext();
                    break;
                case EffectElementEnum.Air:
                    resistPercent = Target.Stats[CharacteristicEnum.AIR_ELEMENT_RESIST_PERCENT].TotalInContext();
                    reduction = Target.Stats[CharacteristicEnum.AIR_ELEMENT_REDUCTION].TotalInContext();
                    break;
                case EffectElementEnum.Water:
                    resistPercent = Target.Stats[CharacteristicEnum.WATER_ELEMENT_RESIST_PERCENT].TotalInContext();
                    reduction = Target.Stats[CharacteristicEnum.WATER_ELEMENT_REDUCTION].TotalInContext();
                    break;
                case EffectElementEnum.Fire:
                    resistPercent = Target.Stats[CharacteristicEnum.FIRE_ELEMENT_RESIST_PERCENT].TotalInContext();
                    reduction = Target.Stats[CharacteristicEnum.FIRE_ELEMENT_REDUCTION].TotalInContext();
                    break;
                case EffectElementEnum.Neutral:
                    resistPercent = Target.Stats[CharacteristicEnum.NEUTRAL_ELEMENT_RESIST_PERCENT].TotalInContext();
                    reduction = Target.Stats[CharacteristicEnum.NEUTRAL_ELEMENT_REDUCTION].TotalInContext();
                    break;
            }

            jet.Min = (1.0d - (resistPercent / 100.0d)) * (jet.Min - reduction);
            jet.Max = (1.0d - (resistPercent / 100.0d)) * (jet.Max - reduction);
        }

        public Jet EvaluateConcreteJet()
        {

            short boost = Source.SpellModifiers.GetModifierBoost(Handler.CastHandler.Cast.SpellId, SpellModifierTypeEnum.BASE_DAMAGE);

            if (BaseMaxDamages == 0 || BaseMaxDamages <= BaseMinDamages)
            {
                BaseMinDamages += boost;

                int delta = GetJetDelta(BaseMinDamages);

                return new Jet(delta, delta);
            }
            else
            {
                double jetMin = BaseMinDamages + boost;
                double jetMax = BaseMaxDamages + boost;

                int deltaMin = GetJetDelta(jetMin);
                int deltaMax = GetJetDelta(jetMax);

                return new Jet(deltaMin, deltaMax);
            }
        }
        public bool IsSpellDamage()
        {
            return Handler != null && !Handler.CastHandler.Cast.Weapon;
        }
        public bool IsWeaponDamage()
        {
            return Handler != null && Handler.CastHandler.Cast.Weapon;
        }
        private int GetJetDelta(double jet)
        {
            double weaponDamageBonus = 0;
            double spellDamageBonus = 0;
            double damageBonusPercent = 0;
            double elementDamageBonus = 0;
            double allDamageBonus = 0;
            double elementDelta = 0;

            if (this.Handler.CastHandler.Cast.Weapon)
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
                switch (Element)
                {
                    case EffectElementEnum.Neutral:
                        elementDelta = Source.Stats.Strength.TotalInContext();
                        elementDamageBonus = Source.Stats[CharacteristicEnum.NEUTRAL_DAMAGE_BONUS].TotalInContext();
                        break;
                    case EffectElementEnum.Earth:
                        elementDelta = Source.Stats.Strength.TotalInContext();
                        elementDamageBonus = Source.Stats[CharacteristicEnum.EARTH_DAMAGE_BONUS].TotalInContext();
                        break;
                    case EffectElementEnum.Water:
                        elementDelta = Source.Stats.Chance.TotalInContext();
                        elementDamageBonus = Source.Stats[CharacteristicEnum.WATER_DAMAGE_BONUS].TotalInContext();
                        break;
                    case EffectElementEnum.Air:
                        elementDelta = Source.Stats.Agility.TotalInContext();
                        elementDamageBonus = Source.Stats[CharacteristicEnum.AIR_DAMAGE_BONUS].TotalInContext();
                        break;
                    case EffectElementEnum.Fire:
                        elementDelta = Source.Stats.Intelligence.TotalInContext();
                        elementDamageBonus = Source.Stats[CharacteristicEnum.FIRE_DAMAGE_BONUS].TotalInContext();
                        break;
                    default:
                        elementDelta = jet;
                        break;
                }
            }



            double percentBonus = (100d + elementDelta + damageBonusPercent + weaponDamageBonus + spellDamageBonus) / 100.0d;
            percentBonus = Math.Max(1, percentBonus); // percent bonus cant be inferior to 1

            double fixBonus = allDamageBonus + elementDamageBonus;
            double result = jet * percentBonus + fixBonus;


            return (int)result;
        }


        public Fighter GetSource()
        {
            return Source;
        }
    }
}
