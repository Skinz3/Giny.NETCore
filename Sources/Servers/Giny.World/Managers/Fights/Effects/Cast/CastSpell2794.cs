using Giny.Protocol.Enums;
using Giny.World.Managers.Effects;
using Giny.World.Managers.Fights.Cast;
using Giny.World.Managers.Fights.Fighters;
using Giny.World.Records.Spells;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Giny.World.Managers.Fights.Effects.Cast
{
    /*
     * Déplacement Osamodas
     */
    [SpellEffectHandler(EffectsEnum.Effect_CastSpell_2794)]
    public class CastSpell2794 : SpellEffectHandler 
    {
        public CastSpell2794(EffectDice effect, SpellCastHandler castHandler) : base(effect, castHandler)
        {

        }

        protected override void Apply(IEnumerable<Fighter> targets)
        {
            Spell spell = CreateCastedSpell();

            if (spell == null)
            {
                return;
            }
            foreach (var target in targets)  // target , sure 
            {
                SpellCast cast = new SpellCast(target, spell, TargetCell, CastHandler.Cast);  // TargetCell, sure ! 
                cast.Token = this.GetTriggerToken<ITriggerToken>();
                cast.Force = true;
                cast.Silent = true;
                target.CastSpell(cast);
            }
        }
    }
}
