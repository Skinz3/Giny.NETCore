using Giny.Protocol.Enums;
using Giny.World.Managers.Effects;
using Giny.World.Managers.Fights.Cast;
using Giny.World.Managers.Fights.Fighters;
using Giny.World.Managers.Fights.Marks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Giny.World.Managers.Fights.Effects.Marks
{
    [SpellEffectHandler(EffectsEnum.Effect_DispelTrapsOfTarget)]
    public class DispelTrapsOfTarget : SpellEffectHandler
    {
        public DispelTrapsOfTarget(EffectDice effect, SpellCastHandler castHandler) : base(effect, castHandler)
        {
        }

        protected override void Apply(IEnumerable<Fighter> targets)
        {
            foreach (var target in targets)
            {
                var traps = target.GetMarks<Trap>();

                foreach (var trap in traps.ToArray())
                {
                    Source.Fight.RemoveMark(trap);
                }

            }
        }
    }
}
