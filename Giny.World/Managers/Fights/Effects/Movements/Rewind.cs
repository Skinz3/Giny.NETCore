using Giny.Protocol.Enums;
using Giny.World.Managers.Effects;
using Giny.World.Managers.Fights.Cast;
using Giny.World.Managers.Fights.Fighters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Giny.World.Managers.Fights.Effects.Movements
{
    [SpellEffectHandler(EffectsEnum.Effect_Rewind)]
    public class Rewind : SpellEffectHandler
    {

        public Rewind(EffectDice effect, SpellCastHandler castHandler) : base(effect, castHandler)
        {
        }

        protected override void Apply(IEnumerable<Fighter> targets)
        {
            foreach (var target in targets)
            {
                var telefrag = target.Teleport(Source, target.TurnStartCell);

                if (telefrag != null)
                {
                    this.CastHandler.AddTelefrag(telefrag);
                }
            }
        }
    }
}
