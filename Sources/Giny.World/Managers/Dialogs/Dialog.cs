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
    public abstract class Dialog
    {
        public Character Character
        {
            get;
            set;
        }

        public Dialog(Character character)
        {
            this.Character = character;
        }

        public abstract DialogTypeEnum DialogType
        {
            get;
        }

        public abstract void Open();

        public virtual void Close()
        {
            Character.Client.Character.Dialog = null;
        }
        protected void LeaveDialogMessage()
        {
            Character.Client.Send(new LeaveDialogMessage((byte)DialogType));
        }
    }
}
