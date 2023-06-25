using Giny.World.Managers.Fights.Cast;
using Giny.World.Managers.Fights.Fighters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Giny.World.Managers.Effects.Targets
{
    public class UnknownCriterion : TargetCriterion
    {
        public UnknownCriterion(string criterion)
        {
            Criterion = criterion;
        }

        public string Criterion
        {
            get;
            set;
        }

        public override bool IsTargetValid(Fighter actor, SpellEffectHandler handler)
        {
            return true;
        }

        public override string ToString()
        {
            return "Unknown";
        }
    }
}
