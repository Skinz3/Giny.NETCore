using Giny.World.Managers.Fights.Cast;
using Giny.World.Managers.Fights.Fighters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Giny.World.Managers.Effects.Targets
{
    public class SummonerCriterion : TargetCriterion
    {
        public SummonerCriterion(bool required)
        {
            Required = required;
        }

        public bool Required
        {
            get;
            set;
        }

        public override bool IsTargetValid(Fighter actor, SpellEffectHandler handler)
        {
            bool result = Required == (actor == handler.Source ||
                (actor.GetSummoner() != null && (actor.GetSummoner() == handler.Source || actor.GetSummoner() == handler.Source.GetSummoner())) ||
                handler.Source.GetSummoner() != null && handler.Source.GetSummoner() == actor);

            return result;
        }

        public override string ToString()
        {
            return "Summoner";
        }
    }
}
