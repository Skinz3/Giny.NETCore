using Giny.World.Managers.Fights.Cast;
using Giny.World.Managers.Fights.Fighters;
using Giny.World.Managers.Fights.Units;
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
            var source = GetLastAttackerSource(handler);
            return source.LastAttacker == actor;
        }

        public static Fighter GetLastAttackerSource(SpellEffectHandler handler)
        {
            var lastAtkSource = handler.Source;

            if (handler.CastHandler.Cast.Token != null)
            {
                if (handler.CastHandler.Cast.Token is Damage)
                {
                    var damages = handler.CastHandler.Cast.Token as Damage;

                    lastAtkSource = damages.Target;

                }
            }

            return lastAtkSource;
        }
        public override string ToString()
        {
            return Required ? "LastAttacker" : "Not Last Attaker (Not handled)";
        }
    }
}
