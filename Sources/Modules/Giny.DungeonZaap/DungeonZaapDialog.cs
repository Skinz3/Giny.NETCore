using Giny.Protocol.Enums;
using Giny.Protocol.Types;
using Giny.World.Managers.Dialogs;
using Giny.World.Managers.Entities.Characters;
using Giny.World.Managers.Maps.Elements;
using Giny.World.Records.Maps;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Giny.DungeonZaap
{
    public class DungeonZaapDialog : ZaapDialog
    {
        public override TeleporterTypeEnum TeleporterType => TeleporterTypeEnum.TELEPORTER_ZAAP;
        public DungeonZaapDialog(Character character) : base(character)
        {
            foreach (var dungeon in DungeonRecord.GetDungeonRecords())
            {
                if (dungeon.Rooms.Count <= 1)
                {
                    continue;
                }

                var mapId = dungeon.Rooms.Keys.FirstOrDefault();

                var targetMap = MapRecord.GetMap(mapId);

                if (targetMap == null)
                {
                    continue;
                }


                Destinations.Add(mapId, new TeleportDestination()
                {
                    cost = GetCost(targetMap, character.Map),
                    level = 1,
                    type = (byte)TeleporterType,
                    mapId = targetMap.Id,
                    subAreaId = targetMap.SubareaId,
                });

            }
        }

        public override short GetCost(MapRecord teleporterMap, MapRecord currentMap)
        {
            return (short)(teleporterMap.Dungeon.OptimalPlayerLevel * 100);
        }
        public override void Open()
        {
            base.Open();
        }

        public override void Teleport(MapRecord map)
        {
            base.Teleport(map);
        }
    }
}
