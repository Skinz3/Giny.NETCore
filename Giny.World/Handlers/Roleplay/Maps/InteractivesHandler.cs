using Giny.Core.Network.Messages;
using Giny.Protocol.Messages;
using Giny.World.Network;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Giny.World.Handlers.Roleplay.Maps
{
    class InteractivesHandler
    {
        [MessageHandler]
        public static void HandleInteractiveUse(InteractiveUseRequestMessage message, WorldClient client)
        {
            client.Character.Map.Instance.UseInteractive(client.Character, message.elemId, message.skillInstanceUid);
        }
    }
}
