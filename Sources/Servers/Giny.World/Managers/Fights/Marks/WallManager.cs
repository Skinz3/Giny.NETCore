using Giny.Core.DesignPattern;
using Giny.Protocol.Custom.Enums;
using Giny.Protocol.Messages;
using Giny.World.Managers.Fights.Fighters;
using System;
using System.Collections.Generic;
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

        private const short WallMaxRange = 6;

        public List<Wall> GetWalls(SummonedBomb bomb)
        {
            List<Wall> results = new List<Wall>();

            /*   var center = bomb.Cell.Point;


            foreach (var direction in WallsDirections)
            {
                var points = center.GetCellsInDirection(direction, WallMaxRange).ToList();
                points.RemoveAll(x => x == null);
                points = points.Where(x => center.DistanceTo(x) >= 2).ToList();

                foreach (var point in points)
                {
                    bomb.Fight.Send(new ShowCellMessage(bomb.Id, point.CellId));
                }

                foreach (var point in points)
                {
                    var otherBomb = bomb.Team.GetFighter<SummonedBomb>(x => x.Cell.Id == point.CellId);

                    if (otherBomb != null)
                    {
                        Wall wall = Wall.CreateWall(bomb, otherBomb);
                        bomb.Fight.AddMark(wall);
                    }
                }

            }*/

            return results;


        }
    }
}
