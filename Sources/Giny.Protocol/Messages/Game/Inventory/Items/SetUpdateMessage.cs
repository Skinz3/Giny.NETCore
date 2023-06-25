using System.Collections.Generic;
using Giny.Core.Network.Messages;
using Giny.Protocol.Types;
using Giny.Core.IO.Interfaces;
using Giny.Protocol;
using Giny.Protocol.Enums;

namespace Giny.Protocol.Messages
{ 
    public class SetUpdateMessage : NetworkMessage  
    { 
        public  const ushort Id = 2482;
        public override ushort MessageId => Id;

        public short setId;
        public short[] setObjects;
        public ObjectEffect[] setEffects;

        public SetUpdateMessage()
        {
        }
        public SetUpdateMessage(short setId,short[] setObjects,ObjectEffect[] setEffects)
        {
            this.setId = setId;
            this.setObjects = setObjects;
            this.setEffects = setEffects;
        }
        public override void Serialize(IDataWriter writer)
        {
            if (setId < 0)
            {
                throw new System.Exception("Forbidden value (" + setId + ") on element setId.");
            }

            writer.WriteVarShort((short)setId);
            writer.WriteShort((short)setObjects.Length);
            for (uint _i2 = 0;_i2 < setObjects.Length;_i2++)
            {
                if (setObjects[_i2] < 0)
                {
                    throw new System.Exception("Forbidden value (" + setObjects[_i2] + ") on element 2 (starting at 1) of setObjects.");
                }

                writer.WriteVarShort((short)setObjects[_i2]);
            }

            writer.WriteShort((short)setEffects.Length);
            for (uint _i3 = 0;_i3 < setEffects.Length;_i3++)
            {
                writer.WriteShort((short)(setEffects[_i3] as ObjectEffect).TypeId);
                (setEffects[_i3] as ObjectEffect).Serialize(writer);
            }

        }
        public override void Deserialize(IDataReader reader)
        {
            uint _val2 = 0;
            uint _id3 = 0;
            ObjectEffect _item3 = null;
            setId = (short)reader.ReadVarUhShort();
            if (setId < 0)
            {
                throw new System.Exception("Forbidden value (" + setId + ") on element of SetUpdateMessage.setId.");
            }

            uint _setObjectsLen = (uint)reader.ReadUShort();
            setObjects = new short[_setObjectsLen];
            for (uint _i2 = 0;_i2 < _setObjectsLen;_i2++)
            {
                _val2 = (uint)reader.ReadVarUhShort();
                if (_val2 < 0)
                {
                    throw new System.Exception("Forbidden value (" + _val2 + ") on elements of setObjects.");
                }

                setObjects[_i2] = (short)_val2;
            }

            uint _setEffectsLen = (uint)reader.ReadUShort();
            for (uint _i3 = 0;_i3 < _setEffectsLen;_i3++)
            {
                _id3 = (uint)reader.ReadUShort();
                _item3 = ProtocolTypeManager.GetInstance<ObjectEffect>((short)_id3);
                _item3.Deserialize(reader);
                setEffects[_i3] = _item3;
            }

        }


    }
}








