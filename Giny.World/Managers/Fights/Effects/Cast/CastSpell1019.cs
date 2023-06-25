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
    [SpellEffectHandler(EffectsEnum.Effect_CastSpell_1019)]
    public class CastSpell1019 : SpellEffectHandler
    {
        public CastSpell1019(EffectDice effect, SpellCastHandler castHandler) : base(effect, castHandler)
        {

        }

        protected override void Apply(IEnumerable<Fighter> targets)
        {
            Spell spell = CreateCastedSpell();

            ITriggerToken token = this.GetTriggerToken<ITriggerToken>();

            var source = Source;

            if (token != null)
            {
                source = token.GetSource();
            }

            foreach (var target in targets) // Ratrapry (Prisma) verify source.
            {
                var targetCell = target.Cell;

                if (token != null) // Dérobade, sram
                {
                    targetCell = token.GetSource().Cell;
                }

                SpellCast cast = new SpellCast(source, spell, targetCell, CastHandler.Cast);
                cast.Token = this.GetTriggerToken<ITriggerToken>();
                cast.Force = true;
                cast.Silent = true;
                source.CastSpell(cast);
            }


        }
    }
}
