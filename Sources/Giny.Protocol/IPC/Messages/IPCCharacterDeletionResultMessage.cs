
using Giny.Core.IO.Interfaces;
using Giny.Core.Network.IPC;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Giny.Protocol.IPC.Messages
{
    public class IPCCharacterDeletionResultMessage : IPCMessage
    {
        public static ushort Id = 10;

        public override ushort MessageId
        {
            get
            {
                return Id;
            }
        }
        public bool succes;

        public IPCCharacterDeletionResultMessage()
        {

        }

        public IPCCharacterDeletionResultMessage(bool succes)
        {
            this.succes = succes;
        }

        public override void Serialize(IDataWriter writer)
        {
            writer.WriteBoolean(succes);
        }

        public override void Deserialize(IDataReader reader)
        {
            this.succes = reader.ReadBoolean();
        }
    }
}
