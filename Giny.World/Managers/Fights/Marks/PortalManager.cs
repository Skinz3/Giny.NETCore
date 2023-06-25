using Giny.Core.DesignPattern;
using Giny.Protocol.Enums;
using Giny.World.Managers.Fights.Fighters;
using Giny.World.Managers.Maps;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Giny.World.Managers.Fights.Marks
{
    [WIP]
    public class PortalManager : Singleton<PortalManager>
    {
        private const int Same = 0;

        private const int Opposite = 1;

        private const int Trigonometric = 2;

        private const int CounterTrigonometric = 3;

        private const int BestDistance = 63;

        public const short PortalTeleportSpellId = 14573;

        public Tuple<Portal, Portal> GetPortalsTuple(Fight fight, short cellId)
        {
            var portals = fight.GetMarks<Portal>().ToList();

            var enter = portals.FirstOrDefault(x => x.CenterCell.Id == cellId);

            if (enter == null)
            {
                return null;
            }

            var checkPoints = portals.FindAll(x => x.Source.Id == enter.Source.Id && x.Active).Select(x => new MapPoint(x.CenterCell.Id)).ToList();
            MapPoint startPoint = new MapPoint(enter.CenterCell.Id);
            MapPoint next = null;
            int index = 0;

            if (checkPoints.Count() == 1 && startPoint.CellId == checkPoints[0].CellId)
            {
                return null;
            }

            List<MapPoint> pointsList = new List<MapPoint>();
            for (int i = 0; i < checkPoints.Count; i++)
            {
                if (checkPoints[i].CellId != startPoint.CellId)
                {
                    pointsList.Add(checkPoints[i]);
                }
            }
            List<uint> res = new List<uint>();
            MapPoint current = startPoint;
            int maxTry = pointsList.Count + 1;
            while (maxTry > 0)
            {
                maxTry--;
                res.Add((uint)current.CellId);
                index = pointsList.IndexOf(current);
                if (index != -1)
                {
                    pointsList.RemoveAt(index);
                }
                next = getClosestPortal(current, pointsList);
                if (next == null)
                {
                    break;
                }
                current = next;
            }
            if (res.Count < 2)
            {
                return null;
            }
            return new Tuple<Portal, Portal>(GetPortal(enter.Source, (short)res.First()), GetPortal(enter.Source, (short)res.Last()));
        }
        public Portal GetPortal(Fighter owner, short cellId)
        {
            return owner.Fight.GetMarks<Portal>().FirstOrDefault(x => x.Source.Id == owner.Id && x.CenterCell.Id == cellId);
        }
        public int GetCasesCount(Fight fight, short cellId)
        {
            var portals = fight.GetMarks<Portal>().ToList();

            int casesCount = 0;
            var enter = portals.FirstOrDefault(x => x.CenterCell.Id == cellId);
            if (enter == null)
                return casesCount;
            var checkPoints = portals.FindAll(x => x.Source.Id == enter.Source.Id && x.Active).Select(x => new MapPoint(x.CenterCell.Id)).ToList();
            MapPoint startPoint = new MapPoint(enter.CenterCell.Id);
            MapPoint next = null;
            int index = 0;
            if (checkPoints.Count() == 1 && startPoint.CellId == checkPoints[0].CellId)
            {
                return casesCount;
            }
            List<MapPoint> pointsList = new List<MapPoint>();
            for (int i = 0; i < checkPoints.Count(); i++)
            {
                if (checkPoints[i].CellId != startPoint.CellId)
                {
                    pointsList.Add(checkPoints[i]);
                }
            }
            List<uint> res = new List<uint>();
            MapPoint current = startPoint;
            int maxTry = pointsList.Count + 1;
            while (maxTry > 0)
            {
                maxTry--;
                res.Add((uint)current.CellId);
                index = pointsList.IndexOf(current);
                if (index != -1)
                {
                    pointsList.RemoveAt(index);
                }
                next = getClosestPortal(current, pointsList);
                if (next == null)
                {
                    break;
                }
                current = next;
            }
            if (res.Count < 2)
            {
                return casesCount;
            }
            for (int i = 1; i < res.Count; i++)
            {
                var actual = new MapPoint((short)res[i - 1]);
                var end = new MapPoint((short)res[i]);
                casesCount += (int)actual.ManhattanDistanceTo(end);
            }
            return casesCount;
        }

        private MapPoint getClosestPortal(MapPoint refMapPoint, List<MapPoint> portals)
        {
            int dist = 0;
            List<MapPoint> closests = new List<MapPoint>();
            int bestDist = BestDistance;
            foreach (var portal in portals)
            {
                dist = refMapPoint.ManhattanDistanceTo(portal);
                if (dist < bestDist)
                {
                    closests.Clear();
                    closests.Add(portal);
                    bestDist = dist;
                }
                else if (dist == bestDist)
                {
                    closests.Add(portal);
                }
            }
            if (closests.Count == 0)
            {
                return null;
            }
            if (closests.Count == 1)
            {
                return closests[0];
            }
            return getBestNextPortal(refMapPoint, closests);
        }

        private MapPoint getBestNextPortal(MapPoint refCell, List<MapPoint> closests)
        {
            Point refCoord = new Point();
            Point nudge = new Point();

            if (closests.Count < 2)
            {
                throw new ArgumentException("closests should have a size of 2.");
            }
            refCoord = new Point(refCell.X, refCell.Y);
            nudge = new Point(refCoord.X, refCoord.Y + 1);
            closests.Sort(delegate (MapPoint o1, MapPoint o2)
            {
                double tap = GetOrientedAngle(refCoord, nudge, new Point(o1.X, o1.Y)) - GetOrientedAngle(refCoord, nudge, new Point(o2.X, o2.Y));
                return tap > 0 ? 1 : tap < 0 ? -1 : 0;
            });
            MapPoint res = getBestPortalWhenRefIsNotInsideClosests(refCell, closests);
            if (res != null)
            {
                return res;
            }
            return closests[0];
        }

        private MapPoint getBestPortalWhenRefIsNotInsideClosests(MapPoint refCell, List<MapPoint> sortedClosests)
        {
            if (sortedClosests.Count < 2)
            {
                return null;
            }
            MapPoint prev = sortedClosests[sortedClosests.Count - 1];
            foreach (var portal in sortedClosests)
            {
                switch (compareAngles(refCell.Coordinates, prev.Coordinates, portal.Coordinates))
                {
                    case Opposite:
                        if (sortedClosests.Count <= 2)
                        {
                            return null;
                        }
                        break;
                    case CounterTrigonometric:
                        return prev;
                    default:
                        prev = portal;
                        continue;
                }
            }
            return null;
        }

        private double GetOrientedAngle(Point refCell, Point cellA, Point cellB)
        {
            switch (compareAngles(refCell, cellA, cellB))
            {
                case Same:
                    return 0;
                case Opposite:
                    return Math.PI;
                case Trigonometric:
                    return GetAngle(refCell, cellA, cellB);
                case CounterTrigonometric:
                    return 2 * Math.PI - GetAngle(refCell, cellA, cellB);
                default:
                    return 0;
            }
        }

        private double GetAngle(Point coordRef, Point coordA, Point coordB)
        {
            var a = GetDistance(coordA, coordB);
            var b = GetDistance(coordRef, coordA);
            var c = GetDistance(coordRef, coordB);
            return Math.Acos((b * b + c * c - a * a) / (2 * b * c));
        }

        private double GetDistance(Point ref_start, Point ref_end)
        {
            return Math.Sqrt(Math.Pow(ref_start.X - ref_end.X, 2) + Math.Pow(ref_start.Y - ref_end.Y, 2));
        }

        private int compareAngles(Point refe, Point start, Point end)
        {
            Point aVec = vector(refe, start);
            Point bVec = vector(refe, end);
            int det = GetDeterminant(aVec, bVec);
            if (det != 0)
            {
                return det > 0 ? Trigonometric : CounterTrigonometric;
            }
            return aVec.X >= 0 == bVec.X >= 0 && aVec.Y >= 0 == bVec.Y >= 0 ? Same : Opposite;
        }

        private int GetDeterminant(Point aVec, Point bVec)
        {
            return aVec.X * bVec.Y - aVec.Y * bVec.X;
        }

        private Point vector(Point start, Point end)
        {
            return new Point(end.X - start.X, end.Y - start.Y);
        }
        public Color GetPortalColor(TeamEnum team)
        {
            switch (team)
            {
                case TeamEnum.TEAM_CHALLENGER:
                    return Color.Red;
                case TeamEnum.TEAM_DEFENDER:
                    return Color.Blue;
            }
            throw new Exception("Unknown team side. Unable to compute portal color.");
        }
    }
}
