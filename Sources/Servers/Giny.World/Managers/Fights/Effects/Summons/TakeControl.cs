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

namespace Giny.World.Managers.Fights.Effects.Summons
{
    [SpellEffectHandler(EffectsEnum.Effect_TakeControl)]
    public class TakeControl : SpellEffectHandler
    {
        public TakeControl(EffectDice effect, SpellCastHandler castHandler) : base(effect, castHandler)
        {

        }

        protected override void Apply(IEnumerable<Fighter> targets)
        {
            if (!(Source is CharacterFighter))
            {
                return;
            }

            foreach (var target in targets.OfType<SummonedFighter>())
            {
                int id = target.BuffIdProvider.Pop();
                TakeControlBuff buff = new TakeControlBuff(id, target, this, FightDispellableEnum.DISPELLABLE);
                target.AddBuff(buff);
            }
        }
    }
}
