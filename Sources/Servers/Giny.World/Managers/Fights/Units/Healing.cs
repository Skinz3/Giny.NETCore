using Giny.Core.DesignPattern;
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
        private SpellEffectHandler EffectHandler
        {
            get;
            set;
        }
        private EffectSchoolEnum EffectSchool
        {
            get;
            set;
        }
        public Healing(Fighter source, Fighter target, EffectSchoolEnum effectSchool, double baseMin, double baseMax, SpellEffectHandler effectHandler)
        {
            this.Source = source;
            this.Target = target;
            this.BaseMinHeal = baseMin;
            this.BaseMaxHeal = baseMax;
            this.EffectHandler = effectHandler;
            this.EffectSchool = effectSchool;
        }


     
        public void Compute()
        {
            if (Computed.HasValue)
            {
                return;
            }

            if (EffectSchool == EffectSchoolEnum.Fix)
            {
                Computed = new Jet(BaseMinHeal, BaseMaxHeal).Generate(Source.Random, Source.HasRandDownModifier(), Source.HasRandUpModifier());
                return;
            }

            Jet jet = EvaluateConcreteJet();

            jet.ComputeShapeEfficiencyModifiers(Target, EffectHandler);


            Target.Fight.Warn("Min :" + (int)jet.Min + " Max:" + (int)  jet.Max);

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

            switch (EffectSchool)
            {
                case EffectSchoolEnum.Earth:
                    result = jet * ((100d + Source.Stats.Strength.TotalInContext()) / 100d) + bonus;
                    break;
                case EffectSchoolEnum.Water:
                    result = jet * ((100d + Source.Stats.Chance.TotalInContext()) / 100d) + bonus;
                    break;
                case EffectSchoolEnum.Air:
                    result = jet * ((100d + Source.Stats.Agility.TotalInContext()) / 100d) + bonus;
                    break;
                case EffectSchoolEnum.Fire:
                    result = jet * ((100d + Source.Stats.Intelligence.TotalInContext()) / 100d) + bonus;
                    break;
                default:
                    throw new InvalidOperationException("Invalid healing effect school : " + EffectSchool);
            }

            return (int)result;
        }
        public Fighter GetSource()
        {
            return Source;
        }
    }
}
