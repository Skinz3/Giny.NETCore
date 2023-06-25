using Giny.Protocol.Enums;
using Giny.World.Managers.Effects;
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
    [SpellEffectHandler(EffectsEnum.Effect_AddMP_128)]
    [SpellEffectHandler(EffectsEnum.Effect_AddMP)]
    public class MpBuff : SpellEffectHandler
    {
        public MpBuff(EffectDice effect, SpellCastHandler castHandler) : base(effect, castHandler)
        {
        }

        protected override void Apply(IEnumerable<Fighter> targets)
        {
            foreach (var target in targets)
            {
                if (this.Effect.Duration != 0)
                {
                    base.AddStatBuff(target, (short)Effect.Min, target.Stats.MovementPoints, FightDispellableEnum.DISPELLABLE);
                }
                else
                {
                    target.GainMp(Source, (short)Effect.Min);
                }
            }
        }
    }
}
