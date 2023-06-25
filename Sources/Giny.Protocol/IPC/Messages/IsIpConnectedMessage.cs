using Giny.Core.IO.Interfaces;
using Giny.Core.Network.IPC;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Giny.Protocol.IPC.Messages
{
    public class IsIpConnectedMessage : IPCMessage
    {
        public const ushort Id = 13;

        public override ushort MessageId => Id;

        public bool connected;

        public IsIpConnectedMessage(bool connected)
        {
            this.connected = connected;
        }
        public IsIpConnectedMessage()
        {

        }

        public override void Deserialize(IDataReader reader)
        {
            connected = reader.ReadBoolean();
        }

        public override void Serialize(IDataWriter writer)
        {
            writer.WriteBoolean(connected);
        }
    }
}
