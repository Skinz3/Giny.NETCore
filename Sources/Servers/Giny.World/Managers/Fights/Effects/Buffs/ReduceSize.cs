using Giny.Protocol.Enums;
using Giny.World.Managers.Effects;
using Giny.World.Managers.Fights.Buffs;
using Giny.World.Managers.Fights.Cast;
using Giny.World.Managers.Fights.Fighters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Giny.World.Managers.Fights.Effects.Buffs
{
    [SpellEffectHandler(EffectsEnum.Effect_ReduceSize)]
    public class ReduceSize : SpellEffectHandler
    {
        public ReduceSize(EffectDice effect, SpellCastHandler castHandler) : base(effect, castHandler)
        {
        }

        protected override void Apply(IEnumerable<Fighter> targets)
        {
            foreach (var target in targets)
            {
                int buffId = target.BuffIdProvider.Pop();
                RescaleSkinBuff buff = new RescaleSkinBuff(buffId, (-Effect.Min / 100d),
                    target, this, Effect.DispellableEnum);
                target.AddBuff(buff);
            }
        }
    }
}
