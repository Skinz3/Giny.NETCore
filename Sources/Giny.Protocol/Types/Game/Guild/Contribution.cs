using System.Collections.Generic;
using Giny.Core.IO.Interfaces;
using Giny.Protocol;
using Giny.Protocol.Enums;

namespace Giny.Protocol.Types
{
    public class Contribution
    {
        public const ushort Id = 7253;
        public virtual ushort TypeId => Id;

        public long contributorId;
        public string contributorName;
        public long amount;

        public Contribution()
        {
        }
        public Contribution(long contributorId, string contributorName, long amount)
        {
            this.contributorId = contributorId;
            this.contributorName = contributorName;
            this.amount = amount;
        }
        public virtual void Serialize(IDataWriter writer)
        {
            if (contributorId < 0 || contributorId > 9007199254740992)
            {
                throw new System.Exception("Forbidden value (" + contributorId + ") on element contributorId.");
            }

            writer.WriteVarLong((long)contributorId);
            writer.WriteUTF((string)contributorName);
            if (amount < 0 || amount > 9007199254740992)
            {
                throw new System.Exception("Forbidden value (" + amount + ") on element amount.");
            }

            writer.WriteVarLong((long)amount);
        }
        public virtual void Deserialize(IDataReader reader)
        {
            contributorId = (long)reader.ReadVarUhLong();
            if (contributorId < 0 || contributorId > 9007199254740992)
            {
                throw new System.Exception("Forbidden value (" + contributorId + ") on element of Contribution.contributorId.");
            }

            contributorName = (string)reader.ReadUTF();
            amount = (long)reader.ReadVarUhLong();
            if (amount < 0 || amount > 9007199254740992)
            {
                throw new System.Exception("Forbidden value (" + amount + ") on element of Contribution.amount.");
            }

        }


    }
}


