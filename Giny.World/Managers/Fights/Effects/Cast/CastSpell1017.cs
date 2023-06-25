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
     * Mot Blessant (chaque cible lance le sort)
     * Invocation Osamodas
     */
    [SpellEffectHandler(EffectsEnum.Effect_CastSpell_1017)]
    public class CastSpell1017 : SpellEffectHandler
    {
        public CastSpell1017(EffectDice effect, SpellCastHandler castHandler) : base(effect, castHandler)
        {

        }

        protected override void Apply(IEnumerable<Fighter> targets)
        {
            Spell spell = CreateCastedSpell();

            ITriggerToken token = this.GetTriggerToken<ITriggerToken>();

            var targetCell = Source.Cell;

            if (token != null)
            {
                targetCell = token.GetSource().Cell;
            }


            foreach (var target in targets)
            {
                SpellCast cast = new SpellCast(target, spell, targetCell, CastHandler.Cast);
                cast.Token = this.GetTriggerToken<ITriggerToken>();
                cast.Force = true;
                cast.Silent = true;
                target.CastSpell(cast);
            }
        }
    }
}
