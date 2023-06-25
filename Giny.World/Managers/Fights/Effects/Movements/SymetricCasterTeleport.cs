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
    [SpellEffectHandler(EffectsEnum.Effect_SymetricCasterTeleport)]
    public class SymetricCasterTeleport : SpellEffectHandler
    {
        public SymetricCasterTeleport(EffectDice effect, SpellCastHandler castHandler) : base(effect, castHandler)
        {
        }

        protected override void Apply(IEnumerable<Fighter> targets)
        {
            foreach (var target in targets)
            {
                var targetPoint = new MapPoint((2 * Source.Cell.Point.X - target.Cell.Point.X), (2 * Source.Cell.Point.Y - target.Cell.Point.Y));
                Telefrag telefrag = target.Teleport(Source, Source.Fight.Map.GetCell(targetPoint));

                if (telefrag != null)
                    CastHandler.AddTelefrag(telefrag);

            }
        }
    }
}
