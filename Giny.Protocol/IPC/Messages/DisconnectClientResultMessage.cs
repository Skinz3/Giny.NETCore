
using Giny.Core.IO.Interfaces;
using Giny.Core.Network.IPC;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Giny.Protocol.IPC.Messages
{
    public class DisconnectClientResultMessage : IPCMessage
    {
        public const ushort Id = 5;

        public override ushort MessageId
        {
            get
            {
                return Id;
            }
        }

        public bool sucess;

        public DisconnectClientResultMessage()
        {

        }
        public DisconnectClientResultMessage(bool sucess)
        {
            this.sucess = sucess;
        }
        public override void Serialize(IDataWriter writer)
        {
            writer.WriteBoolean(sucess);
        }

        public override void Deserialize(IDataReader reader)
        {
            this.sucess = reader.ReadBoolean();
        }
    }
}
