using Giny.Protocol.Enums;
using Giny.World.Managers.Effects;
using Giny.World.Managers.Fights.Cast;
using Giny.World.Managers.Fights.Fighters;
using Giny.World.Managers.Fights.Marks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Giny.World.Managers.Fights.Effects.Marks
{
    [SpellEffectHandler(EffectsEnum.Effect_DispelRunesOfTarget)]
    public class DispelRunesOfTarget : SpellEffectHandler
    {
        public DispelRunesOfTarget(EffectDice effect, SpellCastHandler castHandler) : base(effect, castHandler)
        {
        }

        protected override void Apply(IEnumerable<Fighter> targets)
        {
            foreach (var target in targets)
            {
                var runes = target.GetMarks<Rune>();

                foreach (var rune in runes.ToArray())
                {
                    Source.Fight.RemoveMark(rune);
                }

            }
        }
    }
}
