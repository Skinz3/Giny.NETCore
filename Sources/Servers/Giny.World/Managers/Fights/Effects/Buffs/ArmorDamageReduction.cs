using Giny.Core.DesignPattern;
using Giny.Protocol.Enums;
using Giny.World.Managers.Effects;
using Giny.World.Managers.Fights.Buffs;
using Giny.World.Managers.Fights.Cast;
using Giny.World.Managers.Fights.Cast.Units;
using Giny.World.Managers.Fights.Fighters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Giny.World.Managers.Fights.Effects.Buffs
{
    [SpellEffectHandler(EffectsEnum.Effect_AddGlobalDamageReduction_105)]
    [SpellEffectHandler(EffectsEnum.Effect_ArmorDamageReduction)]
    public class ArmorDamageReduction : SpellEffectHandler
    {
        public ArmorDamageReduction(EffectDice effect, SpellCastHandler castHandler) : base(effect, castHandler)
        {
        }

        [WIP]
        protected override void Apply(IEnumerable<Fighter> targets)
        {
            Damage damage = GetTriggerToken<Damage>();

            if (damage != null)
            {
                int reduction = damage.Target.CalculateArmorValue((short)Effect.Min);
                int dmgReduction = Math.Min(damage.Computed.Value, reduction);
                damage.Computed = (short)(damage.Computed.Value - dmgReduction);
                damage.Target.OnDamageReduced(damage, dmgReduction);
            }
            else
            {
                foreach (var target in targets)
                {
                    int id = target.BuffIdProvider.Pop();
                    Buff buff = new GlobalDamageReductionBuff(id,
                       (short)Effect.Min,  target, this, FightDispellableEnum.DISPELLABLE);
                    target.AddBuff(buff);
                }
            }
        }
    }
}
