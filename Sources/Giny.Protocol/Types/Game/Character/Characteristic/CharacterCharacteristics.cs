using System.Collections.Generic;
using Giny.Core.IO.Interfaces;
using Giny.Protocol;
using Giny.Protocol.Enums;

namespace Giny.Protocol.Types
{ 
    public class CharacterCharacteristics  
    { 
        public const ushort Id = 9333;
        public virtual ushort TypeId => Id;

        public CharacterCharacteristic[] characteristics;

        public CharacterCharacteristics()
        {
        }
        public CharacterCharacteristics(CharacterCharacteristic[] characteristics)
        {
            this.characteristics = characteristics;
        }
        public virtual void Serialize(IDataWriter writer)
        {
            writer.WriteShort((short)characteristics.Length);
            for (uint _i1 = 0;_i1 < characteristics.Length;_i1++)
            {
                writer.WriteShort((short)(characteristics[_i1] as CharacterCharacteristic).TypeId);
                (characteristics[_i1] as CharacterCharacteristic).Serialize(writer);
            }

        }
        public virtual void Deserialize(IDataReader reader)
        {
            uint _id1 = 0;
            CharacterCharacteristic _item1 = null;
            uint _characteristicsLen = (uint)reader.ReadUShort();
            for (uint _i1 = 0;_i1 < _characteristicsLen;_i1++)
            {
                _id1 = (uint)reader.ReadUShort();
                _item1 = ProtocolTypeManager.GetInstance<CharacterCharacteristic>((short)_id1);
                _item1.Deserialize(reader);
                characteristics[_i1] = _item1;
            }

        }


    }
}








