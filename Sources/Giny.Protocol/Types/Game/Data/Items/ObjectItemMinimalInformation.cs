using System.Collections.Generic;
using Giny.Core.IO.Interfaces;
using Giny.Protocol;
using Giny.Protocol.Enums;

namespace Giny.Protocol.Types
{ 
    public class ObjectItemMinimalInformation : Item  
    { 
        public new const ushort Id = 1570;
        public override ushort TypeId => Id;

        public short objectGID;
        public ObjectEffect[] effects;

        public ObjectItemMinimalInformation()
        {
        }
        public ObjectItemMinimalInformation(short objectGID,ObjectEffect[] effects)
        {
            this.objectGID = objectGID;
            this.effects = effects;
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

        }
        public override void Deserialize(IDataReader reader)
        {
            uint _id2 = 0;
            ObjectEffect _item2 = null;
            base.Deserialize(reader);
            objectGID = (short)reader.ReadVarUhShort();
            if (objectGID < 0)
            {
                throw new System.Exception("Forbidden value (" + objectGID + ") on element of ObjectItemMinimalInformation.objectGID.");
            }

            uint _effectsLen = (uint)reader.ReadUShort();
            for (uint _i2 = 0;_i2 < _effectsLen;_i2++)
            {
                _id2 = (uint)reader.ReadUShort();
                _item2 = ProtocolTypeManager.GetInstance<ObjectEffect>((short)_id2);
                _item2.Deserialize(reader);
                effects[_i2] = _item2;
            }

        }


    }
}








