using Giny.Protocol.Custom.Enums;
using Giny.Protocol.Enums;
using Giny.Protocol.Messages;
using Giny.World.Managers.Dialogs;
using Giny.World.Managers.Entities.Characters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Giny.World.Managers.Exchanges
{
    public abstract class Exchange : Dialog
    {
        public override DialogTypeEnum DialogType
        {
            get
            {
                return DialogTypeEnum.DIALOG_EXCHANGE;
            }
        }
        public abstract ExchangeTypeEnum ExchangeType
        {
            get;
        }

        protected bool Succes = false;

        public Exchange(Character character)
            : base(character)
        {

        }

        public abstract void MoveItemPriced(int objectUID, int quantity, long price);

        public abstract void ModifyItemPriced(int objectUID, int quantity, long price);

        public abstract void MoveItem(int uid, int quantity);

        public abstract void Ready(bool ready, short step);

        public abstract void MoveKamas(long quantity);

        public override void Close()
        {
            Character.Client.Send(new ExchangeLeaveMessage()
            {
                dialogType = (byte)DialogType,
                success = Succes,
            });
            base.Close();
        }

        public abstract void OnNpcGenericAction(NpcActionsEnum action);
    }
}
