using Giny.Protocol.Enums;
using Giny.World.Managers.Effects;
using Giny.World.Managers.Fights.Buffs.SpellBoost;
using Giny.World.Managers.Fights.Cast;
using Giny.World.Managers.Fights.Fighters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Giny.World.Managers.Fights.Effects.Debuffs
{
    [SpellEffectHandler(EffectsEnum.Effect_ReduceSpellMinimalRange)]
    public class ReduceSpellMinimalRange : SpellEffectHandler
    {
        public ReduceSpellMinimalRange(EffectDice effect, SpellCastHandler castHandler) : base(effect, castHandler)
        {

        }

        protected override void Apply(IEnumerable<Fighter> targets)
        {
            short spellId = (short)Effect.Min;
            short delta = (short)Effect.Value;

            foreach (var target in targets)
            {
                if (target.HasSpell(spellId))
                {
                    SpellReduceMinimalRangeBuff buff = new SpellReduceMinimalRangeBuff(target.BuffIdProvider.Pop(), spellId, delta
                        , target, this, (FightDispellableEnum)Effect.Dispellable);

                    target.AddBuff(buff);
                }
            }
        }
    }
}
