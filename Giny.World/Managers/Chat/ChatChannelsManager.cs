using Giny.Core.DesignPattern;
using Giny.Protocol.Enums;
using Giny.World.Network;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;
using Giny.Protocol.Messages;
using Giny.Protocol.Types;
using Giny.Core.Extensions;
using Giny.Core.Network.Messages;

namespace Giny.World.Managers.Chat
{
    public class ChatChannelsManager : Singleton<ChatChannelsManager>
    {
        public delegate void ChatHandlerDelegate(WorldClient client, ChatServerMessage message);
        private readonly Dictionary<ChatActivableChannelsEnum, ChatHandlerDelegate> ChatHandlers = new Dictionary<ChatActivableChannelsEnum, ChatHandlerDelegate>();

        [StartupInvoke("Chat Channels", StartupInvokePriority.SixthPath)]
        public void Initialize()
        {
            foreach (var method in typeof(ChatChannels).GetMethods())
            {
                var attribute = method.GetCustomAttribute<ChatChannelHandlerAttribute>();

                if (attribute != null)
                {
                    ChatHandlers.Add(attribute.Channel, (ChatHandlerDelegate)Delegate.CreateDelegate(typeof(ChatHandlerDelegate), method));
                }
            }

        }
        public void Handle(WorldClient client, ChatServerMessage chatMessage)
        {
            if (chatMessage.content.StartsWith(ChatCommandsManager.COMMANDS_PREFIX))
            {
                ChatCommandsManager.Instance.Handle(chatMessage.content.Remove(0, 1), client, client);
                return;
            }

            var channel = (ChatActivableChannelsEnum)chatMessage.channel;

            if (ChatHandlers.ContainsKey(channel))
            {
                ChatHandlers[channel].Invoke(client, chatMessage);
            }
            else
            {
                client.Character.Reply("Ce chat n'est pas géré");
            }
        }

        public ChatServerCopyWithObjectMessage GetServerCopyWithObjectMessage(ChatActivableChannelsEnum channel, ObjectItem[] items, string message, WorldClient target)
        {
            return new ChatServerCopyWithObjectMessage(items, (byte)channel, message, DateTime.Now.GetUnixTimeStamp(),
                 string.Empty, target.Character.Id, target.Character.Name);
        }
        public ChatServerWithObjectMessage GetChatServerWithObjectMessage(ChatActivableChannelsEnum channel, ObjectItem[] items, string message, WorldClient client)
        {
            return new ChatServerWithObjectMessage(items, (byte)channel, message, DateTime.Now.GetUnixTimeStamp(),
                 string.Empty, client.Character.Id, client.Character.Name, string.Empty, client.Account.Id);
        }
        public ChatServerMessage GetChatServerMessage(ChatActivableChannelsEnum channel, string message, WorldClient client)
        {
            return new ChatServerMessage(client.Character.Id, client.Character.Name, string.Empty,
                client.Account.Id, (byte)channel, message, DateTime.Now.GetUnixTimeStamp(), string.Empty);
        }

        public ChatServerCopyMessage GetChatServerCopyMessage(ChatActivableChannelsEnum channel, string message, WorldClient target)
        {
            return new ChatServerCopyMessage(target.Character.Id, target.Character.Name, (byte)channel,
                 message, DateTime.Now.GetUnixTimeStamp(), string.Empty);
        }
    }
}
