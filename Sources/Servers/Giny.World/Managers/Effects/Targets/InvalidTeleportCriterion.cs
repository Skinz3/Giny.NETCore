using Giny.World.Managers.Fights.Cast;
using Giny.World.Managers.Fights.Fighters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Giny.World.Managers.Effects.Targets
{
    /// <summary>
    /// SpellManager.as:352
    /// </summary>
    public class InvalidTeleportCriterion : TargetCriterion
    {
        public override bool RefreshTargets => true;

        public override bool IsTargetValid(Fighter actor, SpellEffectHandler handler)
        {
            return actor.WasTeleportedInInvalidCell;
        }
        public override string ToString()
        {
            return "Invalid Teleport";
        }
    }
}
