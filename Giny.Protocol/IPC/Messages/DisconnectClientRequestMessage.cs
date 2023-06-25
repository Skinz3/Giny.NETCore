using Giny.Core.IO.Interfaces;
using Giny.Core.Network.IPC;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Giny.Protocol.IPC.Messages
{
    public class DisconnectClientRequestMessage : IPCMessage
    {
        public const ushort Id = 4;

        public override ushort MessageId
        {
            get
            {
                return Id;
            }
        }
        public int accountId;

        public DisconnectClientRequestMessage(int accountId)
        {
            this.accountId = accountId;
        }
        public DisconnectClientRequestMessage()
        {

        }

        public override void Serialize(IDataWriter writer)
        {
            writer.WriteInt(accountId);
        }

        public override void Deserialize(IDataReader reader)
        {
            this.accountId = reader.ReadInt();
        }
    }
}
