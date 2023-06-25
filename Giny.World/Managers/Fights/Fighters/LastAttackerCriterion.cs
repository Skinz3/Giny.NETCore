using Giny.World.Managers.Effects.Targets;
using Giny.World.Managers.Fights.Cast;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Giny.World.Managers.Fights.Fighters
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
    }
}
