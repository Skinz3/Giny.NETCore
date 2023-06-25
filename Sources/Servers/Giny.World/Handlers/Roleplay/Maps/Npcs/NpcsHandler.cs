using Giny.Core.Network.Messages;
using Giny.Protocol.Custom.Enums;
using Giny.Protocol.Enums;
using Giny.Protocol.Messages;
using Giny.World.Managers.Dialogs;
using Giny.World.Managers.Entities.Npcs;
using Giny.World.Managers.Exchanges;
using Giny.World.Network;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Giny.World.Handlers.Roleplay.Maps.Npcs
{
    class NpcsHandler
    {
        [MessageHandler]
        public static void HandleNpcGenericActionRequestMessage(NpcGenericActionRequestMessage message, WorldClient client)
        {
            if (message.npcMapId == client.Character.Map.Id)
            {
                if (client.Character.IsInDialog<Exchange>())
                {
                    client.Character.GetDialog<Exchange>().OnNpcGenericAction((NpcActionsEnum)message.npcActionId);
                }
                else
                {

                    Npc npc = client.Character.Map.Instance.GetEntity<Npc>((long)message.npcId);

                    if (npc != null)
                    {
                        npc.InteractWith(client.Character, (NpcActionsEnum)message.npcActionId);
                    }
                }
            }
            else
            {
                client.Character.ReplyError("Entity is not on map...");
            }
        }
        [MessageHandler]
        public static void HandleNpcDialogReplyMessage(NpcDialogReplyMessage message, WorldClient client)
        {
            if (client.Character.Dialog is NpcTalkDialog)
            {
                client.Character.GetDialog<NpcTalkDialog>().Reply(message.replyId);
            }
        }
    }
}
