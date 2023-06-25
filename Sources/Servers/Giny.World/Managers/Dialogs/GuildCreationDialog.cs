using Giny.Protocol.Enums;
using Giny.Protocol.Messages;
using Giny.World.Managers.Entities.Characters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Giny.World.Managers.Dialogs
{
    public class GuildCreationDialog : Dialog
    {
        public override DialogTypeEnum DialogType
        {
            get
            {
                return DialogTypeEnum.DIALOG_GUILD_CREATE;
            }
        }

        public GuildCreationDialog(Character character)
            : base(character)
        {

        }
        public override void Open()
        {
            Character.Client.Send(new GuildCreationStartedMessage());
        }
    }
}
