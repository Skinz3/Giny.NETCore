using Giny.Core.DesignPattern;
using Giny.World.Managers.Fights.Cast;
using Giny.World.Managers.Fights.Fighters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Giny.World.Managers.Effects.Targets
{
    public class ThroughPortalCriterion : TargetCriterion
    {
        private bool Required
        {
            get;
            set;
        }
        public ThroughPortalCriterion(bool required)
        {
            this.Required = required;
        }

        [WIP("todo")]
        public override bool IsTargetValid(Fighter actor, SpellEffectHandler handler)
        {
            return true;
        }
        public override string ToString()
        {
            return "Through Portal";
        }
    }
}
