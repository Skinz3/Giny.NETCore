using Giny.Core.DesignPattern;
using Giny.IO.D2OClasses;
using Giny.Protocol.Custom.Enums;
using Giny.Protocol.Enums;
using Giny.World.Managers.Effects;
using Giny.World.Managers.Fights.Cast;
using Giny.World.Managers.Fights.Fighters;
using Giny.World.Managers.Fights.Zones.Sets;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Giny.World.Managers.Fights.Units
{
    public class Healing : ITriggerToken
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

        private double BaseMinHeal
        {
            get;
            set;
        }
        private double BaseMaxHeal
        {
            get;
            set;
        }
        public int? Computed
        {
            get;
            set;
        }
        private SpellEffectHandler Handler
        {
            get;
            set;
        }
        private EffectElementEnum Element
        {
            get;
            set;
        }
        public bool Fix
        {
            get;
            set;
        }
        public Healing(Fighter source, Fighter target, EffectElementEnum effectSchool, double baseMin, double baseMax, SpellEffectHandler? effectHandler = null, bool fix = false)
        {
            this.Source = source;
            this.Target = target;
            this.BaseMinHeal = baseMin;
            this.BaseMaxHeal = baseMax;
            this.Handler = effectHandler;
            this.Element = effectSchool;
            this.Fix = fix;
        }



        public void Compute()
        {
            if (Computed.HasValue)
            {
                return;
            }

            if (Fix)
            {
                Computed = new Jet(BaseMinHeal, BaseMaxHeal).Generate(Source.Random, Source.HasRandDownModifier(), Source.HasRandUpModifier());
                return;
            }

            Jet jet = EvaluateConcreteJet();

            if (Handler.CastHandler.Cast.Weapon)
            {
                jet.ApplyBonus(Source.Stats[CharacteristicEnum.WEAPON_POWER].TotalInContext());
            }

            jet.ComputeShapeEfficiencyModifiers(Target, Handler);

            jet.ApplyMultiplicator(Source.Stats[CharacteristicEnum.HEAL_MULTIPLIER].TotalInContext());

            Computed = jet.Generate(Source.Random, Source.HasRandDownModifier(), Source.HasRandUpModifier());
        }
        public Jet EvaluateConcreteJet()
        {
            if (BaseMaxHeal == 0 || BaseMaxHeal <= BaseMinHeal)
            {
                int delta = GetJetDelta(BaseMaxHeal);
                return new Jet(delta, delta);
            }
            else
            {
                int deltaMin = GetJetDelta(BaseMinHeal);
                int deltaMax = GetJetDelta(BaseMaxHeal);

                return new Jet(deltaMin, deltaMax);
            }
        }




        private int GetJetDelta(double jet)
        {
            double result;

            var bonus = Source.Stats[CharacteristicEnum.HEAL_BONUS].TotalInContext();

         
            switch (Element)
            {
                case EffectElementEnum.Earth:
                    result = jet * ((100d + Source.Stats.Strength.TotalInContext()) / 100d) + bonus;
                    break;
                case EffectElementEnum.Water:
                    result = jet * ((100d + Source.Stats.Chance.TotalInContext()) / 100d) + bonus;
                    break;
                case EffectElementEnum.Air:
                    result = jet * ((100d + Source.Stats.Agility.TotalInContext()) / 100d) + bonus;
                    break;
                case EffectElementEnum.Fire:
                    result = jet * ((100d + Source.Stats.Intelligence.TotalInContext()) / 100d) + bonus;
                    break;
                default:
                    throw new InvalidOperationException("Invalid healing effect school : " + Element);
            }

            return (int)result;
        }
        public Fighter GetSource()
        {
            return Source;
        }
    }
}
