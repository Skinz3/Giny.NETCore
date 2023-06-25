using Giny.World.Managers.Fights.Cast;
using Giny.World.Managers.Fights.Fighters;
using System.Linq;

namespace Giny.World.Managers.Effects.Targets
{
    public class JustSummonedCriterion : TargetCriterion
    {
        public override bool RefreshTargets => true;

        /// <summary>
        /// handler.CastHandler.Initialized because we CheckWhenExecute. We dont want to check it before a summoned fighter can be created.
        /// </summary>
        public override bool IsTargetValid(Fighter actor, SpellEffectHandler handler)
        {
            return actor.IsSummoned() && handler.CastHandler.Initialized && handler.CastHandler.GetEffectHandlers().Any(x => actor.GetSummoningEffect() == x);
        }
        public override string ToString()
        {
            return "Just Summoned";
        }
    }
}