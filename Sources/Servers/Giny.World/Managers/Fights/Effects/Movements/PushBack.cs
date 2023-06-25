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

namespace Giny.World.Managers.Fights.Effects.Movements
{
    [SpellEffectHandler(EffectsEnum.Effect_PushBack)]
    [SpellEffectHandler(EffectsEnum.Effect_PushBack_1103)]
    public class PushBack : SpellEffectHandler
    {
        public PushBack(EffectDice effect, SpellCastHandler castHandler) : base(effect, castHandler)
        {

        }

        protected override void Apply(IEnumerable<Fighter> targets)
        {
            foreach (var target in targets)
            {
                target.PushBack(Source, CastCell, (short)Effect.Min, TargetCell);
            }
        }
    }
}
