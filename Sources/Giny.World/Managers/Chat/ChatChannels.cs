using Giny.Core.DesignPattern;
using Giny.Core.Network.Messages;
using Giny.Protocol.Custom.Enums;
using Giny.Protocol.Enums;
using Giny.Protocol.Messages;
using Giny.Protocol.Types;
using Giny.World.Network;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Giny.World.Managers.Chat
{
    [WIP("Muted")]
    class ChatChannels
    {
        public const double GlobalChannelIntervalSeconds = 20;

        [ChatChannelHandler(ChatActivableChannelsEnum.CHANNEL_GUILD)]
        public static void HandleChatGuild(WorldClient client, ChatServerMessage message)
        {
            if (client.Character.HasGuild)
            {
                client.Character.Guild.Send(message);
            }
        }
        [ChatChannelHandler(ChatActivableChannelsEnum.CHANNEL_PARTY)]
        public static void HandleChatParty(WorldClient client, ChatServerMessage message)
        {
            if (client.Character.HasParty)
            {
                client.Character.Party.Send(message);
            }
        }
        [ChatChannelHandler(ChatActivableChannelsEnum.CHANNEL_ADMIN)]
        public static void Admin(WorldClient client, ChatServerMessage message)
        {
            if (client.Account.Role == ServerRoleEnum.Administrator)
            {
                WorldServer.Instance.Send(message);
            }
        }
        [ChatChannelHandler(ChatActivableChannelsEnum.CHANNEL_SALES)]
        public static void Sales(WorldClient client, ChatServerMessage message)
        {
            if (HandleGlobalChannel(client, message, client.Character.LastSalesChatMessage))
            {
                client.Character.LastSalesChatMessage = DateTime.UtcNow;
            }
        }
        [ChatChannelHandler(ChatActivableChannelsEnum.CHANNEL_NOOB)]
        public static void Noob(WorldClient client,ChatServerMessage message)
        {
            WorldServer.Instance.Send(message);
        }
        [ChatChannelHandler(ChatActivableChannelsEnum.CHANNEL_SEEK)]
        public static void Seek(WorldClient client, ChatServerMessage message)
        {
            if (HandleGlobalChannel(client, message, client.Character.LastSeekChatMessage))
            {
                client.Character.LastSeekChatMessage = DateTime.UtcNow;
            }
        }

        private static bool HandleGlobalChannel(WorldClient client, ChatServerMessage message, DateTime lastMessageData)
        {
            if (lastMessageData == null)
            {
                WorldServer.Instance.Send(message);
                return true;
            }

            TimeSpan diff = DateTime.UtcNow - lastMessageData;

            if (diff.TotalSeconds < GlobalChannelIntervalSeconds)
            {
                client.Character.TextInformation(TextInformationTypeEnum.TEXT_INFORMATION_MESSAGE, 115, (short)(GlobalChannelIntervalSeconds - diff.TotalSeconds));
                return true;
            }
            else
            {
                WorldServer.Instance.Send(message);
                return true;
            }
        }
        [ChatChannelHandler(ChatActivableChannelsEnum.CHANNEL_GLOBAL)]
        public static void HandleChatGlobal(WorldClient client, ChatServerMessage message)
        {
            if (!client.Character.Fighting)
            {
                if (client.Character.Map != null)
                {
                    if (client.Character.Map.Instance.Mute && client.Account.Role == ServerRoleEnum.Player)
                    {
                        client.Character.TextInformation(TextInformationTypeEnum.TEXT_INFORMATION_MESSAGE, 113);
                        return;
                    }
                    client.Character.SendMap(message);
                }
                else
                    client.Character.OnChatError(ChatErrorEnum.CHAT_ERROR_INVALID_MAP);
            }
            else
            {
                client.Character.Fighter.Fight.Send(message);
            }
        }
    }
}
