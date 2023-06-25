using Giny.Core.IO.Interfaces;
using Giny.Core.Network.IPC;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Giny.Protocol.IPC.Messages
{
    public class IPCCharacterDeletionRequestMessage : IPCMessage
    {
        public const ushort Id = 9;

        public override ushort MessageId
        {
            get
            {
                return Id; 
            }
        }

        public int accountId;

        public long characterId;

        public IPCCharacterDeletionRequestMessage(int accountId, long characterId)
        {
            this.accountId = accountId;
            this.characterId = characterId;
        }

        public IPCCharacterDeletionRequestMessage()
        {

        }

        public override void Serialize(IDataWriter writer)
        {
            writer.WriteInt(accountId);
            writer.WriteLong(characterId);
        }

        public override void Deserialize(IDataReader reader)
        {
            this.accountId = reader.ReadInt();
            this.characterId = reader.ReadLong();
        }
    }
}
