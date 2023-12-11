using Giny.Protocol.Enums;
using Giny.World.Managers.Effects;
using Giny.World.Managers.Fights.Cast;
using Giny.World.Managers.Fights.Fighters;
using Giny.World.Managers.Fights.Zones;
using Microsoft.AspNetCore.Http.Features;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Giny.World.Managers.Fights.Effects.Movements
{
    [SpellEffectHandler(EffectsEnum.Effect_Attract)]
    public class Attract : SpellEffectHandler
    {
        public Attract(EffectDice effect, SpellCastHandler castHandler) : base(effect, castHandler)
        {
        }

        protected override void Apply(IEnumerable<Fighter> targets)
        {
            var range = Source.GetSpellRange(CastHandler.Cast.Spell.Level);

            var direction = Source.Cell.Point.OrientationTo(TargetCell.Point);

            var zone = new Line(1, (byte)range, false, true, direction);

            var cells = zone.GetCells(Source.Cell, Source.Cell, Source.Fight.Map);


            Fighter? target = null;

            foreach (var cell in cells)
            {
                var fighter = Source.Fight.GetFighter(cell.Id);

                if (fighter != null && IsValidTarget(fighter))
                {
                    target = fighter;
                    break;
                }
            }

            if (target != null)
            {
                target.PullForward(Source, target.Cell, target.Cell.Point.DistanceTo(TargetCell.Point), TargetCell);
            }
            
        }
    }
}
