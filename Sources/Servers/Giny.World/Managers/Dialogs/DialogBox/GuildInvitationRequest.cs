using Giny.Protocol.Custom.Enums;
using Giny.Protocol.Enums;
using Giny.Protocol.Messages;
using Giny.World.Managers.Dialogs.DialogBox;
using Giny.World.Managers.Entities.Characters;
using Giny.World.Managers.Guilds;
using Giny.World.Network;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Giny.World.Managers.Dialogs
{
    public class GuildInvitationRequest : RequestBox
    {
        public GuildInvitationRequest(Character source, Character target) : base(source, target)
        {
        }
        protected override void OnAccept()
        {
            if (Source.Guild != null)
            {
                Source.Guild.Join(Target, Guild.NEWBIE_RANK_ID);
            }

            SendGuildInvitationRecruter(Source, GuildInvitationStateEnum.GUILD_INVITATION_OK);
            SendGuildInvitationRecruted(Target, GuildInvitationStateEnum.GUILD_INVITATION_OK);
            base.OnAccept();
        }
        protected override void OnDeny()
        {
            Source.TextInformation(TextInformationTypeEnum.TEXT_INFORMATION_MESSAGE, 1440, Target.Name);

            SendGuildInvitationRecruter(Source, GuildInvitationStateEnum.GUILD_INVITATION_CANCELED);
            SendGuildInvitationRecruted(Target, GuildInvitationStateEnum.GUILD_INVITATION_CANCELED);
            base.OnDeny();
        }
        protected override void OnOpen()
        {
            SendGuildInvitationRecruter(Source, GuildInvitationStateEnum.GUILD_INVITATION_SENT);
            SendGuildInvitationRecruted(Target, GuildInvitationStateEnum.GUILD_INVITATION_SENT);

            Target.Client.Send(new GuildInvitedMessage(Source.Name, Source.Guild.GetGuildInformations()));

            base.OnOpen();
        }
        protected override void OnCancel()
        {
            Source.TextInformation(TextInformationTypeEnum.TEXT_INFORMATION_MESSAGE, 1441, Target.Name);

            SendGuildInvitationRecruter(Source, GuildInvitationStateEnum.GUILD_INVITATION_CANCELED);
            SendGuildInvitationRecruted(Target, GuildInvitationStateEnum.GUILD_INVITATION_CANCELED);
            base.OnCancel();
        }

        private void SendGuildInvitationRecruter(Character character, GuildInvitationStateEnum state)
        {
            character.Client.Send(new GuildInvitationStateRecruterMessage(Target.Name, (byte)state));
        }

        private void SendGuildInvitationRecruted(Character character, GuildInvitationStateEnum state)
        {
            character.Client.Send(new GuildInvitationStateRecrutedMessage((byte)state));
        }
    }
}