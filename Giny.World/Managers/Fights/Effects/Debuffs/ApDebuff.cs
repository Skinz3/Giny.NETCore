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

namespace Giny.World.Managers.Fights.Effects.Debuffs
{
    [SpellEffectHandler(EffectsEnum.Effect_LostAP)]
    [SpellEffectHandler(EffectsEnum.Effect_SubAP)]
    public class ApDebuff : SpellEffectHandler
    {
        public ApDebuff(EffectDice effect, SpellCastHandler castHandler) :
            base(effect, castHandler)
        {
        }

        protected override void Apply(IEnumerable<Fighter> targets)
        {
            foreach (var target in targets)
            {
                if (this.Effect.Duration > 0 && Effect.EffectEnum != EffectsEnum.Effect_LostAP)
                {
                    base.AddStatBuff(target, (short)-Effect.Min, target.Stats.ActionPoints, FightDispellableEnum.DISPELLABLE);
                }
                else
                {
                    target.LooseAp(Source, (short)Effect.Min, Actions.ActionsEnum.ACTION_CHARACTER_ACTION_POINTS_LOST);
                }
            }
        }
    }
}
