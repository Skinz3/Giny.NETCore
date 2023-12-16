using Giny.Protocol.Enums;
using Giny.World.Managers.Effects;
using Giny.World.Managers.Fights.Buffs;
using Giny.World.Managers.Fights.Cast;
using Giny.World.Managers.Fights.Fighters;
using Giny.World.Managers.Fights.Triggers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Giny.World.Managers.Fights.Effects.Buffs
{
    [SpellEffectHandler(EffectsEnum.Effect_SpellImmunity)]
    public class SpellImmunity : SpellEffectHandler
    {
        public SpellImmunity(EffectDice effect, SpellCastHandler castHandler) : base(effect, castHandler)
        {

        }

        protected override void Apply(IEnumerable<Fighter> targets)
        {
            short spellId = (short)Effect.Min;

            foreach (var target in targets)
            {
                int id = target.BuffIdProvider.Pop();
                SpellImmunityBuff buff = new SpellImmunityBuff(id, target, this, Effect.DispellableEnum, spellId);
                target.AddBuff(buff);
            }
        }
    }
}
