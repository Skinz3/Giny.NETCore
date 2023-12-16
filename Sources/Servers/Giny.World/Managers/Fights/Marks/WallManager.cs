using Giny.Core.DesignPattern;
using Giny.Protocol.Custom.Enums;
using Giny.Protocol.Messages;
using Giny.World.Managers.Fights.Cast;
using Giny.World.Managers.Fights.Fighters;
using Giny.World.Records.Spells;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Giny.World.Managers.Fights.Marks
{
    public class WallManager : Singleton<WallManager>
    {
        private DirectionsEnum[] WallsDirections = new DirectionsEnum[]
        {
            DirectionsEnum.DIRECTION_NORTH_EAST,
            DirectionsEnum.DIRECTION_NORTH_WEST,
            DirectionsEnum.DIRECTION_SOUTH_EAST,
            DirectionsEnum.DIRECTION_SOUTH_WEST,
        };

        private Dictionary<byte, short> WallSpells = new Dictionary<byte, short>()
        {
            {2,13458 },
            {3,13461 },
            {5,13501 },
            {4,13465 },
        };

        private Dictionary<byte, Color> WallColors = new Dictionary<byte, Color>()
        {
            {2,Color.FromArgb(16711680) },
            {3,Color.FromArgb(8421376) },
            {4,Color.FromArgb(8421631) },
            {5,Color.FromArgb(8404992)},
        };

        public const short WallMaxRange = 6;

        private List<Wall> GetWalls(SummonedBomb bomb)
        {
            List<Wall> results = new List<Wall>();

            if (!bomb.AliveSafe)
            {
                return results;
            }

            var center = bomb.Cell.Point;


            foreach (var direction in WallsDirections)
            {
                var points = center.GetCellsInDirection(direction, WallMaxRange + 1).ToList();
                points.RemoveAll(x => x == null);
                points = points.Where(x => center.DistanceTo(x) >= 2).ToList();



                foreach (var point in points)
                {
                    var otherBomb = bomb.Team.GetFighter<SummonedBomb>(x =>
                    x.AliveSafe && x.Cell.Id == point.CellId
                    && x.Record.Id == bomb.Record.Id);

                    if (otherBomb != null)
                    {
                        SpellBombRecord spellBomb = SpellBombRecord.GetSpellBomb(bomb.Record.Id);

                        if (WallSpells.ContainsKey(spellBomb.WallId))
                        {

                            short wallSpellId = WallSpells[spellBomb.WallId];

                            Color wallColor = WallColors[spellBomb.WallId];

                            var grade = bomb.GetSummoningEffect().CastHandler.Cast.Spell.Level.Grade;

                            Spell wallSpell = new Spell(SpellRecord.GetSpellRecord(wallSpellId), grade);

                            Wall wall = Wall.CreateWall(bomb, otherBomb, wallSpell, wallColor);

                            results.Add(wall);
                            break;
                        }
                        else
                        {
                            throw new NotImplementedException("Not implemented wall type :" + spellBomb.WallId);
                        }

                    }
                }

            }

            return results;


        }

        public void UpdateWalls(Fight fight)
        {
            var oldWalls = fight.GetMarks<Wall>().ToList();

            var newWalls = new List<Wall>();

            foreach (var summonedBomb in fight.GetFighters<SummonedBomb>(x => x.AliveSafe))
            {
                newWalls.AddRange(WallManager.Instance.GetWalls(summonedBomb));
            }

            foreach (var newWall in newWalls)
            {
                var old = oldWalls.FirstOrDefault(x => x.CompareCells(newWall));

                if (old != null && old.Valid())
                {
                    oldWalls.Remove(old);
                    continue;
                }
                else if (fight.GetMarks<Wall>().Any(x => x.CompareCells(newWall)))
                {
                    continue;
                }
                else
                {
                    if(newWall.Valid())
                    {
                        fight.AddMark(newWall);
                    }

                }
            }

            foreach (var old in oldWalls)
            {
                fight.RemoveMark(old);
            }
        }
    }
}
