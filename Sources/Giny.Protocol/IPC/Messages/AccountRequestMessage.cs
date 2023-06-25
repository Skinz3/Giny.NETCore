using Giny.Core.IO.Interfaces;
using Giny.Core.Network.IPC;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Giny.Protocol.IPC.Messages
{
    public class AccountRequestMessage : IPCMessage
    {
        public const ushort Id = 3;

        public override ushort MessageId
        {
            get
            {
                return Id;
            }
        }

        public string ticket;

        public AccountRequestMessage(string ticket)
        {
            this.ticket = ticket;
        }
        public AccountRequestMessage()
        {

        }
        public override void Serialize(IDataWriter writer)
        {
            writer.WriteUTF(ticket);
        }

        public override void Deserialize(IDataReader reader)
        {
            this.ticket = reader.ReadUTF();
        }
    }
}
