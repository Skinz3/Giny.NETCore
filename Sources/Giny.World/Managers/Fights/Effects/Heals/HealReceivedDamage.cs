using Giny.Protocol.Enums;
using Giny.World.Managers.Effects;
using Giny.World.Managers.Fights.Cast;
using Giny.World.Managers.Fights.Cast.Units;
using Giny.World.Managers.Fights.Effects.Damages;
using Giny.World.Managers.Fights.Fighters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Giny.World.Managers.Fights.Effects.Heals
{
    /*
     * Mot interdit
     * Prygen
     */
    [SpellEffectHandler(EffectsEnum.Effect_HealReceivedDamages)]
    public class HealReceivedDamage : SpellEffectHandler
    {
        public HealReceivedDamage(EffectDice effect, SpellCastHandler castHandler) : base(effect, castHandler)
        {
        }

        protected override void Apply(IEnumerable<Fighter> targets)
        {
            Damage damage = GetTriggerToken<Damage>();

            if (damage != null)
            {
                damage.Applied += delegate (DamageResult result)
                {
                    foreach (var target in targets)
                    {
                        target.Heal(new Healing(Source, target, (short)result.Total));
                    }
                };
            }
            else
            {
                foreach (var target in targets)
                {
                    short delta = (short)(Source.TotalDamageReceivedSequenced * (Effect.Min / 100d));
                    target.Heal(new Healing(Source, target, delta));
                }
            }


        }

        private void OnDamageApplied(short obj)
        {
            throw new NotImplementedException();
        }
    }

}
