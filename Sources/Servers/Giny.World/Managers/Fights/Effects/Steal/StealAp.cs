using Giny.Protocol.Custom.Enums;
using Giny.Protocol.Enums;
using Giny.World.Managers.Effects;
using Giny.World.Managers.Fights.Cast;
using Giny.World.Managers.Fights.Fighters;
using Giny.World.Managers.Fights.Triggers;
using Giny.World.Managers.Fights.Units;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Giny.World.Managers.Fights.Effects.Steal
{
    [SpellEffectHandler(EffectsEnum.Effect_StealAPFix)]
    [SpellEffectHandler(EffectsEnum.Effect_StealAP_84)]
    public class StealAp : SpellEffectHandler
    {
        public StealAp(EffectDice effect, SpellCastHandler castHandler) : base(effect, castHandler)
        {
        }

        protected override void Apply(IEnumerable<Fighter> targets)
        {
            foreach (var target in targets)
            {
                short delta = 0;

                if (Effect.EffectEnum == EffectsEnum.Effect_StealAP_84)
                {
                    delta = RollAP(target, Effect.Min);

                    int dodged = Effect.Min - delta;
                    
                    if (dodged > 0)
                    {
                        target.OnDodge(Source, ActionsEnum.ACTION_FIGHT_SPELL_DODGED_PA, dodged);
                    }
                }
                else if (Effect.EffectEnum == EffectsEnum.Effect_StealAPFix)
                {
                    delta = (short)Effect.Min;
                }



                if (delta > 0)
                {
                    if (this.Effect.Duration > 1)
                    {
                        base.AddStatBuff(target, (short)-delta, target.Stats.ActionPoints, FightDispellableEnum.DISPELLABLE, (short)EffectsEnum.Effect_SubAP);
                        base.AddStatBuff(Source, (short)delta, Source.Stats.ActionPoints, FightDispellableEnum.DISPELLABLE, (short)EffectsEnum.Effect_AddAP_111);
                    }
                    else
                    {
                        target.LooseAp(Source, delta, ActionsEnum.ACTION_CHARACTER_ACTION_POINTS_LOST);
                        Source.GainAp(Source, delta);
                    }
                }


                target.TriggerBuffs(TriggerTypeEnum.OnApRemovalAttempt, null);
                Source.TriggerBuffs(TriggerTypeEnum.OnCasterRemoveApAttempt, null);
            }
        }
        private short RollAP(Fighter fighter, int maxValue)
        {
            short value = 0;

            for (var i = 0; i < maxValue && value < fighter.Stats.ActionPoints.TotalInContext(); i++)
            {
                if (fighter.RollAPLose(Source, value))
                {
                    value++;
                }
            }

            return value;
        }
    }
}
