using Giny.Core.Network.Messages;
using Giny.Protocol.Enums;
using Giny.Protocol.Messages;
using Giny.World.Managers.Dialogs;
using Giny.World.Network;
using Giny.World.Records.Maps;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Giny.World.Handlers.Roleplay.Maps
{
    class TeleporterHandler
    {
        [MessageHandler]
        public static void HandleTeleportRequestMessage(TeleportRequestMessage message, WorldClient client)
        {
            TeleporterDialog dialog = client.Character.GetDialog<TeleporterDialog>();

            if (dialog != null)
            {
                dialog.Teleport(MapRecord.GetMap((long)message.mapId));
            }
        }
        [MessageHandler]
        public static void HandleZaapRespawnSaveRequest(ZaapRespawnSaveRequestMessage message, WorldClient client)
        {
            var dialog = client.Character.GetDialog<ZaapDialog>();

            if (dialog == null || dialog.TeleporterType != TeleporterTypeEnum.TELEPORTER_ZAAP)
            {
                client.Character.TextInformation(TextInformationTypeEnum.TEXT_INFORMATION_ERROR, 243);
                return;
            }

            client.Character.Record.SpawnPointMapId = client.Character.Map.Id;
            client.Send(new ZaapRespawnUpdatedMessage(client.Character.Record.SpawnPointMapId));
        }
    }
}
