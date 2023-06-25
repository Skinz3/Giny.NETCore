using Giny.Protocol.Enums;
using Giny.Protocol.Messages;
using Giny.World.Managers.Entities.Characters;
using Giny.World.Managers.Exchanges;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Giny.World.Managers.Dialogs.DialogBox
{
    public class PlayerTradeRequestBox : RequestBox
    {
        public PlayerTradeRequestBox(Character source, Character target)
            : base(source, target)
        {
        }
        protected override void OnOpen()
        {
            this.Send(new ExchangeRequestedTradeMessage(Source.Id, Target.Id, (byte)ExchangeTypeEnum.PLAYER_TRADE));
        }
        protected override void OnAccept()
        {
            Source.Dialog = new PlayerTradeExchange(base.Source, base.Target);
            Target.Dialog = new PlayerTradeExchange(base.Target, base.Source);
            Source.Dialog.Open();
        }
        protected override void OnDeny()
        {
            Send(new ExchangeLeaveMessage(false, (byte)DialogTypeEnum.DIALOG_EXCHANGE));
        }
        protected override void OnCancel()
        {
            base.Deny();
        }
    }
}
