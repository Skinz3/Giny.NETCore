using Giny.Core.Network.Messages;
using Giny.Protocol.Enums;
using Giny.Protocol.Messages;
using Giny.World.Network;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Giny.World.Handlers.Roleplay.Maps
{
    class DialogsHandler
    {
        [MessageHandler]
        public static void HandleLeaveDialogRequest(LeaveDialogRequestMessage message, WorldClient client)
        {
            if (client.Character.IsInDialog())
            {
                client.Send(new LeaveDialogMessage((byte)client.Character.Dialog.DialogType));
                client.Character.LeaveDialog();
            }

            if (client.Character.IsInRequest())
            {
                client.Character.RequestBox.Cancel();
            }
        }
    }
}
