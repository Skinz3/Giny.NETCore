using Giny.World.Managers.Fights.Cast;
using Giny.World.Managers.Fights.Fighters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Giny.World.Managers.Effects.Targets
{
    public class LifeCriterion : TargetCriterion
    {
        public LifeCriterion(int lifePercent, bool mustBeGreater)
        {
            LifePercent = lifePercent;
            MustBeGreater = mustBeGreater;
        }

        public int LifePercent
        {
            get;
            set;
        }

        public bool MustBeGreater
        {
            get;
            set;
        }

        public override bool IsDisjonction => false;

        public override bool IsTargetValid(Fighter actor, SpellEffectHandler handler)
        {
            bool result = MustBeGreater ? actor.Stats.LifePercentage >= LifePercent :
                actor.Stats.LifePercentage < LifePercent;

            return result;
        }
        public override string ToString()
        {
            if (MustBeGreater)
            {
                return "LifePercent > (" + LifePercent + "%)";
            }
            else
            {
                return "LifePercent <= (" + LifePercent + "%)";
            }
        }
    }
}
