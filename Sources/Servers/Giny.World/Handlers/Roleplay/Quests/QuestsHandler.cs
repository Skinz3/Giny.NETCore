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
            var activeQuests = new QuestActiveInformations[1];
            activeQuests[0] = new QuestActiveDetailedInformations(2083, new QuestObjectiveInformations[]
            {
                new QuestObjectiveInformations(8107,false,new string[0]),
                       new QuestObjectiveInformations(8108,true,new string[] {"hello"})
            }, 1463);



            client.Send(new QuestListMessage(new short[0], new short[0], activeQuests, new short[0]));
        }

        [MessageHandler]
        public static void HandleQuestStepInfoRequestMessage(QuestStepInfoRequestMessage message, WorldClient client)
        {
            
            client.Send(new QuestStepInfoMessage(new QuestActiveDetailedInformations(2083, new QuestObjectiveInformations[]
            {
                new QuestObjectiveInformations(8107,true,new string[] {"hello"}),
                 new QuestObjectiveInformations(8108,true,new string[] {"hello"})
            }, 1463)));
        }
    }
}
