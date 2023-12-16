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
    [SpellEffectHandler(EffectsEnum.Effect_RestoreHPPercent)]
    public class HealHpPercent : SpellEffectHandler
    {
        public HealHpPercent(EffectDice effect, SpellCastHandler castHandler) : base(effect, castHandler)
        {
        }

        protected override void Apply(IEnumerable<Fighter> targets)
        {
            foreach (var target in targets)
            {
                double delta = target.Stats.MaxLifePoints * (Effect.Min / 100d);
                target.Heal(new Healing(Source, target, EffectElementEnum.None, delta, delta, this, true));
            }
        }
    }
}
