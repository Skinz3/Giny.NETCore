using System.Collections.Generic;
using Giny.Core.IO.Interfaces;
using Giny.Protocol;
using Giny.Protocol.Enums;

namespace Giny.Protocol.Types
{ 
    public class ObjectItemNotInContainer : Item  
    { 
        public new const ushort Id = 1294;
        public override ushort TypeId => Id;

        public short objectGID;
        public ObjectEffect[] effects;
        public int objectUID;
        public int quantity;

        public ObjectItemNotInContainer()
        {
        }
        public ObjectItemNotInContainer(short objectGID,ObjectEffect[] effects,int objectUID,int quantity)
        {
            this.objectGID = objectGID;
            this.effects = effects;
            this.objectUID = objectUID;
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
        }
        public override void Deserialize(IDataReader reader)
        {
            uint _id2 = 0;
            ObjectEffect _item2 = null;
            base.Deserialize(reader);
            objectGID = (short)reader.ReadVarUhShort();
            if (objectGID < 0)
            {
                throw new System.Exception("Forbidden value (" + objectGID + ") on element of ObjectItemNotInContainer.objectGID.");
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
                throw new System.Exception("Forbidden value (" + objectUID + ") on element of ObjectItemNotInContainer.objectUID.");
            }

            quantity = (int)reader.ReadVarUhInt();
            if (quantity < 0)
            {
                throw new System.Exception("Forbidden value (" + quantity + ") on element of ObjectItemNotInContainer.quantity.");
            }

        }


    }
}








