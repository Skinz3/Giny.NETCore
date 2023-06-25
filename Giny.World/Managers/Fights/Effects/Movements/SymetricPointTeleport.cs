using Giny.Protocol.Enums;
using Giny.World.Managers.Effects;
using Giny.World.Managers.Fights.Cast;
using Giny.World.Managers.Fights.Fighters;
using Giny.World.Managers.Maps;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Giny.World.Managers.Fights.Effects.Movements
{
    [SpellEffectHandler(EffectsEnum.Effect_SymetricPointTeleport)]
    public class SymetricPointTeleport : SpellEffectHandler
    {
        public SymetricPointTeleport(EffectDice effect, SpellCastHandler castHandler) : base(effect, castHandler)
        {
        }

        protected override void Apply(IEnumerable<Fighter> targets)
        {
            foreach (var target in targets)
            {
                var targetPoint = new MapPoint((2 * TargetCell.Point.X - target.Cell.Point.X), (2 * TargetCell.Point.Y - target.Cell.Point.Y));

                var telefrag = target.Teleport(Source, Source.Fight.Map.GetCell(targetPoint));

                if (telefrag != null)
                {
                    this.CastHandler.AddTelefrag(telefrag);
                }
            }
        }
    }
}
