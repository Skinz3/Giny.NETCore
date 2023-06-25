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

namespace Giny.World.Managers.Fights.Effects.Buffs
{
    [SpellEffectHandler(EffectsEnum.Effect_AddVitality)]
    public class AddVitality : SpellEffectHandler
    {
        private const ActionsEnum ActionId = ActionsEnum.ACTION_CHARACTER_BOOST_VITALITY;

        public AddVitality(EffectDice effect, SpellCastHandler castHandler) :
            base(effect, castHandler)
        {
        }

        protected override void Apply(IEnumerable<Fighter> targets)
        {
            foreach (var target in targets)
            {
                short delta = (short)Effect.Min;
                this.AddVitalityBuff(target, delta, FightDispellableEnum.DISPELLABLE, ActionId);
            }
        }
    }
}
