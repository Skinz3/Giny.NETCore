using Giny.Protocol.Custom.Enums;
using Giny.Protocol.Enums;
using Giny.Protocol.Types;
using Giny.World.Managers.Entities.Characters;
using Giny.World.Managers.Maps;
using Giny.World.Managers.Maps.Elements;
using Giny.World.Managers.Maps.Teleporters;
using Giny.World.Records.Maps;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Giny.World.Managers.Dialogs
{
    public abstract class TeleporterDialog : Dialog
    {
        public override DialogTypeEnum DialogType
        {
            get
            {
                return DialogTypeEnum.DIALOG_TELEPORTER;
            }
        }
        public abstract TeleporterTypeEnum TeleporterType
        {
            get;
        }

        public Dictionary<long, TeleportDestination> Destinations
        {
            get;
            set;
        }
        public TeleporterDialog(Character character, MapElement element)
            : base(character)
        {
            var zaapMaps = TeleportersManager.Instance.GetMaps(TeleporterType, int.Parse(element.Param1));

            Destinations = new Dictionary<long, TeleportDestination>();

            for (int i = 0; i < zaapMaps.Count(); i++)
            {
                MapRecord targetMap = MapRecord.GetMap(zaapMaps[i]);

                Destinations.Add(targetMap.Id, new TeleportDestination()
                {
                    cost = GetCost(targetMap, character.Map),
                    level = 1,
                    type = (byte)TeleporterType,
                    mapId = targetMap.Id,
                    subAreaId = targetMap.SubareaId,
                });
            }
        }
        public override void Close()
        {
            base.Close();
            LeaveDialogMessage();
        }

        public abstract void Teleport(MapRecord map);

        public abstract short GetCost(MapRecord teleporterMap, MapRecord currentMap);
    }
}
