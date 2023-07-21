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
    [SpellEffectHandler(EffectsEnum.Effect_CasterSplashHeal)]
    public class CasterSplashHeal : SpellEffectHandler
    {
        public CasterSplashHeal(EffectDice effect, SpellCastHandler castHandler) : base(effect, castHandler)
        {
        }

        protected override void Apply(IEnumerable<Fighter> targets)
        {
            var token = GetTriggerToken<Damage>();

            if (token == null || !token.Computed.HasValue)
            {
                OnTokenMissing<Damage>();
                return;
            }

            foreach (var target in targets)
            {
                double delta = token.Computed.Value * (Effect.Min / 100d);
                target.Heal(new Healing(Source, target, EffectSchoolEnum.Fix, delta, delta, this));
            }
        }
    }
}
