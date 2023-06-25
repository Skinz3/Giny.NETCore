using Giny.Protocol.Enums;
using Giny.World.Managers.Effects;
using Giny.World.Managers.Fights.Buffs;
using Giny.World.Managers.Fights.Cast;
using Giny.World.Managers.Fights.Fighters;
using Giny.World.Records.Maps;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Giny.World.Managers.Fights.Effects.Buffs
{
    [SpellEffectHandler(EffectsEnum.Effect_AddShieldPercentLevel)]
    public class AddShieldLevel : SpellEffectHandler
    {
        public AddShieldLevel(EffectDice effect, SpellCastHandler castHandler) :
            base(effect, castHandler)
        {

        }

        protected override void Apply(IEnumerable<Fighter> targets)
        {
            double delta = Source.Level * (Effect.Min / 100d);
            foreach (var target in targets)
            {
                int id = target.BuffIdProvider.Pop();
                ShieldBuff buff = new ShieldBuff(id, (short)delta, target, this, FightDispellableEnum.DISPELLABLE);
                target.AddBuff(buff);
            }
        }
    }
}
