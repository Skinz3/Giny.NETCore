using Giny.World.Managers.Fights.Cast;
using Giny.World.Managers.Fights.Fighters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Giny.World.Managers.Effects.Targets
{
    public class CarriedCriterion : TargetCriterion
    {
        public CarriedCriterion()
        {

        }

        public override bool IsTargetValid(Fighter actor, SpellEffectHandler handler)
        {
            return actor.IsCarried();
        }
        public override string ToString()
        {
            return "Is Carried";
        }
    }
}
