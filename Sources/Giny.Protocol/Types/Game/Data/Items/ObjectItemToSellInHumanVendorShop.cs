using System.Collections.Generic;
using Giny.Core.IO.Interfaces;
using Giny.Protocol;
using Giny.Protocol.Enums;

namespace Giny.Protocol.Types
{ 
    public class ObjectItemToSellInHumanVendorShop : Item  
    { 
        public new const ushort Id = 3410;
        public override ushort TypeId => Id;

        public short objectGID;
        public ObjectEffect[] effects;
        public int objectUID;
        public int quantity;
        public long objectPrice;
        public long publicPrice;

        public ObjectItemToSellInHumanVendorShop()
        {
        }
        public ObjectItemToSellInHumanVendorShop(short objectGID,ObjectEffect[] effects,int objectUID,int quantity,long objectPrice,long publicPrice)
        {
            this.objectGID = objectGID;
            this.effects = effects;
            this.objectUID = objectUID;
            this.quantity = quantity;
            this.objectPrice = objectPrice;
            this.publicPrice = publicPrice;
        }
        public override void Serialize(IDataWriter writer)
        {
            base.Serialize(writer);
            if (objectGID < 0)
            {
                throw new System.Exception("Forbidden value (" + objectGID + ") on element objectGID.");
            }

            writer.WriteVarShort((short)objectGID);
            writer.WriteShort((short)effects.Length);
            for (uint _i2 = 0;_i2 < effects.Length;_i2++)
            {
                writer.WriteShort((short)(effects[_i2] as ObjectEffect).TypeId);
                (effects[_i2] as ObjectEffect).Serialize(writer);
            }

            if (objectUID < 0)
            {
                throw new System.Exception("Forbidden value (" + objectUID + ") on element objectUID.");
            }

            writer.WriteVarInt((int)objectUID);
            if (quantity < 0)
            {
                throw new System.Exception("Forbidden value (" + quantity + ") on element quantity.");
            }

            writer.WriteVarInt((int)quantity);
            if (objectPrice < 0 || objectPrice > 9.00719925474099E+15)
            {
                throw new System.Exception("Forbidden value (" + objectPrice + ") on element objectPrice.");
            }

            writer.WriteVarLong((long)objectPrice);
            if (publicPrice < 0 || publicPrice > 9.00719925474099E+15)
            {
                throw new System.Exception("Forbidden value (" + publicPrice + ") on element publicPrice.");
            }

            writer.WriteVarLong((long)publicPrice);
        }
        public override void Deserialize(IDataReader reader)
        {
            uint _id2 = 0;
            ObjectEffect _item2 = null;
            base.Deserialize(reader);
            objectGID = (short)reader.ReadVarUhShort();
            if (objectGID < 0)
            {
                throw new System.Exception("Forbidden value (" + objectGID + ") on element of ObjectItemToSellInHumanVendorShop.objectGID.");
            }

            uint _effectsLen = (uint)reader.ReadUShort();
            for (uint _i2 = 0;_i2 < _effectsLen;_i2++)
            {
                _id2 = (uint)reader.ReadUShort();
                _item2 = ProtocolTypeManager.GetInstance<ObjectEffect>((short)_id2);
                _item2.Deserialize(reader);
                effects[_i2] = _item2;
            }

            objectUID = (int)reader.ReadVarUhInt();
            if (objectUID < 0)
            {
                throw new System.Exception("Forbidden value (" + objectUID + ") on element of ObjectItemToSellInHumanVendorShop.objectUID.");
            }

            quantity = (int)reader.ReadVarUhInt();
            if (quantity < 0)
            {
                throw new System.Exception("Forbidden value (" + quantity + ") on element of ObjectItemToSellInHumanVendorShop.quantity.");
            }

            objectPrice = (long)reader.ReadVarUhLong();
            if (objectPrice < 0 || objectPrice > 9.00719925474099E+15)
            {
                throw new System.Exception("Forbidden value (" + objectPrice + ") on element of ObjectItemToSellInHumanVendorShop.objectPrice.");
            }

            publicPrice = (long)reader.ReadVarUhLong();
            if (publicPrice < 0 || publicPrice > 9.00719925474099E+15)
            {
                throw new System.Exception("Forbidden value (" + publicPrice + ") on element of ObjectItemToSellInHumanVendorShop.publicPrice.");
            }

        }


    }
}








