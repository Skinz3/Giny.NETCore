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

namespace Giny.World.Managers.Fights.Effects.Debuffs
{
    [SpellEffectHandler(EffectsEnum.Effect_ReduceFinalDamages)]
    public class ReduceFinalDamages : SpellEffectHandler
    {
        public ReduceFinalDamages(EffectDice effect, SpellCastHandler castHandler) : base(effect, castHandler)
        {

        }

        protected override void Apply(IEnumerable<Fighter> targets)
        {
            foreach (var target in targets)
            {
                int id = target.BuffIdProvider.Pop();
                FinalDamageBuff buff = new FinalDamageBuff(id, (short)(-Effect.Min), target, this, FightDispellableEnum.DISPELLABLE);
                target.AddBuff(buff);
            }
        }
    }
}
