using Giny.Core.Network.Messages;
using Giny.Protocol.Messages;
using Giny.World.Network;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Giny.World.Handlers.Roleplay.Arena
{
    class ArenaHandler
    {
        [MessageHandler]
        public static void HandleGameRolePlayArenaRegisterMessage(GameRolePlayArenaRegisterMessage message, WorldClient client)
        {
            //     client.Character.RegisterArena();

        }

        [MessageHandler]
        public static void HandleGameRolePlayArenaUnregisterMessage(GameRolePlayArenaUnregisterMessage message, WorldClient client)
        {
            //  client.Character.UnregisterArena();
        }

        [MessageHandler]
        public static void HandleGameRolePlayArenaFightAnswerMessage(GameRolePlayArenaFightAnswerMessage message, WorldClient client)
        {
            //   client.Character.AnwserArena(message.accept);
        }
    }
}
