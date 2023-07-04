using System.Collections.Generic;
using Giny.Core.IO.Interfaces;
using Giny.Protocol;
using Giny.Protocol.Enums;

namespace Giny.Protocol.Types
{
    public class AdditionalTaxCollectorInformation
    {
        public const ushort Id = 4816;
        public virtual ushort TypeId => Id;

        public long collectorCallerId;
        public string collectorCallerName;
        public int date;

        public AdditionalTaxCollectorInformation()
        {
        }
        public AdditionalTaxCollectorInformation(long collectorCallerId, string collectorCallerName, int date)
        {
            this.collectorCallerId = collectorCallerId;
            this.collectorCallerName = collectorCallerName;
            this.date = date;
        }
        public virtual void Serialize(IDataWriter writer)
        {
            if (collectorCallerId < 0 || collectorCallerId > 9007199254740992)
            {
                throw new System.Exception("Forbidden value (" + collectorCallerId + ") on element collectorCallerId.");
            }

            writer.WriteVarLong((long)collectorCallerId);
            writer.WriteUTF((string)collectorCallerName);
            if (date < 0)
            {
                throw new System.Exception("Forbidden value (" + date + ") on element date.");
            }

            writer.WriteInt((int)date);
        }
        public virtual void Deserialize(IDataReader reader)
        {
            collectorCallerId = (long)reader.ReadVarUhLong();
            if (collectorCallerId < 0 || collectorCallerId > 9007199254740992)
            {
                throw new System.Exception("Forbidden value (" + collectorCallerId + ") on element of AdditionalTaxCollectorInformation.collectorCallerId.");
            }

            collectorCallerName = (string)reader.ReadUTF();
            date = (int)reader.ReadInt();
            if (date < 0)
            {
                throw new System.Exception("Forbidden value (" + date + ") on element of AdditionalTaxCollectorInformation.date.");
            }

        }


    }
}


