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
    [SpellEffectHandler(EffectsEnum.Effect_StealKamas)]
    public class StealKamas : SpellEffectHandler
    {
        public StealKamas(EffectDice effect, SpellCastHandler castHandler) : base(effect, castHandler)
        {

        }

        protected override void Apply(IEnumerable<Fighter> targets)
        {
            if (Source is CharacterFighter source)
            {
                var effect = Effect.Generate(Source.Random);

                source.Character.AddKamas(effect.Value);
                source.Character.OnKamasGained(effect.Value);
            }
            else
            {
                foreach (var target in targets.OfType<CharacterFighter>())
                {
                    if (target.Character.RemoveKamas(Effect.Value))
                    {
                        target.Character.OnKamasLost(Effect.Value);
                    }
                }
            }
           
        }
    }
}
