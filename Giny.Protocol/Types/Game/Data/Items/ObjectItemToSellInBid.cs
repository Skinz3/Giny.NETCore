using System.Collections.Generic;
using Giny.Core.IO.Interfaces;
using Giny.Protocol;
using Giny.Protocol.Enums;

namespace Giny.Protocol.Types
{ 
    public class ObjectItemToSellInBid : ObjectItemToSell  
    { 
        public new const ushort Id = 712;
        public override ushort TypeId => Id;

        public int unsoldDelay;

        public ObjectItemToSellInBid()
        {
        }
        public ObjectItemToSellInBid(int unsoldDelay,short objectGID,ObjectEffect[] effects,int objectUID,int quantity,long objectPrice)
        {
            this.unsoldDelay = unsoldDelay;
            this.objectGID = objectGID;
            this.effects = effects;
            this.objectUID = objectUID;
            this.quantity = quantity;
            this.objectPrice = objectPrice;
        }
        public override void Serialize(IDataWriter writer)
        {
            base.Serialize(writer);
            if (unsoldDelay < 0)
            {
                throw new System.Exception("Forbidden value (" + unsoldDelay + ") on element unsoldDelay.");
            }

            writer.WriteInt((int)unsoldDelay);
        }
        public override void Deserialize(IDataReader reader)
        {
            base.Deserialize(reader);
            unsoldDelay = (int)reader.ReadInt();
            if (unsoldDelay < 0)
            {
                throw new System.Exception("Forbidden value (" + unsoldDelay + ") on element of ObjectItemToSellInBid.unsoldDelay.");
            }

        }


    }
}








