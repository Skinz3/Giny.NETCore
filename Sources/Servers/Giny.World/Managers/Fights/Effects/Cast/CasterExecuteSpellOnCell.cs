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
    [SpellEffectHandler(EffectsEnum.Effect_CasterExecuteSpellOnCell)]
    public class CasterExecuteSpellOnCell : SpellEffectHandler
    {
        public CasterExecuteSpellOnCell(EffectDice effect, SpellCastHandler castHandler) : base(effect, castHandler)
        {
        }

        protected override void Apply(IEnumerable<Fighter> targets)
        {
            Spell spell = CreateCastedSpell();

            if (spell == null)
            {
                return;
            }

            SpellCast cast = new SpellCast(Source, spell, TargetCell, CastHandler.Cast);    
            cast.Token = this.GetTriggerToken<ITriggerToken>();
            cast.Force = true;
            cast.Silent = true;
            Source.CastSpell(cast);
        }
    }
}
