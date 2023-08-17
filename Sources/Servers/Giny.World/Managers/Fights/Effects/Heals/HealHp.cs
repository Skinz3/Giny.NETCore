using Giny.Core.DesignPattern;
using Giny.Protocol.Custom.Enums;
using Giny.Protocol.Enums;
using Giny.World.Managers.Effects;
using Giny.World.Managers.Fights.Cast;
using Giny.World.Managers.Fights.Fighters;
using Giny.World.Managers.Fights.Units;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Giny.World.Managers.Fights.Effects.Heals
{
    [Annotation("heal zone efficiency (éni)")] // Verifier le calcul pour tout ces effets
    [SpellEffectHandler(EffectsEnum.Effect_HealHP_143)]
    [SpellEffectHandler(EffectsEnum.Effect_HealHP_81)]
    [SpellEffectHandler(EffectsEnum.Effect_HealHP_108)]
    public class HealHp : SpellEffectHandler
    {
        public HealHp(EffectDice effect, SpellCastHandler castHandler) : base(effect, castHandler)
        {

        }

        protected override void Apply(IEnumerable<Fighter> targets)
        {
            foreach (var target in targets)
            {
                target.Heal(new Healing(Source, target, EffectSchoolEnum.Fire, Effect.Min, Effect.Max, this));
            }
        }
    }
}
