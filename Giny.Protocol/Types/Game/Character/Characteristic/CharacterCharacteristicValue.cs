using System.Collections.Generic;
using Giny.Core.IO.Interfaces;
using Giny.Protocol;
using Giny.Protocol.Enums;

namespace Giny.Protocol.Types
{ 
    public class CharacterCharacteristicValue : CharacterCharacteristic  
    { 
        public new const ushort Id = 9463;
        public override ushort TypeId => Id;

        public int total;

        public CharacterCharacteristicValue()
        {
        }
        public CharacterCharacteristicValue(int total,short characteristicId)
        {
            this.total = total;
            this.characteristicId = characteristicId;
        }
        public override void Serialize(IDataWriter writer)
        {
            base.Serialize(writer);
            writer.WriteInt((int)total);
        }
        public override void Deserialize(IDataReader reader)
        {
            base.Deserialize(reader);
            total = (int)reader.ReadInt();
        }


    }
}








