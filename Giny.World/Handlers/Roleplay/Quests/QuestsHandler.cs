using Giny.Core.Network.Messages;
using Giny.Protocol.Messages;
using Giny.Protocol.Types;
using Giny.World.Network;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Giny.World.Handlers.Quests
{
    class QuestsHandler
    {
        [MessageHandler]
        public static void HandleQuestListRequestMessage(QuestListRequestMessage message,WorldClient client)
        {
            client.Send(new QuestListMessage(new short[0], new short[0], new QuestActiveInformations[0], new short[0]));
        }
    }
}
