using Giny.Protocol.Enums;
using Giny.World.Managers.Effects;
using Giny.World.Managers.Fights.Cast;
using Giny.World.Managers.Fights.Fighters;
using Giny.World.Records.Maps;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Giny.World.Managers.Fights.Effects.Other
{
    [SpellEffectHandler(EffectsEnum.Effect_RemoveSpellEffects)]
    public class RemoveSpellEffects : SpellEffectHandler
    {
        public RemoveSpellEffects(EffectDice effect, SpellCastHandler castHandler) : base(effect,  castHandler)
        {

        }

        protected override void Apply(IEnumerable<Fighter> targets)
        {
            short spellId = (short)Effect.Value;

            foreach (var target in targets)
            {
                target.RemoveSpellEffects(Source, spellId);
            }
        }
    }
}
