using System.Collections.Generic;
using Giny.Core.IO.Interfaces;
using Giny.Protocol;
using Giny.Protocol.Enums;

namespace Giny.Protocol.Types
{ 
    public class DebtInformation  
    { 
        public const ushort Id = 149;
        public virtual ushort TypeId => Id;

        public double id;
        public double timestamp;

        public DebtInformation()
        {
        }
        public DebtInformation(double id,double timestamp)
        {
            this.id = id;
            this.timestamp = timestamp;
        }
        public virtual void Serialize(IDataWriter writer)
        {
            if (id < 0 || id > 9.00719925474099E+15)
            {
                throw new System.Exception("Forbidden value (" + id + ") on element id.");
            }

            writer.WriteDouble((double)id);
            if (timestamp < 0 || timestamp > 9.00719925474099E+15)
            {
                throw new System.Exception("Forbidden value (" + timestamp + ") on element timestamp.");
            }

            writer.WriteDouble((double)timestamp);
        }
        public virtual void Deserialize(IDataReader reader)
        {
            id = (double)reader.ReadDouble();
            if (id < 0 || id > 9.00719925474099E+15)
            {
                throw new System.Exception("Forbidden value (" + id + ") on element of DebtInformation.id.");
            }

            timestamp = (double)reader.ReadDouble();
            if (timestamp < 0 || timestamp > 9.00719925474099E+15)
            {
                throw new System.Exception("Forbidden value (" + timestamp + ") on element of DebtInformation.timestamp.");
            }

        }


    }
}








