using Giny.Protocol.Custom.Enums;
using Giny.Protocol.Enums;
using Giny.World.Managers.Effects;
using Giny.World.Managers.Fights.Buffs;
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
    [SpellEffectHandler(EffectsEnum.Effect_GiveHpPercentWhenAttack)]
    public class GiveHpPercentWhenAttack : SpellEffectHandler
    {
        public GiveHpPercentWhenAttack(EffectDice effect, SpellCastHandler castHandler) : base(effect, castHandler)
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


            double delta = token.Computed.Value * (Effect.Min / 100d);

            var healTarget = token.GetSource();
            healTarget.Heal(new Healing(Source, healTarget, EffectSchoolEnum.Fix, delta, delta, this));

        }
    }
}
