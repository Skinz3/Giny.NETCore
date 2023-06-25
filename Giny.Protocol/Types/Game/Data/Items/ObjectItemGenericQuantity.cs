using System.Collections.Generic;
using Giny.Core.IO.Interfaces;
using Giny.Protocol;
using Giny.Protocol.Enums;

namespace Giny.Protocol.Types
{ 
    public class ObjectItemGenericQuantity : Item  
    { 
        public new const ushort Id = 2677;
        public override ushort TypeId => Id;

        public short objectGID;
        public int quantity;

        public ObjectItemGenericQuantity()
        {
        }
        public ObjectItemGenericQuantity(short objectGID,int quantity)
        {
            this.objectGID = objectGID;
            this.quantity = quantity;
        }
        public override void Serialize(IDataWriter writer)
        {
            base.Serialize(writer);
            if (objectGID < 0)
            {
                throw new System.Exception("Forbidden value (" + objectGID + ") on element objectGID.");
            }

            writer.WriteVarShort((short)objectGID);
            if (quantity < 0)
            {
                throw new System.Exception("Forbidden value (" + quantity + ") on element quantity.");
            }

            writer.WriteVarInt((int)quantity);
        }
        public override void Deserialize(IDataReader reader)
        {
            base.Deserialize(reader);
            objectGID = (short)reader.ReadVarUhShort();
            if (objectGID < 0)
            {
                throw new System.Exception("Forbidden value (" + objectGID + ") on element of ObjectItemGenericQuantity.objectGID.");
            }

            quantity = (int)reader.ReadVarUhInt();
            if (quantity < 0)
            {
                throw new System.Exception("Forbidden value (" + quantity + ") on element of ObjectItemGenericQuantity.quantity.");
            }

        }


    }
}








