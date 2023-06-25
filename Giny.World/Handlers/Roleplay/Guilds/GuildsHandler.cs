using Giny.Core.Network.Messages;
using Giny.Protocol.Custom.Enums;
using Giny.Protocol.Enums;
using Giny.Protocol.Messages;
using Giny.World.Managers.Dialogs;
using Giny.World.Managers.Entities.Characters;
using Giny.World.Managers.Guilds;
using Giny.World.Network;
using Giny.World.Records.Guilds;
using Giny.World.Records.Items;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Giny.World.Handlers.Roleplay.Guilds
{
    class GuildsHandler
    {
        [MessageHandler]
        public static void HandleGuildCreationRequest(GuildCreationValidMessage message, WorldClient client)
        {
            GuildCreationResultEnum result = GuildsManager.Instance.CreateGuild(client.Character, message.guildName, message.guildEmblem);
            client.Character.OnGuildCreate(result);

        }
        [MessageHandler]
        public static void HandleGuildMotdSetRequestMessage(GuildMotdSetRequestMessage message, WorldClient client)
        {
            if (client.Character.HasGuild)
            {
                client.Character.Guild.SetMotd(client.Character, message.content);
            }
        }

        [MessageHandler]
        public static void HandleGuildGetInformationsMessage(GuildGetInformationsMessage message, WorldClient client)
        {
            switch ((GuildInformationsTypeEnum)message.infoType)
            {
                case GuildInformationsTypeEnum.INFO_GENERAL:
                    client.Send(client.Character.Guild.GetGuildInformationsGeneralMessage());
                    break;
                case GuildInformationsTypeEnum.INFO_MEMBERS:
                    client.Send(client.Character.Guild.GetGuildInformationsMembersMessage());
                    break;
                case GuildInformationsTypeEnum.INFO_BOOSTS:
                    break;
                case GuildInformationsTypeEnum.INFO_PADDOCKS:
                    break;
                case GuildInformationsTypeEnum.INFO_HOUSES:
                    break;
                case GuildInformationsTypeEnum.INFO_TAX_COLLECTOR_GUILD_ONLY:
                    break;
                case GuildInformationsTypeEnum.INFO_TAX_COLLECTOR_ALLIANCE:
                    break;
                case GuildInformationsTypeEnum.INFO_TAX_COLLECTOR_LEAVE:
                    break;
                default:
                    break;
            }
        }
        [MessageHandler]
        public static void GuildChangeMemberParameters(GuildChangeMemberParametersMessage message, WorldClient client)
        {
            if (client.Character.HasGuild)
            {
                GuildMemberRecord member = client.Character.Guild.Record.GetMember((long)message.memberId);

                if (member != null)
                {
                    client.Character.Guild.ChangeParameters(member, message.experienceGivenPercent, message.rankId);
                }
            }
        }

        [MessageHandler]
        public static void HandleGuildKickRequestMessage(GuildKickRequestMessage message, WorldClient client)
        {
            if (!client.Character.HasGuild)
            {
                return;
            }

            GuildMemberRecord member = client.Character.Guild.Record.GetMember(message.kickedId);

            if (member != null)
            {
                client.Character.Guild.Leave(client.Character, member);
            }
        }
        [MessageHandler]
        public static void HandleGuildInvitationAnswer(GuildInvitationAnswerMessage message, WorldClient client)
        {
            if (!client.Character.HasGuild && client.Character.HasRequestBoxOpen<GuildInvitationRequest>())
            {
                if (message.accept)
                    client.Character.RequestBox.Accept();
                else
                    client.Character.RequestBox.Deny();
            }
        }
        [MessageHandler]
        public static void HandleGuildInvitation(GuildInvitationMessage message, WorldClient client)
        {
            if (client.Character.GuildMember.HasRight(GuildRightsEnum.RIGHT_MANAGE_APPLY_AND_INVITATION))
            {
                var target = WorldServer.Instance.GetOnlineClient(x => x.Character.Id == message.targetId);

                if (target == null)
                    client.Character.TextInformation(TextInformationTypeEnum.TEXT_INFORMATION_ERROR, 208);
                else if (target.Character.HasGuild)
                    client.Character.TextInformation(TextInformationTypeEnum.TEXT_INFORMATION_ERROR, 206);
                else if (target.Character.Busy)
                    client.Character.TextInformation(TextInformationTypeEnum.TEXT_INFORMATION_ERROR, 209);
                else if (!client.Character.Guild.CanAddMember())
                    client.Character.TextInformation(TextInformationTypeEnum.TEXT_INFORMATION_ERROR, 55, GuildsManager.MaxMemberCount);
                else
                {
                    target.Character.OpenRequestBox(new GuildInvitationRequest(client.Character, target.Character));
                }
            }
        }
    }
}
