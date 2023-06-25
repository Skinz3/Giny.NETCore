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

namespace Giny.World.Managers.Fights.Effects.Damages
{
    [SpellEffectHandler(EffectsEnum.Effect_DamageSharing)]
    public class DamageSharing : SpellEffectHandler
    {
        public DamageSharing(EffectDice effect, SpellCastHandler castHandler) : base(effect, castHandler)
        {

        }

        protected override void Apply(IEnumerable<Fighter> targets)
        {
            Damage damage = GetTriggerToken<Damage>();

            if (damage != null)
            {
                IEnumerable<Fighter> fighters = Source.Team.GetFighters<Fighter>().Where(x => x.HasDamageSharingBuff(this));

                short sharedDelta = (short)(damage.Computed.Value / fighters.Count());

                foreach (var ally in Source.Team.GetFighters<Fighter>())
                {
                    Damage sharedDamage = new Damage(damage.Source, ally, damage.EffectSchool, 0, 0,
                        damage.GetEffectHandler());

                    sharedDamage.Computed = sharedDelta;
                    sharedDamage.IgnoreBoost = true;
                    sharedDamage.IgnoreResistances = true;
                    sharedDamage.WontTriggerBuffs = true;
                    ally.InflictDamage(sharedDamage);
                }

                damage.Computed = 0;
            }
            else
            {
                OnTokenMissing<Damage>();
            }
        }
    }
}
