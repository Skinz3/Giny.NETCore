using System.Collections.Generic;
using Giny.Core.IO.Interfaces;
using Giny.Protocol;
using Giny.Protocol.Enums;

namespace Giny.Protocol.Types
{ 
    public class ObjectItemInformationWithQuantity : ObjectItemMinimalInformation  
    { 
        public new const ushort Id = 2220;
        public override ushort TypeId => Id;

        public int quantity;

        public ObjectItemInformationWithQuantity()
        {
        }
        public ObjectItemInformationWithQuantity(int quantity,short objectGID,ObjectEffect[] effects)
        {
            this.quantity = quantity;
            this.objectGID = objectGID;
            this.effects = effects;
        }
        public override void Serialize(IDataWriter writer)
        {
            base.Serialize(writer);
            if (quantity < 0)
            {
                throw new System.Exception("Forbidden value (" + quantity + ") on element quantity.");
            }

            writer.WriteVarInt((int)quantity);
        }
        public override void Deserialize(IDataReader reader)
        {
            base.Deserialize(reader);
            quantity = (int)reader.ReadVarUhInt();
            if (quantity < 0)
            {
                throw new System.Exception("Forbidden value (" + quantity + ") on element of ObjectItemInformationWithQuantity.quantity.");
            }

        }


    }
}








