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
    public class HandshakeMessage : IPCMessage
    {
        public const ushort Id = 6;

        public override ushort MessageId
        {
            get
            {
                return Id;
            }
        }
        public short serverId;

        public HandshakeMessage()
        {

        }

        public HandshakeMessage(short serverId)
        {
            this.serverId = serverId;
        }
        public override void Serialize(IDataWriter writer)
        {
            writer.WriteShort(serverId);
        }

        public override void Deserialize(IDataReader reader)
        {
            this.serverId = reader.ReadShort();
        }
    }
}
