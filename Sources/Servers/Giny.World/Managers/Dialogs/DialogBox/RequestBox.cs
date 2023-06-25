using Giny.Core.Network.Messages;
using Giny.World.Managers.Entities.Characters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Giny.World.Managers.Dialogs.DialogBox
{
    public abstract class RequestBox
    {
        public Character Source
        {
            get;
            protected set;
        }
        public Character Target
        {
            get;
            protected set;
        }
        protected RequestBox(Character source, Character target)
        {
            this.Source = source;
            this.Target = target;
        }
        public void Send(NetworkMessage message)
        {
            Source.Client.Send(message);
            Target.Client.Send(message);
        }
        public void Open()
        {
            this.OnOpen();
        }
        protected virtual void OnOpen()
        {
        }
        public void Accept()
        {
            this.OnAccept();
            this.Close();
        }
        protected virtual void OnAccept()
        {
        }
        public void Deny()
        {
            this.OnDeny();
            this.Close();
        }
        protected virtual void OnDeny()
        {
        }
        public void Cancel()
        {
            this.OnCancel();
            this.Close();
        }
        protected virtual void OnCancel()
        {
        }
        protected void Close()
        {
            this.Source.RequestBox = null;
            this.Target.RequestBox = null;
        }

    }
}
