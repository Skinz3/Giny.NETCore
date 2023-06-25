using Giny.Core.DesignPattern;
using Giny.Protocol.Enums;
using Giny.World.Managers.Effects;
using Giny.World.Managers.Fights.Cast;
using Giny.World.Managers.Fights.Fighters;
using Giny.World.Managers.Fights.Marks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Rune = Giny.World.Managers.Fights.Marks.Rune;

namespace Giny.World.Managers.Fights.Effects.Movements
{
    [SpellEffectHandler(EffectsEnum.Effect_TeleportToRune)]
    public class TeleportRune : SpellEffectHandler
    {
        public TeleportRune(EffectDice effect, SpellCastHandler castHandler) : base(effect, castHandler)
        {

        }

        protected override void Apply(IEnumerable<Fighter> targets)
        {
            Rune rune = Source.GetMarks<Rune>().LastOrDefault();

            if (rune != null)
            {
                Telefrag telefrag = Source.Teleport(Source, rune.CenterCell);

                if (telefrag != null)
                {
                    CastHandler.AddTelefrag(telefrag);
                }

                Source.Fight.RemoveMark(rune);

            }
            else
            {
                Source.Fight.Warn("Unable to teleport to rune...");
            }
        }
    }
}
