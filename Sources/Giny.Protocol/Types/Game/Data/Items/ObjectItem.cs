using System.Collections.Generic;
using Giny.Core.IO.Interfaces;
using Giny.Protocol;
using Giny.Protocol.Enums;

namespace Giny.Protocol.Types
{ 
    public class ObjectItem : Item  
    { 
        public new const ushort Id = 8568;
        public override ushort TypeId => Id;

        public short position;
        public short objectGID;
        public ObjectEffect[] effects;
        public int objectUID;
        public int quantity;

        public ObjectItem()
        {
        }
        public ObjectItem(short position,short objectGID,ObjectEffect[] effects,int objectUID,int quantity)
        {
            this.position = position;
            this.objectGID = objectGID;
            this.effects = effects;
            this.objectUID = objectUID;
            this.quantity = quantity;
        }
        public override void Serialize(IDataWriter writer)
        {
            base.Serialize(writer);
            writer.WriteShort((short)position);
            if (objectGID < 0)
            {
                throw new System.Exception("Forbidden value (" + objectGID + ") on element objectGID.");
            }

            writer.WriteVarShort((short)objectGID);
            writer.WriteShort((short)effects.Length);
            for (uint _i3 = 0;_i3 < effects.Length;_i3++)
            {
                writer.WriteShort((short)(effects[_i3] as ObjectEffect).TypeId);
                (effects[_i3] as ObjectEffect).Serialize(writer);
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
            uint _id3 = 0;
            ObjectEffect _item3 = null;
            base.Deserialize(reader);
            position = (short)reader.ReadShort();
            if (position < 0)
            {
                throw new System.Exception("Forbidden value (" + position + ") on element of ObjectItem.position.");
            }

            objectGID = (short)reader.ReadVarUhShort();
            if (objectGID < 0)
            {
                throw new System.Exception("Forbidden value (" + objectGID + ") on element of ObjectItem.objectGID.");
            }

            uint _effectsLen = (uint)reader.ReadUShort();
            for (uint _i3 = 0;_i3 < _effectsLen;_i3++)
            {
                _id3 = (uint)reader.ReadUShort();
                _item3 = ProtocolTypeManager.GetInstance<ObjectEffect>((short)_id3);
                _item3.Deserialize(reader);
                effects[_i3] = _item3;
            }

            objectUID = (int)reader.ReadVarUhInt();
            if (objectUID < 0)
            {
                throw new System.Exception("Forbidden value (" + objectUID + ") on element of ObjectItem.objectUID.");
            }

            quantity = (int)reader.ReadVarUhInt();
            if (quantity < 0)
            {
                throw new System.Exception("Forbidden value (" + quantity + ") on element of ObjectItem.quantity.");
            }

        }


    }
}








