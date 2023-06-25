
using Giny.Core.IO.Interfaces;
using Giny.Core.Network.IPC;
using Giny.Protocol.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Giny.Protocol.IPC.Messages
{
    public class IPCServerStatusUpdateMessage : IPCMessage
    {
        public const ushort Id = 12;
        public override ushort MessageId
        {
            get
            {
                return Id;
            }
        }
        public ServerStatusEnum status;

        public IPCServerStatusUpdateMessage()
        {

        }

        public IPCServerStatusUpdateMessage(ServerStatusEnum status)
        {
            this.status = status;
        }
        public override void Serialize(IDataWriter writer)
        {
            writer.WriteByte((byte)status);
        }

        public override void Deserialize(IDataReader reader)
        {
            

            this.status = (ServerStatusEnum)reader.ReadByte();
        }
    }
}
