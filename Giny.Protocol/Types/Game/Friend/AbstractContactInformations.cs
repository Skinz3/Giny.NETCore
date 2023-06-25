using System.Collections.Generic;
using Giny.Core.IO.Interfaces;
using Giny.Protocol;
using Giny.Protocol.Enums;

namespace Giny.Protocol.Types
{ 
    public class AbstractContactInformations  
    { 
        public const ushort Id = 7523;
        public virtual ushort TypeId => Id;

        public int accountId;
        public AccountTagInformation accountTag;

        public AbstractContactInformations()
        {
        }
        public AbstractContactInformations(int accountId,AccountTagInformation accountTag)
        {
            this.accountId = accountId;
            this.accountTag = accountTag;
        }
        public virtual void Serialize(IDataWriter writer)
        {
            if (accountId < 0)
            {
                throw new System.Exception("Forbidden value (" + accountId + ") on element accountId.");
            }

            writer.WriteInt((int)accountId);
            accountTag.Serialize(writer);
        }
        public virtual void Deserialize(IDataReader reader)
        {
            accountId = (int)reader.ReadInt();
            if (accountId < 0)
            {
                throw new System.Exception("Forbidden value (" + accountId + ") on element of AbstractContactInformations.accountId.");
            }

            accountTag = new AccountTagInformation();
            accountTag.Deserialize(reader);
        }


    }
}








