using Giny.World.Managers.Fights.Cast;
using Giny.World.Managers.Fights.Fighters;
using Org.BouncyCastle.Asn1.Ocsp;
using System.Linq;

namespace Giny.World.Managers.Effects.Targets
{
    public class JustSummonedCriterion : TargetCriterion
    {
        public override bool RefreshTargets => true;

        private bool Required
        {
            get;
            set;
        }
        public JustSummonedCriterion(bool required)
        {
            this.Required = required;
        }

        /// <summary>
        /// handler.CastHandler.Initialized because we CheckWhenExecute. We dont want to check it before a summoned fighter can be created.
        /// </summary>
        public override bool IsTargetValid(Fighter actor, SpellEffectHandler handler)
        {
            bool valid = actor.IsSummoned() && handler.CastHandler.Initialized && handler.CastHandler.GetEffectHandlers().Any(x => actor.GetSummoningEffect() == x);

            return Required ? valid : !valid;

        }
        public override string ToString()
        {
            return Required ? "Just Summoned" : "Not just summoned";
        }
    }
}