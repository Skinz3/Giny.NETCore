using Giny.Core.IO.Interfaces;
using Giny.Core.Network.IPC;
using Giny.Protocol.IPC.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Giny.Protocol.IPC.Messages
{
    public class AccountMessage : IPCMessage
    {
        public const ushort Id = 2;

        public override ushort MessageId
        {
            get
            {
                return Id;
            }
        }

        public Account Account;

        public AccountMessage(Account account)
        {
            this.Account = account;
        }
        public AccountMessage()
        {

        }

        public override void Serialize(IDataWriter writer)
        {
            bool accountDefined = Account != null;

            writer.WriteBoolean(accountDefined);

            if (accountDefined)
                Account.Serialize(writer);
        }

        public override void Deserialize(IDataReader reader)
        {
            bool accountDefined = reader.ReadBoolean();

            if (accountDefined)
            {
                Account = new Account();
                Account.Deserialize(reader);
            }
        }
    }
}
