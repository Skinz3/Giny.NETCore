using Giny.Protocol.Enums;
using Giny.World.Managers.Actions;
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
    [SpellEffectHandler(EffectsEnum.Effect_AddVitalityPercent)]
    public class AddVitalityPercent : SpellEffectHandler
    {
        private const ActionsEnum ActionId = ActionsEnum.ACTION_CHARACTER_BOOST_VITALITY;

        public AddVitalityPercent(EffectDice effect, SpellCastHandler castHandler) :
            base(effect, castHandler)
        {
        }

        protected override void Apply(IEnumerable<Fighter> targets)
        {
            foreach (var target in targets)
            {
                short delta = (short)(target.Stats.MaxLifePoints * (double)Effect.Min / 100.0d);
                this.AddVitalityBuff(target, delta, FightDispellableEnum.DISPELLABLE, ActionId);
            }
        }
    }
}
