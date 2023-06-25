using Giny.Protocol.Custom.Enums;
using Giny.Protocol.Enums;
using Giny.World.Managers.Effects;
using Giny.World.Managers.Fights.Cast;
using Giny.World.Managers.Fights.Cast.Units;
using Giny.World.Managers.Fights.Fighters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Giny.World.Managers.Fights.Effects.Heals
{
    [SpellEffectHandler(EffectsEnum.Effect_GiveHPPercent)]
    public class GiveHpPercent : SpellEffectHandler
    {
        public GiveHpPercent(EffectDice effect, SpellCastHandler castHandler) : base(effect, castHandler)
        {

        }

        protected override void Apply(IEnumerable<Fighter> targets)
        {
            short delta = (short)(Source.Stats.LifePoints * (Effect.Min / 100d));

            Damage damage = new Damage(Source, Source, EffectSchoolEnum.Fix, delta, delta, this);
            damage.IgnoreShield = true;
            Source.InflictDamage(damage);

            foreach (var target in targets)
            {
                if (target != Source)
                {
                    target.Heal(new Healing(Source, target, delta));
                }
            }
        }
    }
}
