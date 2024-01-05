using Giny.Core.Network.Messages;
using Giny.Protocol.Custom.Enums;
using Giny.Protocol.Enums;
using Giny.Protocol.Messages;
using Giny.Protocol.Types;
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
        public static void HandleGuildBulletinSetRequestMessage(GuildBulletinSetRequestMessage message, WorldClient client)
        {
            if (client.Character.HasGuild)
            {
                client.Character.Guild.SetBulletin(client.Character, message.content);
            }
        }

        [MessageHandler]
        public static void HandleCreateGuildRankRequestMessage(CreateGuildRankRequestMessage message, WorldClient client)
        {
            if (client.Character.HasGuild)
            {
                Guild guild = client.Character.Guild;
                GuildMemberRecord member = client.Character.GuildMember;

                if (member.HasRight(GuildRightsEnum.RIGHT_MANAGE_RANKS_AND_RIGHTS, guild))
                {
                    GuildRankRecord parentRank = guild.GetGuildRank(message.parentRankId);

                    if (parentRank == null)
                        guild.CreateRank(guild.GetRankNextOrder(), message.gfxId, true, new GuildRightsEnum[0], guild.GetRankNextId(), message.name);
                    else
                        guild.CreateRank(guild.GetRankNextOrder(), message.gfxId, true, parentRank.Rights, guild.GetRankNextId(), message.name);
                }
            }
        }

        [MessageHandler]
        public static void HandleUpdateGuildRankRequestMessage(UpdateGuildRankRequestMessage message, WorldClient client)
        {
            if (client.Character.HasGuild)
            {
                GuildMemberRecord member = client.Character.GuildMember;

                if (member != null)
                {
                    client.Character.Guild.ChangeRank(member, message.rank);
                }
            }
        }

        [MessageHandler]
        public static void HandleUpdateGuildRightsMessage(UpdateGuildRightsMessage message, WorldClient client)
        {
            if (client.Character.HasGuild)
            {
                GuildMemberRecord member = client.Character.GuildMember;

                if (member != null)
                {
                    client.Character.Guild.ChangeRights(member, message.rankId, message.rights);
                }
            }
        }

        [MessageHandler]
        public static void HandleGuildGetPlayerApplicationMessage(GuildGetPlayerApplicationMessage message, WorldClient client)
        {
            client.Send(new GuildPlayerNoApplicationInformationMessage());
        }

        [MessageHandler]
        public static void HandleGuildRanksRequestMessage(GuildRanksRequestMessage message, WorldClient client)
        {
            if (client.Character.HasGuild)
            {
                client.Send(client.Character.Guild.GetGuildRanksMessage());
            }
        }

        [MessageHandler]
        public static void HandleGuildSummaryRequestMessage(GuildSummaryRequestMessage message, WorldClient client)
        {
            client.Send(GuildsManager.Instance.GetGuildSummaryMessage());
        }

        [MessageHandler]
        public static void HandleGuildFactsRequestMessage(GuildFactsRequestMessage message, WorldClient client)
        {
            var guild = GuildsManager.Instance.GetGuild(message.guildId);

            if (guild != null)
            {
                client.Send(guild.GetGuildFactsMessage());
            }
        }

        [MessageHandler]
        public static void HandleGuildLogbookInformationRequestMessage(GuildLogbookInformationRequestMessage message, WorldClient client)
        {
            if (client.Character.HasGuild)
            {
                client.Send(client.Character.Guild.GetGuildLogbookInformationMessage());
            }
        }

        [MessageHandler]
        public static void HandleGuildGetInformationsMessage(GuildGetInformationsMessage message, WorldClient client)
        {
            if (!client.Character.HasGuild)
                return;

            client.Character.RefreshGuild();

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
                    client.Send(client.Character.Guild.GetGuildInformationsPaddocksMessage());
                    break;
                case GuildInformationsTypeEnum.INFO_HOUSES:
                    client.Send(client.Character.Guild.GetGuildHousesInformationMessage());
                    break;



                case GuildInformationsTypeEnum.INFO_RECRUITMENT:
                    client.Send(client.Character.Guild.GetRecruitmentInformationMessage());
                    break;
                default:
                    client.Character.ReplyWarning("Unhandled " + (GuildInformationsTypeEnum)message.infoType);
                    break;
            }
        }

        [MessageHandler]
        public static void HandleGuildUpdateNoteMessage(GuildUpdateNoteMessage message, WorldClient client)
        {
            if (client.Character.HasGuild)
            {
                GuildMemberRecord member = client.Character.GuildMember;
                GuildMemberRecord target = client.Character.Guild.Record.GetMember(message.memberId);

                if (member != null)
                {
                    client.Character.Guild.ChangeNote(member, target, message.note);
                }
            }
        }

        [MessageHandler]
        public static void HandleGuildChangeMemberParameters(GuildChangeMemberParametersMessage message, WorldClient client)
        {
            if (client.Character.HasGuild)
            {
                GuildMemberRecord member = client.Character.GuildMember;
                GuildMemberRecord target = client.Character.Guild.Record.GetMember(message.memberId);

                if (member != null)
                {
                    client.Character.Guild.ChangeParameters(member, target, message.experienceGivenPercent, message.rankId);
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

            if (client.Character.HasGuild && client.Character.HasRequestBoxOpen<GuildInvitationRequest>())
            {
                if (!message.accept)
                    client.Character.RequestBox.Cancel();
            }
        }

        [MessageHandler]
        public static void HandleGuildInvitation(GuildInvitationMessage message, WorldClient client)
        {
            if (client.Character.GuildMember.HasRight(GuildRightsEnum.RIGHT_MANAGE_APPLY_AND_INVITATION, client.Character.Guild))
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