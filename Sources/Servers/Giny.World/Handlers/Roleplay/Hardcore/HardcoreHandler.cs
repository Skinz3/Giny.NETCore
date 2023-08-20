using Giny.Core.Network.Messages;
using Giny.Protocol.Messages;
using Giny.World.Network;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Giny.World.Handlers.Roleplay.Hardcore
{
    public class HardcoreHandler
    {
        [MessageHandler]
        public static void HandleGameRolePlayFreeSoulRequest(GameRolePlayFreeSoulRequestMessage message, WorldClient client)
        {
            client.Send(new GameRolePlayGameOverMessage());
        }

    }
}
