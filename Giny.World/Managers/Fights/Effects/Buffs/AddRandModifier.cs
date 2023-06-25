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
    [SpellEffectHandler(EffectsEnum.Effect_RandDownModifier)]
    [SpellEffectHandler(EffectsEnum.Effect_RandUpModifier)]
    public class AddRandModifier : SpellEffectHandler
    {
        public AddRandModifier(EffectDice effect, SpellCastHandler castHandler) : base(effect, castHandler)
        {

        }

        protected override void Apply(IEnumerable<Fighter> targets)
        {
            foreach (var target in targets)
            {
                int buffId = target.BuffIdProvider.Pop();
                RandModifierBuff buff = new RandModifierBuff(buffId, IsRandUpModifier(),
                    target, this, FightDispellableEnum.DISPELLABLE);
                target.AddBuff(buff);

            }
        }
        private bool IsRandUpModifier()
        {
            switch (Effect.EffectEnum)
            {
                case EffectsEnum.Effect_RandUpModifier:
                    return true;
                case EffectsEnum.Effect_RandDownModifier:
                    return false;
            }

            throw new Exception("Not handled. (IsRandUpModifier())");
        }
    }
}
