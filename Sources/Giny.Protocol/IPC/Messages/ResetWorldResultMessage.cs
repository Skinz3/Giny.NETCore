using Giny.Core.IO.Interfaces;
using Giny.Core.Network.IPC;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Giny.Protocol.IPC.Messages
{
    public class ResetWorldResultMessage : IPCMessage
    {
        public const ushort Id = 17;

        public override ushort MessageId
        {
            get
            {
                return Id;
            }
        }

        public bool success;

        public ResetWorldResultMessage(bool success)
        {
            this.success = success;
        }
        public ResetWorldResultMessage()
        {

        }


        public override void Serialize(IDataWriter writer)
        {
            writer.WriteBoolean(success);
        }

        public override void Deserialize(IDataReader reader)
        {
            this.success = reader.ReadBoolean();
        }
    }
}
