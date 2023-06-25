using Giny.Core.IO.Interfaces;
using Giny.Core.Network.IPC;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Giny.Protocol.IPC.Messages
{
    public class IsIpConnectedRequestMessage : IPCMessage
    {
        public const ushort Id = 14;

        public override ushort MessageId => Id;

        public string ip;

        public IsIpConnectedRequestMessage(string ip)
        {
            this.ip = ip;
        }
        public IsIpConnectedRequestMessage()
        {

        }
        public override void Deserialize(IDataReader reader)
        {
            ip = reader.ReadUTF();
        }

        public override void Serialize(IDataWriter writer)
        {
            writer.WriteUTF(ip);
        }
    }
}
