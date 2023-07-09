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
    /// <summary>
    /// Dofus Abyssal
    /// Flèche fulminante
    /// </summary>
    [SpellEffectHandler(EffectsEnum.Effect_CasterExecuteSpellGlobalLimitation)]  
    public class CasterExecuteSpellGlobalLimitation : SpellEffectHandler
    {

        public CasterExecuteSpellGlobalLimitation(EffectDice effect, SpellCastHandler castHandler) : base(effect, castHandler)
        {

        }

        protected override void Apply(IEnumerable<Fighter> targets)
        {
            Spell spell = CreateCastedSpell();


            foreach (var target in targets.Take(Effect.Value))
            {
                Source.Fight.Send(new Giny.Protocol.Messages.ShowCellMessage(target.Cell.Id, target.Cell.Id));

                System.Threading.Thread.Sleep(500);
                SpellCast cast = new SpellCast(Source, spell, target.Cell, CastHandler.Cast);
                cast.Token = this.GetTriggerToken<ITriggerToken>();
                cast.Force = true;
                cast.Silent = true;
                Source.CastSpell(cast);
            }
        }
    }
}
