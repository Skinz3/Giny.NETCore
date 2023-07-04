using System.Collections.Generic;
using Giny.Core.IO.Interfaces;
using Giny.Protocol;
using Giny.Protocol.Enums;

namespace Giny.Protocol.Types
{
    public class NuggetsBeneficiary
    {
        public const ushort Id = 6606;
        public virtual ushort TypeId => Id;

        public long beneficiaryPlayerId;
        public int nuggetsQuantity;

        public NuggetsBeneficiary()
        {
        }
        public NuggetsBeneficiary(long beneficiaryPlayerId, int nuggetsQuantity)
        {
            this.beneficiaryPlayerId = beneficiaryPlayerId;
            this.nuggetsQuantity = nuggetsQuantity;
        }
        public virtual void Serialize(IDataWriter writer)
        {
            if (beneficiaryPlayerId < 0 || beneficiaryPlayerId > 9007199254740992)
            {
                throw new System.Exception("Forbidden value (" + beneficiaryPlayerId + ") on element beneficiaryPlayerId.");
            }

            writer.WriteVarLong((long)beneficiaryPlayerId);
            writer.WriteInt((int)nuggetsQuantity);
        }
        public virtual void Deserialize(IDataReader reader)
        {
            beneficiaryPlayerId = (long)reader.ReadVarUhLong();
            if (beneficiaryPlayerId < 0 || beneficiaryPlayerId > 9007199254740992)
            {
                throw new System.Exception("Forbidden value (" + beneficiaryPlayerId + ") on element of NuggetsBeneficiary.beneficiaryPlayerId.");
            }

            nuggetsQuantity = (int)reader.ReadInt();
        }


    }
}


