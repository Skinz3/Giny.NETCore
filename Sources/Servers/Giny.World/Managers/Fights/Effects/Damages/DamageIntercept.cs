using Giny.Core.DesignPattern;
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
    [WIP]
    [SpellEffectHandler(EffectsEnum.Effect_DamageIntercept)]
    public class DamageIntercept : SpellEffectHandler
    {
        public DamageIntercept(EffectDice effect, SpellCastHandler castHandler) : base(effect, castHandler)
        {
        }

        protected override void Apply(IEnumerable<Fighter> targets)
        {
            Damage damage = GetTriggerToken<Damage>();

            if (damage != null && Source != damage.Target)
            {
                Damage newDamages = new Damage(damage.Source, Source, damage.EffectSchool, damage.BaseMinDamages, damage.BaseMaxDamages, this);
                newDamages.Computed = damage.Computed;
                Source.InflictDamage(newDamages);

                damage.Computed = 0;
            }
            else
            {
                OnTokenMissing<Damage>();
            }
        }
    }
}
