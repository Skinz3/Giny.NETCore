using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Giny.Core.Network.Messages;
using Giny.Protocol.Enums;
using Giny.Protocol.Messages;
using Giny.World.Managers.Chat;
using Giny.World.Network;

namespace Giny.World.Handlers.Roleplay.Social
{
    class ChatHandler
    {
        [MessageHandler]
        public static void HandleChatClientMultiWithObject(ChatClientMultiWithObjectMessage message, WorldClient client)
        {
            var chatMessage = ChatChannelsManager.Instance.GetChatServerWithObjectMessage((ChatActivableChannelsEnum)message.channel, message.objects, message.content, client);
            ChatChannelsManager.Instance.Handle(client, chatMessage);
        }
        [MessageHandler]
        public static void HandleChatSmileyRequestMessage(ChatSmileyRequestMessage message, WorldClient client)
        {
            client.Character.DisplaySmiley(message.smileyId);
        }
        [MessageHandler]
        public static void HandleChatMultiClient(ChatClientMultiMessage message, WorldClient client)
        {
            var chatMessage = ChatChannelsManager.Instance.GetChatServerMessage((ChatActivableChannelsEnum)message.channel, message.content, client);
            ChatChannelsManager.Instance.Handle(client, chatMessage);
        }

        [MessageHandler]
        public static void ChatClientPrivate(ChatClientPrivateMessage message, WorldClient client)
        {
            var target = WorldServer.Instance.GetClient(message.receiver);

            if (target == client)
            {
                client.Character.OnChatError(ChatErrorEnum.CHAT_ERROR_INTERIOR_MONOLOGUE);
                return;
            }

            if (target != null)
            {
                target.Send(ChatChannelsManager.Instance.GetChatServerMessage(ChatActivableChannelsEnum.PSEUDO_CHANNEL_PRIVATE, message.content, client));
                client.Send(ChatChannelsManager.Instance.GetChatServerCopyMessage(ChatActivableChannelsEnum.PSEUDO_CHANNEL_PRIVATE, message.content, target));
            }
            else
            {
                client.Character.OnChatError(ChatErrorEnum.CHAT_ERROR_RECEIVER_NOT_FOUND);
            }

        }

        [MessageHandler]
        public static void HandleChatClientPrivateWithObject(ChatClientPrivateWithObjectMessage message, WorldClient client)
        {
            var target = WorldServer.Instance.GetClient(message.receiver);

            if (target == client)
            {
                client.Character.OnChatError(ChatErrorEnum.CHAT_ERROR_INTERIOR_MONOLOGUE);
                return;
            }

            if (target != null)
            {
                client.Send(ChatChannelsManager.Instance.GetServerCopyWithObjectMessage(ChatActivableChannelsEnum.PSEUDO_CHANNEL_PRIVATE, message.objects, message.content, target));
                target.Send(ChatChannelsManager.Instance.GetChatServerWithObjectMessage(ChatActivableChannelsEnum.PSEUDO_CHANNEL_PRIVATE, message.objects, message.content, client));
            }
            else
            {
                client.Character.OnChatError(ChatErrorEnum.CHAT_ERROR_RECEIVER_NOT_FOUND);
            }
        }


    }
}
