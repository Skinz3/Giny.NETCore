using Giny.Protocol.Enums;
using Giny.World.Managers.Effects;
using Giny.World.Managers.Fights.Cast;
using Giny.World.Managers.Fights.Fighters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Giny.World.Managers.Fights.Effects.Cast
{
    /*
     * Dofus Ebène
     */
    [SpellEffectHandler(EffectsEnum.Effect_SourceExecuteSpellOnTarget)]
    public class SourceExecuteSpellOnTarget : SpellEffectHandler
    {
        public SourceExecuteSpellOnTarget(EffectDice effect, SpellCastHandler castHandler) : base(effect, castHandler)
        {

        }

        protected override void Apply(IEnumerable<Fighter> targets)
        {
            Spell spell = CreateCastedSpell();

            var caster = CastHandler.Cast.GetInitialCaster();

            foreach (var target in targets)
            {
                SpellCast cast = new SpellCast(caster, spell, target.Cell, CastHandler.Cast); //  // Initial Caster or Source ? Ebène
                cast.Token = this.GetTriggerToken<ITriggerToken>();
                cast.Force = true;
                caster.CastSpell(cast);
            }
        }
    }
}
