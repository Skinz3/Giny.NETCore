using Giny.Core.Network.Messages;
using Giny.Protocol.Messages;
using Giny.World.Network;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Giny.World.Handlers.Roleplay.Idols
{
    class IdolsHandler
    {
        [MessageHandler]
        public static void HandleIdolPartyRegisterRequest(IdolPartyRegisterRequestMessage message, WorldClient client)
        {
            client.Character.RefreshIdols();
        }

        [MessageHandler]
        public static void HandleIdolSelectRequest(IdolSelectRequestMessage message, WorldClient client)
        {
            client.Character.IdolsInventory.Update(client.Character);
            client.Character.SelectIdol(message.idolId, message.activate,message.party);
        }
    }
}
