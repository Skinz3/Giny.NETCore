using Giny.Protocol.Custom.Enums;
using Giny.Protocol.Enums;
using Giny.World.Managers.Effects;
using Giny.World.Managers.Fights.Cast;
using Giny.World.Managers.Fights.Fighters;
using Giny.World.Managers.Fights.Triggers;
using Giny.World.Managers.Fights.Units;
using Giny.World.Records.Maps;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Giny.World.Managers.Fights.Effects.Debuffs
{
    [SpellEffectHandler(EffectsEnum.Effect_SubMP)]
    public class MpDebuff : SpellEffectHandler
    {
        public MpDebuff(EffectDice effect, SpellCastHandler castHandler) :
            base(effect, castHandler)
        {

        }

        protected override void Apply(IEnumerable<Fighter> targets)
        {
            foreach (var target in targets)
            {
                if (this.Effect.Duration > 0)
                {
                    base.AddStatBuff(target, (short)-Effect.Min, target.Stats.MovementPoints, Effect.DispellableEnum);
                }
                else
                {
                    target.LooseMp(Source, (short)Effect.Min, ActionsEnum.ACTION_CHARACTER_MOVEMENT_POINTS_LOST);
                }
                target.TriggerBuffs(TriggerTypeEnum.OnMpRemovalAttempt, null);

                Source.TriggerBuffs(TriggerTypeEnum.OnCasterRemoveMpAttempt, null);

            }
        }
    }
}
