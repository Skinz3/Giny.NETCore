using Giny.World.Managers.Fights.Cast;
using Giny.World.Managers.Fights.Fighters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Giny.World.Managers.Effects.Targets
{
    public class LastAttackerCriterion : TargetCriterion
    {
        public LastAttackerCriterion(bool required)
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
            // required ?
            return handler.Source.LastAttacker == actor;
        }
        public override string ToString()
        {
            return Required ? "LastAttacker" : "Not Last Attaker (Not handled)";
        }
    }
}
