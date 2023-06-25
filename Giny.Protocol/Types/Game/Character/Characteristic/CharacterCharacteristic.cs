using System.Collections.Generic;
using Giny.Core.IO.Interfaces;
using Giny.Protocol;
using Giny.Protocol.Enums;

namespace Giny.Protocol.Types
{ 
    public class CharacterCharacteristic  
    { 
        public const ushort Id = 7204;
        public virtual ushort TypeId => Id;

        public short characteristicId;

        public CharacterCharacteristic()
        {
        }
        public CharacterCharacteristic(short characteristicId)
        {
            this.characteristicId = characteristicId;
        }
        public virtual void Serialize(IDataWriter writer)
        {
            writer.WriteShort((short)characteristicId);
        }
        public virtual void Deserialize(IDataReader reader)
        {
            characteristicId = (short)reader.ReadShort();
        }


    }
}








