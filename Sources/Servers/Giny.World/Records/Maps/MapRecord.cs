using Giny.Core;
using Giny.Core.DesignPattern;
using Giny.Core.Extensions;
using Giny.Core.Logging;
using Giny.ORM.Attributes;
using Giny.ORM.Interfaces;
using Giny.Protocol.Custom.Enums;
using Giny.Protocol.Types;
using Giny.World.Managers.Entities.Characters;
using Giny.World.Managers.Maps;
using Giny.World.Managers.Maps.Elements;
using Giny.World.Managers.Maps.Instances;
using ProtoBuf;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Giny.World.Records.Maps
{
    [Table("maps")]
    public class MapRecord : ITable
    {
        [Container]
        private static ConcurrentDictionary<long, MapRecord> Maps = new ConcurrentDictionary<long, MapRecord>();

        [Primary]
        public long Id
        {
            get;
            set;
        }
        public short SubareaId
        {
            get;
            set;
        }

        [Ignore]
        public SubareaRecord Subarea
        {
            get;
            set;
        }
        public sbyte Version
        {
            get;
            set;
        }
        [Ignore]
        public MapInstance Instance
        {
            get;
            set;
        }
        public int LeftMap
        {
            get;
            set;
        }
        public int RightMap
        {
            get;
            set;
        }
        public int TopMap
        {
            get;
            set;
        }
        public int BottomMap
        {
            get;
            set;
        }
        [ProtoSerialize, Update]
        public CellRecord[] Cells
        {
            get;
            set;
        }
        [ProtoSerialize]
        public InteractiveElementRecord[] Elements
        {
            get;
            set;
        }
        [Ignore]
        public List<CellRecord> WalkableCells
        {
            get;
            set;
        }
        [Ignore]
        public List<CellRecord> RightChange
        {
            get;
            set;
        }
        [Ignore]
        public List<CellRecord> LeftChange
        {
            get;
            set;
        }
        [Ignore]
        public List<CellRecord> BottomChange
        {
            get;
            set;
        }


        [Ignore]
        public List<CellRecord> TopChange
        {
            get;
            set;
        }
        [Ignore]
        public List<CellRecord> BlueCells
        {
            get;
            set;
        }
        [Ignore]
        public List<CellRecord> RedCells
        {
            get;
            set;
        }
        [Ignore]
        public MapPositionRecord Position
        {
            get;
            set;
        }
        [Ignore]
        public DungeonRecord Dungeon
        {
            get;
            set;
        }
        [Ignore]
        public bool CanSpawnMonsters =>
            !IsDungeonEntrance
            && Position.AllowMonsterRespawn
            && !IsDungeonMap
            && !HasZaap()
            && Cells.All(x => !x.FarmCell)
            && (BlueCells.Count > 0 && RedCells.Count > 0);

        [Ignore]
        public bool IsDungeonMap => Dungeon != null;

        [Ignore]
        public MonsterRoom MonsterRoom => Dungeon.Rooms[Id];

        [Ignore]
        public bool IsDungeonEntrance
        {
            get;
            set;
        }
        [StartupInvoke("Maps links", StartupInvokePriority.SecondPass)]
        public static void Initialize()
        {
            ProgressLogger progressLogger = new ProgressLogger();

            int i = 0;

            var maps = GetMaps();

            var mapsCount = maps.Count();

            foreach (var map in maps)
            {
                progressLogger.WriteProgressBar(i, mapsCount);
                map.ReloadMembers();
                i++;
            }

            progressLogger.Flush();
        }
        public long? GetNextRoomMapId()
        {
            return Dungeon.GetNextMapId(Id);
        }
        public bool IsCellWalkable(short cellId)
        {
            if (cellId < 0 || cellId > 560)
            {
                return false;
            }
            return Cells[cellId].Walkable;
        }
        public bool IsCellWalkable(int x, int y)
        {
            return IsCellWalkable(MapPoint.CoordToCellId(x, y));
        }
        public bool IsValidFightCell(short cellId)
        {
            return Cells[cellId].IsValidFightCell();
        }
        public bool IsValidFightCell(int x, int y)
        {
            if (!MapPoint.IsInMap(x, y))
            {
                return false;
            }
            return IsValidFightCell(MapPoint.CoordToCellId(x, y));
        }
        public CellRecord RandomWalkableCell()
        {
            return WalkableCells.Where(x => !x.FarmCell).Random();
        }
        public void ReloadMembers()
        {
            this.WalkableCells = this.Cells.Where(x => x.Walkable).ToList();
            this.RightChange = this.Cells.Where(x => x.IsRightChange).ToList();
            this.LeftChange = this.Cells.Where(x => x.IsLeftChange).ToList();
            this.TopChange = this.Cells.Where(x => x.IsTopChange).ToList();
            this.BottomChange = this.Cells.Where(x => x.IsBottomChange).ToList();

            this.BlueCells = this.Cells.Where(x => x.Blue).ToList();
            this.RedCells = this.Cells.Where(x => x.Red).ToList();

            this.Position = MapPositionRecord.GetMapPosition(this.Id);

            if (Position == null)
            {
                Logger.Write("Map " + Id + " has no position in database ...", Channels.Warning);
            }
            this.Subarea = SubareaRecord.GetSubarea(this.SubareaId);

            foreach (var element in this.Elements)
            {
                element.MapId = this.Id;
                element.Skill = InteractiveSkillRecord.GetInteractiveSkill(element.Identifier);
            }

            this.IsDungeonEntrance = DungeonRecord.IsDungeonEntrance(Id);

            this.Dungeon = DungeonRecord.GetDungeonRecord(Id);
        }
        public IEnumerable<CellRecord> GetMapChangeCells(MapScrollEnum scrollType)
        {
            IEnumerable<CellRecord> result = new List<CellRecord>();

            switch (scrollType)
            {
                case MapScrollEnum.TOP:
                    result = TopChange;
                    break;
                case MapScrollEnum.LEFT:
                    result = LeftChange;
                    break;
                case MapScrollEnum.BOTTOM:
                    result = BottomChange;
                    break;
                case MapScrollEnum.RIGHT:
                    result = RightChange;
                    break;
            }
            return result.Where(x => x.Point.IsInMap());
        }
        public CellRecord GetCell(int id)
        {
            if (id <= Cells.Length - 1)
            {
                return Cells[id];
            }
            else
            {
                return null;
            }
        }
        public CellRecord GetCell(int x, int y)
        {
            return GetCell(MapPoint.CoordToCellId(x, y));
        }
        public CellRecord GetCell(MapPoint point)
        {
            return GetCell(point.CellId);
        }
        public InteractiveElementRecord GetFirstElementRecord(InteractiveTypeEnum type)
        {
            return Elements.FirstOrDefault(x => x.Skill != null && x.Skill.Type == type);
        }
        public bool HasZaap()
        {
            return GetFirstElementRecord(InteractiveTypeEnum.ZAAP16) != null;
        }
        public short GetNearCell(InteractiveTypeEnum interactiveType)
        {
            InteractiveElementRecord element = GetFirstElementRecord(interactiveType);

            short cellId = element.Point.GetCellInDirection(DirectionsEnum.DIRECTION_SOUTH_WEST, 1).CellId;

            if (!this.IsCellWalkable(cellId))
            {
                MapPoint[] array = element.Point.GetNearPoints();
                cellId = array.Length == 0 ? this.RandomWalkableCell().Id : array[0].CellId;
            }
            return cellId;
        }
        public InteractiveElementRecord GetElementRecord(int elemId)
        {
            return Elements.FirstOrDefault(x => x.Identifier == elemId);
        }

        public MapCoordinatesExtended GetMapCoordinatesExtended()
        {
            return new MapCoordinatesExtended()
            {
                mapId = Id,
                subAreaId = SubareaId,
                worldX = (short)Position.X,
                worldY = (short)Position.Y
            };
        }

        public static MapRecord GetMap(long mapId)
        {
            return Maps[mapId];
        }
        public static IEnumerable<MapRecord> GetMaps(Point point)
        {
            return MapPositionRecord.GetMapPositions().Where(x => x.Point == point).Select(x => GetMap(x.Id));
        }
        public static IEnumerable<MapRecord> GetMaps()
        {
            return Maps.Values;
        }

        public static InteractiveElementRecord[] GetElementsByBonesId(List<int> bonesIds)
        {
            List<InteractiveElementRecord> records = new List<InteractiveElementRecord>();

            foreach (var map in Maps)
            {
                foreach (var element in map.Value.Elements)
                {
                    if (bonesIds.Contains(element.BonesId))
                    {
                        records.Add(element);
                    }
                }
            }

            return records.ToArray();
        }

    }
    [ProtoContract]
    public class InteractiveElementRecord
    {
        [ProtoMember(1)]
        public int Identifier
        {
            get;
            set;
        }
        [ProtoMember(2)]
        public short CellId
        {
            get;
            set;
        }
        [ProtoMember(3)]
        public int GfxId
        {
            get;
            set;
        }
        [ProtoMember(4)]
        public int BonesId
        {
            get;
            set;
        }
        [ProtoMember(5)]
        public int OffsetX
        {
            get;
            set;
        }
        [ProtoMember(6)]
        public int OffsetY
        {
            get;
            set;
        }
        public InteractiveSkillRecord Skill // this should be an array if we follow official
        {
            get;
            set;
        }
        public MapPoint Point
        {
            get
            {
                return new MapPoint(CellId);
            }
        }
        public long MapId
        {
            get;
            set;
        }
        public bool Stated
        {
            get
            {
                return BonesId != -1;
            }
        }


        public MapElement GetMapElement(MapInstance mapInstance)
        {
            if (Stated)
            {
                return new MapStatedElement(mapInstance, this);
            }
            else
            {
                return new MapInteractiveElement(mapInstance, this);
            }
        }

        [WIP("not working properly.")]
        public bool IsInMap()
        {
            return OffsetX == 0 && OffsetY == 0;
        }
    }
    [ProtoContract]
    public class CellRecord
    {
        [ProtoMember(1)]
        public short Id
        {
            get;
            set;
        }
        [ProtoMember(2)]
        public bool Blue
        {
            get;
            set;
        }
        [ProtoMember(3)]
        public bool Red
        {
            get;
            set;
        }
        [ProtoMember(4)]
        public int LosMov
        {
            get;
            set;
        }
        [ProtoMember(5)]
        public int MapChangeData
        {
            get;
            set;
        }

        private MapPoint m_mapPoint
        {
            get;
            set;
        }
        [Ignore]
        public MapPoint Point
        {
            get
            {
                if (m_mapPoint != null)
                {
                    return m_mapPoint;
                }
                else
                {
                    m_mapPoint = new MapPoint(Id);
                    return m_mapPoint;
                }
            }
        }
        public bool NonWalkableDuringRP => (LosMov & 4) != 0;

        public bool Walkable => (LosMov & 1) == 0; // all map versions >= 11

        public bool NonWalkableDuringFight => (LosMov & 2) != 0;

        public bool FarmCell => (LosMov & 128) != 0;

        public bool IsRightChange => (MapChangeData & 1) > 0;

        public bool IsLeftChange => (MapChangeData & 16) > 0;

        public bool IsTopChange => (MapChangeData & 64) > 0;

        public bool IsBottomChange => (MapChangeData & 4) > 0;

        public bool LineOfSight => (LosMov & 8) == 0;

        public bool IsValidFightCell()
        {
            return Walkable && !FarmCell && !NonWalkableDuringFight && !IsRightChange && !IsBottomChange &&
                !IsTopChange && !IsLeftChange;
        }
        public override string ToString()
        {
            return Id.ToString();
        }
    }
}
