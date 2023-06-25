using Giny.Protocol.Enums;
using Giny.World.Managers.Effects;
using Giny.World.Managers.Fights.Cast;
using Giny.World.Managers.Fights.Fighters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Giny.World.Managers.Fights.Effects.Other
{
    [SpellEffectHandler(EffectsEnum.Effect_ReduceSpellCooldown)]
    public class AddSpellCooldown : SpellEffectHandler
    {
        public AddSpellCooldown(EffectDice effect, SpellCastHandler castHandler) : base(effect, castHandler)
        {
        }

        protected override void Apply(IEnumerable<Fighter> targets)
        {
            short spellId = (short)Effect.Min;
            short reduceDelta = (short)Effect.Value;

            foreach (var target in targets)
            {
                if (target.HasSpell(spellId))
                {
                    target.ReduceSpellCooldown(Source, spellId, reduceDelta);
                }
            }
        }
    }
}
