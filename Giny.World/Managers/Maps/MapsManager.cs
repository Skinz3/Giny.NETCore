using Giny.Core;
using Giny.Core.DesignPattern;
using Giny.Core.Extensions;
using Giny.Core.Logging;
using Giny.Core.Time;
using Giny.ORM;
using Giny.Protocol.Custom.Enums;
using Giny.World.Api;
using Giny.World.Handlers.Roleplay.Maps.Paths;
using Giny.World.Managers.Generic;
using Giny.World.Managers.Maps.Instances;
using Giny.World.Managers.Monsters;
using Giny.World.Records;
using Giny.World.Records.Maps;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Giny.World.Managers.Maps
{
    public class MapsManager : Singleton<MapsManager>
    {
        public const string MAP_KEY = "649ae451ca33ec53bbcbcc33becf15f4";

        [StartupInvoke("Map Instances", StartupInvokePriority.FourthPass)]
        public void CreateInstances()
        {
            var maps = MapRecord.GetMaps().ToArray();

            ProgressLogger progressLogger = new ProgressLogger();

            int n = 0;
            foreach (var record in maps)
            {
                record.Instance = new ClassicMapInstance(record);

                AsyncRandom random = new AsyncRandom();

                if (record.IsDungeonMap)
                {
                    MonstersManager.Instance.SpawnDungeonGroup(record);
                }
                else if (record.CanSpawnMonsters)
                {
                    MonstersManager.Instance.SpawnMonsterGroups(record, random);
                }
                progressLogger.WriteProgressBar(n++, maps.Length);
            }

            progressLogger.Flush();

        }

        public bool AddInteractiveSkill(MapRecord map, int elementId, GenericActionEnum genericAction, InteractiveTypeEnum interactiveType,
            SkillTypeEnum skillType, string param1 = null, string param2 = null, string param3 = null)
        {
            var elements = map.Elements.Where(x => x.Identifier == elementId);

            if (elements.Count() == 0)
            {
                return false;
            }


            var element = elements.First();

            if (element.Skill != null)
            {
                element.Skill.Param1 = param1;
                element.Skill.Param2 = param2;
                element.Skill.Param3 = param3;
                element.Skill.SkillId = skillType;
                element.Skill.Type = interactiveType;
                element.Skill.ActionIdentifier = genericAction;
                element.Skill.UpdateInstantElement();
                element.Skill.Record = SkillRecord.GetSkill(skillType);
                map.Instance.Reload();
            }
            else
            {

                InteractiveSkillRecord skillRecord = new InteractiveSkillRecord()
                {
                    ActionIdentifier = genericAction,
                    Criteria = string.Empty,
                    Id = TableManager.Instance.PopId<InteractiveSkillRecord>(),
                    Identifier = element.Identifier,
                    MapId = map.Id,
                    Param1 = param1,
                    Param2 = param2,
                    Param3 = param3,
                    SkillId = skillType,
                    Type = interactiveType,
                    Record = SkillRecord.GetSkill(skillType),
                };

                element.Skill = skillRecord;
                skillRecord.AddInstantElement();
                map.Instance.Reload();
            }
            return true;

        }
        public int GetNeighbourMapId(MapRecord map, MapScrollEnum scrollType)
        {
            var scrollAction = MapScrollActionRecord.GetMapScrollAction(map.Id);

            switch (scrollType)
            {
                case MapScrollEnum.TOP:
                    return scrollAction != null && scrollAction.TopMapId != 0 ? scrollAction.TopMapId : map.TopMap;
                case MapScrollEnum.LEFT:
                    return scrollAction != null && scrollAction.LeftMapId != 0 ? scrollAction.LeftMapId : map.LeftMap;
                case MapScrollEnum.BOTTOM:
                    return scrollAction != null && scrollAction.BottomMapId != 0 ? scrollAction.BottomMapId : map.BottomMap;
                case MapScrollEnum.RIGHT:
                    return scrollAction != null && scrollAction.RightMapId != 0 ? scrollAction.RightMapId : map.RightMap;
            }

            return -1;
        }

        public short GetNeighbourCellId(short cellId, MapScrollEnum scrollType)
        {
            switch (scrollType)
            {
                case MapScrollEnum.TOP:
                    return (short)(cellId + 532);
                case MapScrollEnum.LEFT:
                    return (short)(cellId + 27);
                case MapScrollEnum.BOTTOM:
                    return (short)(cellId - 532);
                case MapScrollEnum.RIGHT:
                    return (short)(cellId - 27);
                default:
                    return 0;
            }
        }

        public DirectionsEnum GetScrollDirection(MapScrollEnum scrollType)
        {
            switch (scrollType)
            {
                case MapScrollEnum.TOP:
                    return DirectionsEnum.DIRECTION_NORTH;

                case MapScrollEnum.LEFT:
                    return DirectionsEnum.DIRECTION_WEST;

                case MapScrollEnum.BOTTOM:
                    return DirectionsEnum.DIRECTION_SOUTH;

                case MapScrollEnum.RIGHT:
                    return DirectionsEnum.DIRECTION_EAST;

                default:
                    return DirectionsEnum.DIRECTION_EAST;
            }
        }

        private MapScrollEnum InvertScrollType(MapScrollEnum scroll)
        {
            switch (scroll)
            {
                case MapScrollEnum.TOP:
                    return MapScrollEnum.BOTTOM;
                case MapScrollEnum.BOTTOM:
                    return MapScrollEnum.TOP;
                case MapScrollEnum.LEFT:
                    return MapScrollEnum.RIGHT;
                case MapScrollEnum.RIGHT:
                    return MapScrollEnum.LEFT;
            }
            return MapScrollEnum.UNDEFINED;
        }
        public short FindNearMapBorder(MapRecord destinationMap, MapScrollEnum scrollType, MapPoint cellPoint)
        {
            var invertedScrollType = InvertScrollType(scrollType);
            var cells = destinationMap.GetMapChangeCells(invertedScrollType);
            if (cells.Count() == 0)
            {
                return destinationMap.RandomWalkableCell().Id;
            }

            return cells.Aggregate((previous, next) => previous.Point.ManhattanDistanceTo(cellPoint) < next.Point.ManhattanDistanceTo(cellPoint) ? previous : next).Id;
        }
        /*
         * If cell is free & walkable, return cell
         * else return near free cell.
         * if no free cell is available return random walkable cell
         */
        public CellRecord SecureRoleplayCell(MapRecord map, CellRecord roleplayCell)
        {
            if (roleplayCell.Walkable && map.Instance.IsCellFree(roleplayCell.Id))
            {
                return roleplayCell;
            }
            else
            {
                CellRecord freeCell = GetNearFreeCell(map, roleplayCell);

                if (freeCell != null)
                {
                    return freeCell;
                }
                else
                {
                    return map.RandomWalkableCell();
                }
            }
        }
        public CellRecord GetNearFreeCell(MapRecord map, CellRecord roleplayCell)
        {
            MapPoint[] points = roleplayCell.Point.GetNearPoints().Where(x => map.IsCellWalkable(x.CellId) && map.Instance.IsCellFree(x.CellId) == true).ToArray();

            if (points.Length > 0)
            {
                return map.GetCell(points.Random());
            }
            else
            {
                return null;
            }
        }
    }
}
