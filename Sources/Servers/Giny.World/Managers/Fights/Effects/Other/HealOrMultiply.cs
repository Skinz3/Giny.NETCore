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

namespace Giny.World.Managers.Fights.Effects.Other
{
    [SpellEffectHandler(EffectsEnum.Effect_HealOrMultiply)]
    public class HealOrMultiply : SpellEffectHandler
    {
        public HealOrMultiply(EffectDice effect, SpellCastHandler castHandler) : base(effect, castHandler)
        {
        }

        protected override void Apply(IEnumerable<Fighter> targets)
        {
            Damage token = GetTriggerToken<Damage>();

            if (token != null)
            {
                bool heal = Source.Random.Next(2) == 0 ? true : false;

                if (heal)
                {
                    token.Target.Heal(new Healing(token.Source, token.Target, token.Element, token.Computed.Value,
                        token.Computed.Value, null, true));
                    token.Computed = 0;
                }
                else
                {
                    token.Computed *= 2;
                }

            }
        }
    }
}
