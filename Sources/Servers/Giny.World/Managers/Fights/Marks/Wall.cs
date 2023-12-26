using Giny.Protocol.Custom.Enums;
using Giny.World.Managers.Effects;
using Giny.World.Managers.Fights.Cast;
using Giny.World.Managers.Fights.Fighters;
using Giny.World.Managers.Fights.Zones;
using Giny.World.Records.Maps;
using Giny.World.Records.Spells;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Giny.World.Managers.Fights.Marks
{
    public class Wall : Mark
    {
        public SummonedBomb FirstBomb
        {
            get;
            private set;
        }
        public SummonedBomb SecondBomb
        {
            get;
            private set;
        }

        public Wall(int id, Zone zone, MarkTriggerType triggers, Color color, SummonedBomb firstBomb, SummonedBomb secondBomb, Spell markSpell, Spell triggerSpell) : base(id, null, zone, triggers, color, firstBomb.Summoner, firstBomb.Cell, markSpell, triggerSpell, secondBomb.Cell)
        {
            this.FirstBomb = firstBomb;
            this.SecondBomb = secondBomb;
        }

        public override bool InterceptMovement => true;

        public override GameActionMarkTypeEnum Type => GameActionMarkTypeEnum.WALL;


        public override bool OnTurnBegin()
        {
            return false;
        }
        public override bool IsVisibleFor(CharacterFighter fighter)
        {
            return true;
        }


        public SummonedBomb GetPair(SummonedBomb bomb)
        {
            return bomb == FirstBomb ? SecondBomb : FirstBomb;
        }
        public bool IsWallMember(Fighter bomb)
        {
            return FirstBomb == bomb || SecondBomb == bomb;
        }
        public bool Valid()
        {
            bool sameLine = FirstBomb.Cell.Point.IsOnSameLine(SecondBomb.Cell.Point);
            var distance = FirstBomb.Cell.Point.DistanceTo(SecondBomb.Cell.Point) - 1;

            foreach (var cell in Cells)
            {
                var fighter = Source.Fight.GetFighter(cell.Id);

                if (fighter != null && fighter is SummonedBomb bomb)
                {
                    if (bomb.Record.Id == FirstBomb.Record.Id)
                    {
                        return false;
                    }
                }
            }

            foreach (var point in FirstBomb.Cell.Point.GetCellsOnLineBetween(SecondBomb.Cell.Point))
            {
                if (!Cells.Any(x => x.Point.CellId == point.CellId))
                {
                    return false;
                }
            }

            return sameLine && distance <= WallManager.WallMaxRange && FirstBomb.AliveSafe && SecondBomb.AliveSafe;

        }
        public override bool ShouldTriggerOnMove(Fighter target, short oldCellId, short cellId)
        {
            return base.ShouldTriggerOnMove(target, oldCellId, cellId) && !IsWallMember(target);
        }
        public override void OnAdded()
        {
            foreach (var cell in Cells)
            {
                var target = Source.Fight.GetFighter(cell.Id);

                if (target != null)
                {
                    ApplyEffects(target);

                }
            }
        }


        public override void OnRemoved()
        {

        }

        public override void Trigger(Fighter target, MarkTriggerType triggerType)
        {
            ApplyEffects(target);
        }

        protected override Fighter GetTriggerCastSource()
        {
            return FirstBomb;
        }

        public static Wall CreateWall(SummonedBomb bomb1, SummonedBomb bomb2, Spell wallSpell, Color wallColor)
        {
            var wallSize = (byte)(bomb1.Cell.Point.DistanceTo(bomb2.Cell.Point) - 1);


            Line line = new Line(1, wallSize, false, false, bomb1.Cell.Point.OrientationTo(bomb2.Cell.Point));

            Wall wall = new Wall(bomb1.Fight.PopNextMarkId(), line,
                MarkTriggerType.OnTurnBegin | MarkTriggerType.OnMove, wallColor, bomb1, bomb2, wallSpell, wallSpell);

            return wall;
        }

        public bool CompareCells(Wall wall)
        {
            return wall.Cells.Select(x => x.Id).OrderBy(x => x).SequenceEqual(this.Cells.Select(x => x.Id).OrderBy(x => x));
        }

        public override void OnUpdated()
        {

        }
    }
}
