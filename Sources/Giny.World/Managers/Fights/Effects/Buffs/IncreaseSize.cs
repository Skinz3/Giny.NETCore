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
    [SpellEffectHandler(EffectsEnum.Effect_IncreaseSize_2868)]
    [SpellEffectHandler(EffectsEnum.Effect_IncreaseSize)]
    public class IncreaseSize : SpellEffectHandler
    {
        public IncreaseSize(EffectDice effect, SpellCastHandler castHandler) : base(effect, castHandler)
        {
        }

        protected override void Apply(IEnumerable<Fighter> targets)
        {
            foreach (var target in targets)
            {
                int buffId = target.BuffIdProvider.Pop();
                RescaleSkinBuff buff = new RescaleSkinBuff(buffId, (Effect.Min / 100d),
                    target, this, FightDispellableEnum.DISPELLABLE);
                target.AddBuff(buff);
            }
        }
    }
}
