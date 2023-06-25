using System.Collections.Generic;
using Giny.Core.IO.Interfaces;
using Giny.Protocol;
using Giny.Protocol.Enums;

namespace Giny.Protocol.Types
{ 
    public class AdditionalTaxCollectorInformations  
    { 
        public const ushort Id = 1493;
        public virtual ushort TypeId => Id;

        public string collectorCallerName;
        public int date;

        public AdditionalTaxCollectorInformations()
        {
        }
        public AdditionalTaxCollectorInformations(string collectorCallerName,int date)
        {
            this.collectorCallerName = collectorCallerName;
            this.date = date;
        }
        public virtual void Serialize(IDataWriter writer)
        {
            writer.WriteUTF((string)collectorCallerName);
            if (date < 0)
            {
                throw new System.Exception("Forbidden value (" + date + ") on element date.");
            }

            writer.WriteInt((int)date);
        }
        public virtual void Deserialize(IDataReader reader)
        {
            collectorCallerName = (string)reader.ReadUTF();
            date = (int)reader.ReadInt();
            if (date < 0)
            {
                throw new System.Exception("Forbidden value (" + date + ") on element of AdditionalTaxCollectorInformations.date.");
            }

        }


    }
}








