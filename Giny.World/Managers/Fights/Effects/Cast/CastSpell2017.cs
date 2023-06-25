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
     * Étreinte de Servitude , sort : Fers de la Tyrannie
     */
    [SpellEffectHandler(EffectsEnum.Effect_CastSpell2017)]
    public class CastSpell2017 : SpellEffectHandler
    {
        public CastSpell2017(EffectDice effect, SpellCastHandler castHandler) : base(effect, castHandler)
        {

        }

        protected override void Apply(IEnumerable<Fighter> targets)
        {
            Spell spell = CreateCastedSpell();

            if (spell == null)
            {
                return;
            }

            foreach (var target in targets)
            {
                SpellCast cast = new SpellCast(Source, spell, Source.Cell, CastHandler.Cast); 
                cast.Token = this.GetTriggerToken<ITriggerToken>();
                cast.Force = true;
                cast.Silent = true;
                Source.CastSpell(cast);
            }

        }
    }
}
