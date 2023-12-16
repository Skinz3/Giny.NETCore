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

namespace Giny.World.Managers.Fights.Effects.Steal
{
    /*
     * Concentration de Chakra
     */
    [SpellEffectHandler(EffectsEnum.Effect_StealHpBestElement)]
    internal class StealHpBestElement : SpellEffectHandler
    {
        public StealHpBestElement(EffectDice effect, SpellCastHandler castHandler) : base(effect, castHandler)
        {

        }

        protected override void Apply(IEnumerable<Fighter> targets)
        {
            foreach (var target in targets)
            {
                DamageResult result = target.InflictDamage(CreateDamage(target));

                double healDelta = result.Total / 2d;

                Source.Heal(new Healing(Source, target, EffectElementEnum.None, healDelta, healDelta, this,true));
            }
        }

        private Damage CreateDamage(Fighter target)
        {
            return new Damage(Source, target, Source.Stats.GetBestElement(), Effect.Min, Effect.Max, this);
        }

    }
}
