using Giny.Core.DesignPattern;
using Giny.Protocol.Enums;
using Giny.World.Managers.Effects;
using Giny.World.Managers.Fights.Buffs;
using Giny.World.Managers.Fights.Cast;
using Giny.World.Managers.Fights.Fighters;
using Giny.World.Records.Maps;
using Giny.World.Records.Spells;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Giny.World.Managers.Fights.Effects.Cast
{
    /*
     * Mot Interdit
     * Rassemblement 
     * Friction 
     * Ségnifuge (Enutrof)
     * Multilation (Sacrieur)
     * Balise de rappel (pb animation)
     */
    [SpellEffectHandler(EffectsEnum.Effect_TargetExecuteSpell)]
    public class TargetExecuteSpell : SpellEffectHandler
    {
        public TargetExecuteSpell(EffectDice effect, SpellCastHandler castHandler) : base(effect, castHandler)
        {
        }

        protected override void Apply(IEnumerable<Fighter> targets)
        {
            Spell spell = CreateCastedSpell();

            foreach (var target in targets)
            {
                SpellCast cast = new SpellCast(target, spell, target.Cell, CastHandler.Cast);

                var parent = cast.GetParent();

                cast.Token = this.GetTriggerToken<ITriggerToken>();
                cast.Force = true;
                cast.Silent = true;
                //cast.Animation = ??? Balise de rappel 
                target.CastSpell(cast);
            }

        }
    }
}
