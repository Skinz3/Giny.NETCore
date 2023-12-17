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
        public static void HandleQuestListRequestMessage(QuestListRequestMessage message, WorldClient client)
        {
            var activeQuests = client.Character.GetActiveQuests().Select(x => x.GetQuestActiveInformations()).ToArray();

            var finishedQuests = client.Character.GetFinishedQuests().Select(x => (short)x.QuestId).ToArray();

            client.Send(new QuestListMessage(finishedQuests, new short[0], activeQuests, new short[0]));
        }

        [MessageHandler]
        public static void HandleQuestStepInfoRequestMessage(QuestStepInfoRequestMessage message, WorldClient client)
        {
            if (message.questId == 0)
            {
                foreach (var quest in client.Character.GetActiveQuests())
                {
                    client.Send(new QuestStepInfoMessage(quest.GetQuestActiveInformations()));
                }
                return;
            }
            else
            {
                var quest = client.Character.GetQuest(message.questId);

                client.Send(new QuestStepInfoMessage(quest.GetQuestActiveInformations()));
            }

           
        }
    }
}
