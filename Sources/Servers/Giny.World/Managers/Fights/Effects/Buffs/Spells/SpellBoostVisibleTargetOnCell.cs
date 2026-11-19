using Giny.Protocol.Enums;
using Giny.World.Managers.Effects;
using Giny.World.Managers.Fights.Buffs;
using Giny.World.Managers.Fights.Buffs.SpellBoost;
using Giny.World.Managers.Fights.Cast;
using Giny.World.Managers.Fights.Fighters;
using Giny.World.Records.Maps;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Giny.World.Managers.Fights.Effects.Buffs.Spells
{
    /*
     * Forgelance (Désarmé) : le sort ne peut être lancé
     * que sur une case occupée par une cible visible.
     */
    [SpellEffectHandler(EffectsEnum.Effect_BoostVisibleTargetOnCellOn)]
    public class SpellBoostVisibleTargetOnCell : SpellEffectHandler
    {
        public SpellBoostVisibleTargetOnCell(EffectDice effect, SpellCastHandler castHandler) : base(effect, castHandler)
        {

        }

        protected override void Apply(IEnumerable<Fighter> targets)
        {
            short spellId = (short)Effect.Min;
            short delta = (short)Effect.Value;

            foreach (var target in targets)
            {
                int id = target.BuffIdProvider.Pop();
                Buff buff = new SpellBoostVisibleTargetBuff(id, spellId, delta, target, this, Effect.DispellableEnum);
                target.AddBuff(buff);
            }
        }
    }
}
