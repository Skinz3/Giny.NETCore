using Giny.World.Managers.Fights.Cast;
using Giny.World.Managers.Fights.Fighters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Giny.World.Managers.Effects.Targets
{
    public class CanSummonCriterion : TargetCriterion
    {
        private bool Required
        {
            get;
            set;
        }
        public bool Caster
        {
            get;
            private set;
        }
        public CanSummonCriterion(bool caster, bool required)
        {
            this.Caster = caster;
            this.Required = required;
        }

        public override bool IsTargetValid(Fighter actor, SpellEffectHandler handler)
        {
            Fighter target = actor;

            if (Caster)
            {
                target = handler.Source;
            }

            bool flag = target.CanSummon();

            if (!Required)
            {
                return !flag;
            }
            else
            {
                return flag;
            }
        }

        public override string ToString()
        {
            return "Can Summon";
        }
    }
}
