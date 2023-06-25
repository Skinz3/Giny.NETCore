using Giny.Protocol.Enums;
using Giny.World.Managers.Actions;
using Giny.World.Managers.Effects;
using Giny.World.Managers.Fights.Cast;
using Giny.World.Managers.Fights.Fighters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Giny.World.Managers.Fights.Effects.Debuffs
{
    [SpellEffectHandler(EffectsEnum.Effect_ReduceEffectsDuration)]
    public class ReduceEffectsDuration : SpellEffectHandler
    {
        public ReduceEffectsDuration(EffectDice effect, SpellCastHandler castHandler) : base(effect, castHandler)
        {
        }

        protected override void Apply(IEnumerable<Fighter> targets)
        {
            short delta = (short)Effect.Min;

            foreach (var target in targets)
            {
                foreach (var buff in target.GetBuffs().Where(buff => buff.Dispellable == FightDispellableEnum.DISPELLABLE).Where(buff => buff.Duration > 0 && !buff.HasDelay()).ToArray())
                {
                    buff.Duration -= delta;

                    if (buff.Duration <= 0)
                        target.RemoveAndDispellBuff(Source, buff);
                }

                target.OnEffectDurationReduced(Source, Effect.EffectId, delta);
            }


        }
    }
}
