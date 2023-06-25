using System.Collections.Generic;
using Giny.Core.IO.Interfaces;
using Giny.Protocol;
using Giny.Protocol.Enums;

namespace Giny.Protocol.Types
{ 
    public class PaddockInformations  
    { 
        public const ushort Id = 5079;
        public virtual ushort TypeId => Id;

        public short maxOutdoorMount;
        public short maxItems;

        public PaddockInformations()
        {
        }
        public PaddockInformations(short maxOutdoorMount,short maxItems)
        {
            this.maxOutdoorMount = maxOutdoorMount;
            this.maxItems = maxItems;
        }
        public virtual void Serialize(IDataWriter writer)
        {
            if (maxOutdoorMount < 0)
            {
                throw new System.Exception("Forbidden value (" + maxOutdoorMount + ") on element maxOutdoorMount.");
            }

            writer.WriteVarShort((short)maxOutdoorMount);
            if (maxItems < 0)
            {
                throw new System.Exception("Forbidden value (" + maxItems + ") on element maxItems.");
            }

            writer.WriteVarShort((short)maxItems);
        }
        public virtual void Deserialize(IDataReader reader)
        {
            maxOutdoorMount = (short)reader.ReadVarUhShort();
            if (maxOutdoorMount < 0)
            {
                throw new System.Exception("Forbidden value (" + maxOutdoorMount + ") on element of PaddockInformations.maxOutdoorMount.");
            }

            maxItems = (short)reader.ReadVarUhShort();
            if (maxItems < 0)
            {
                throw new System.Exception("Forbidden value (" + maxItems + ") on element of PaddockInformations.maxItems.");
            }

        }


    }
}








