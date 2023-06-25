using Giny.Protocol.Enums;
using Giny.World.Managers.Effects;
using Giny.World.Managers.Fights.Cast;
using Giny.World.Managers.Fights.Fighters;
using Giny.World.Managers.Fights.History;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Giny.World.Managers.Fights.Effects.Movements
{
    [SpellEffectHandler(EffectsEnum.Effect_ReturnToLastPos)]
    public class ReturnToLastPos : SpellEffectHandler
    {
        public ReturnToLastPos(EffectDice effect, SpellCastHandler castHandler) : base(effect, castHandler)
        {
        }

        protected override void Apply(IEnumerable<Fighter> targets)
        {
            foreach (var target in targets)
            {
                MovementHistoryEntry entry = target.MovementHistory.PopPreviousPosition();

                if (entry != null)
                {
                    if (!CastHandler.IsTelefraged(target))
                    {
                        var telefrag = target.Teleport(Source, entry.Cell, false);

                        if (telefrag != null)
                        {
                            this.CastHandler.AddTelefrag(telefrag);
                        }
                    }
                }
            }
        }
    }
}
