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
    [SpellEffectHandler(EffectsEnum.Effect_CastSpell_1018)]
    public class CastSpell1018 : SpellEffectHandler
    {
        public CastSpell1018(EffectDice effect, SpellCastHandler castHandler) : base(effect, castHandler)
        {

        }

        protected override void Apply(IEnumerable<Fighter> targets)
        {
            Spell spell = CreateCastedSpell();

            var caster = CastHandler.Cast.GetCaster();

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
