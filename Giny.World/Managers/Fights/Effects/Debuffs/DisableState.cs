using Giny.Protocol.Enums;
using Giny.World.Managers.Actions;
using Giny.World.Managers.Effects;
using Giny.World.Managers.Fights.Buffs;
using Giny.World.Managers.Fights.Cast;
using Giny.World.Managers.Fights.Fighters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Giny.World.Managers.Fights.Effects.Debuffs
{
    [SpellEffectHandler(EffectsEnum.Effect_DispelState)]
    [SpellEffectHandler(EffectsEnum.Effect_DisableState)]
    public class DisableState : SpellEffectHandler
    {
        public DisableState(EffectDice effect, SpellCastHandler castHandler) : base(effect, castHandler)
        {
        }

        protected override void Apply(IEnumerable<Fighter> targets)
        {
            short stateId = (short)Effect.Value;

            foreach (var target in targets)
            {
                if (Effect.Duration == -1 || Effect.EffectEnum == EffectsEnum.Effect_DispelState)
                {
                    target.DispelState(Source, stateId);
                }
                else
                {
                    foreach (var oldBuff in target.GetBuffs<DisableStateBuff>().Where(x => x.StateId == stateId).ToArray())
                    {
                        target.RemoveAndDispellBuff(Source, oldBuff);
                    }

                    int id = target.BuffIdProvider.Pop();
                    Buff buff = new DisableStateBuff(id, stateId, target, this, FightDispellableEnum.DISPELLABLE,
                        (short)ActionsEnum.ACTION_FIGHT_DISABLE_STATE);
                    target.AddBuff(buff);
                }
            }
        }
    }
}
