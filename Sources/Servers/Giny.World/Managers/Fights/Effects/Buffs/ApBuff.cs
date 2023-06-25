using Giny.Core.DesignPattern;
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
    [SpellEffectHandler(EffectsEnum.Effect_AddAP_111)]
    [SpellEffectHandler(EffectsEnum.Effect_RegainAP)]
    public class ApBuff : SpellEffectHandler
    {
        public ApBuff(EffectDice effect, SpellCastHandler castHandler) : base(effect, castHandler)
        {
        }

        protected override void Apply(IEnumerable<Fighter> targets)
        {
            foreach (var target in targets)
            {
                if (this.Effect.Duration != 0)
                {
                    base.AddStatBuff(target, (short)Effect.Min, target.Stats.ActionPoints, FightDispellableEnum.DISPELLABLE);
                }
                else
                {
                    target.GainAp(Source, (short)Effect.Min);
                }
            }
        }
    }
}
