using Giny.Protocol.Custom.Enums;
using Giny.Protocol.Messages;
using Giny.World.Managers.Dialogs.DialogBox;
using Giny.World.Managers.Entities.Characters;
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
                Source.Guild.Join(Target, false);
            }
            SendGuildInvitationRecruter(Source, GuildInvitationStateEnum.GUILD_INVITATION_OK);
            SendGuildInvitationRecruter(Target, GuildInvitationStateEnum.GUILD_INVITATION_OK);
            base.OnAccept();
        }
        protected override void OnDeny()
        {
            SendGuildInvitationRecruter(Source, GuildInvitationStateEnum.GUILD_INVITATION_CANCELED);
            SendGuildInvitationRecruter(Target, GuildInvitationStateEnum.GUILD_INVITATION_CANCELED);
            base.OnDeny();
        }
        protected override void OnOpen()
        {
            SendGuildInvitationRecruter(Source, GuildInvitationStateEnum.GUILD_INVITATION_SENT);
            Target.Client.Send(new GuildInvitationStateRecrutedMessage((byte)GuildInvitationStateEnum.GUILD_INVITATION_SENT));

            Target.Client.Send(new GuildInvitedMessage(Source.Id, Source.Name, Source.Guild.GetBasicGuildInformations()));

            base.OnOpen();
        }
        protected override void OnCancel()
        {
            SendGuildInvitationRecruter(Source, GuildInvitationStateEnum.GUILD_INVITATION_CANCELED);
            SendGuildInvitationRecruter(Target, GuildInvitationStateEnum.GUILD_INVITATION_CANCELED);
            base.OnCancel();
        }

        private void SendGuildInvitationRecruter(Character character, GuildInvitationStateEnum state)
        {
            character.Client.Send(new GuildInvitationStateRecruterMessage(Target.Name, (byte)state));
        }
    }
}
