using Giny.Protocol.Custom.Enums;
using Giny.Protocol.Enums;
using Giny.Protocol.Messages;
using Giny.World.Managers.Entities.Characters;
using Giny.World.Managers.Formulas;
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
    public class ZaapDialog : TeleporterDialog
    {
        public override TeleporterTypeEnum TeleporterType => TeleporterTypeEnum.TELEPORTER_ZAAP;

        public ZaapDialog(Character character, MapElement element)
            : base(character, element)
        {

        }

        public override void Open()
        {
            this.Character.Client.Send(new ZaapDestinationsMessage(Character.Record.SpawnPointMapId,
              (byte)TeleporterType, Destinations.Values.ToArray()));
        }

        public override void Teleport(MapRecord map)
        {
            if (!Destinations.ContainsKey(map.Id) || map.Id == Character.Map.Id)
            {
                return;
            }
            var destination = Destinations[map.Id];

            var zaapElement = map.GetFirstElementRecord(InteractiveTypeEnum.ZAAP16);

            if (zaapElement == null)
            {
                return;
            }

            short cellId = map.GetNearCell(InteractiveTypeEnum.ZAAP16);

            if (this.Character.RemoveKamas(destination.cost))
            {
                this.Character.OnKamasLost(destination.cost);

                this.Close();
                this.Character.Teleport(map.Id, cellId);
            }
        }



        public override short GetCost(MapRecord teleporterMap, MapRecord currentMap)
        {
            return TeleporterFormulas.Instance.GetZaapCost(teleporterMap, currentMap);
        }
    }
}
