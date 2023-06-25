using Giny.Protocol.Enums;
using Giny.World.Managers.Actions;
using Giny.World.Managers.Effects;
using Giny.World.Managers.Fights.Cast;
using Giny.World.Managers.Fights.Fighters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Giny.World.Managers.Fights.Effects.Steal
{
    [SpellEffectHandler(EffectsEnum.Effect_StealMP_77)]
    public class StealMp : SpellEffectHandler
    {
        public StealMp(EffectDice effect, SpellCastHandler castHandler) : base(effect, castHandler)
        {
        }

        protected override void Apply(IEnumerable<Fighter> targets)
        {
            foreach (var target in targets)
            {
                short delta = RollMP(target, Effect.Min);

                int dodged = Effect.Min - delta;

                if (dodged > 0)
                {
                    target.OnDodge(Source, ActionsEnum.ACTION_FIGHT_SPELL_DODGED_PM, dodged);
                }

                if (this.Effect.Duration > 1 && Effect.EffectEnum != EffectsEnum.Effect_LostMP)
                {
                    base.AddStatBuff(target, (short)-delta, target.Stats.MovementPoints, FightDispellableEnum.DISPELLABLE, (short)EffectsEnum.Effect_SubMP);
                    base.AddStatBuff(target, (short)delta, target.Stats.MovementPoints, FightDispellableEnum.DISPELLABLE, (short)EffectsEnum.Effect_AddMP_128);
                }
                else
                {
                    target.LooseMp(Source, (short)delta, ActionsEnum.ACTION_CHARACTER_MOVEMENT_POINTS_LOST);
                    Source.GainMp(Source, delta);
                }
            }
        }
        private short RollMP(Fighter fighter, int maxValue)
        {
            short value = 0;

            for (var i = 0; i < maxValue && value < fighter.Stats.MovementPoints.TotalInContext(); i++)
            {
                if (fighter.RollMPLose(Source, value))
                {
                    value++;
                }
            }

            return value;
        }
    }
}
