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
            client.Character.Record.SpawnPointMapId = client.Character.Map.Id;
            client.Send(new ZaapRespawnUpdatedMessage(client.Character.Record.SpawnPointMapId));
        }
    }
}
