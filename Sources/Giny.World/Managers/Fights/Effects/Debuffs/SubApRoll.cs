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

namespace Giny.World.Managers.Fights.Effects.Debuffs
{
    [SpellEffectHandler(EffectsEnum.Effect_SubAP_Roll)]
    public class SubApRoll : SpellEffectHandler
    {
        public SubApRoll(EffectDice effect, SpellCastHandler castHandler) : base(effect, castHandler)
        {

        }

        protected override void Apply(IEnumerable<Fighter> targets)
        {
            foreach (var target in targets)
            {
                short delta = RollAP(target, Effect.Min);

                int dodged = Effect.Min - delta;

                if (dodged > 0)
                {
                    target.OnDodge(Source, ActionsEnum.ACTION_FIGHT_SPELL_DODGED_PA, dodged);
                }

                if (this.Effect.Duration > 1)
                {
                    base.AddStatBuff(target, (short)-delta, target.Stats.ActionPoints, FightDispellableEnum.DISPELLABLE, (short)EffectsEnum.Effect_SubAP);
                }
                else
                {
                    target.LooseAp(Source, (short)delta, ActionsEnum.ACTION_CHARACTER_ACTION_POINTS_LOST);
                }
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
