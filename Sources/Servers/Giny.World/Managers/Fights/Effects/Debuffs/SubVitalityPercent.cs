using Giny.Core.DesignPattern;
using Giny.Protocol.Enums;
using Giny.World.Managers.Actions;
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
    [SpellEffectHandler(EffectsEnum.Effect_SubVitalityPercent_1048)]
    [SpellEffectHandler(EffectsEnum.Effect_SubVitalityPercent_2845)]
    public class SubVitalityPercent : SpellEffectHandler
    {
        /// <summary>
        /// Plus clair d'afficher le nombre de PV perdu
        /// </summary>
        private const ActionsEnum ActionId = ActionsEnum.ACTION_CHARACTER_DEBOOST_VITALITY;// ActionsEnum.ACTION_CHARACTER_LIFE_POINTS_MALUS_PERCENT;

        public SubVitalityPercent(EffectDice effect, SpellCastHandler castHandler) : base(effect, castHandler)
        {
        }

        protected override void Apply(IEnumerable<Fighter> targets)
        {
            foreach (var target in targets)
            {
                short delta = 0;

                if (Effect.EffectEnum == EffectsEnum.Effect_SubVitalityPercent_1048)
                {
                    delta = (short)(-target.Stats.LifePoints * (double)Effect.Min / 100.0d);
                }
                else if (Effect.EffectEnum == EffectsEnum.Effect_SubVitalityPercent_2845)
                {
                    delta = (short)(-target.Stats.MaxLifePoints * (double)Effect.Min / 100.0d);
                }

                VitalityDebuff buff = new VitalityDebuff(target.BuffIdProvider.Pop(), delta, target, this,
                   (FightDispellableEnum)Effect.Dispellable, ActionId);

                target.AddBuff(buff);
            }
        }
    }
}
