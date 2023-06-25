using System.Collections.Generic;
using Giny.Core.IO.Interfaces;
using Giny.Protocol;
using Giny.Protocol.Enums;

namespace Giny.Protocol.Types
{ 
    public class CharacterCharacteristicForPreset : SimpleCharacterCharacteristicForPreset  
    { 
        public new const ushort Id = 1673;
        public override ushort TypeId => Id;

        public short stuff;

        public CharacterCharacteristicForPreset()
        {
        }
        public CharacterCharacteristicForPreset(short stuff,string keyword,short @base,short additionnal)
        {
            this.stuff = stuff;
            this.keyword = keyword;
            this.@base = @base;
            this.additionnal = additionnal;
        }
        public override void Serialize(IDataWriter writer)
        {
            base.Serialize(writer);
            writer.WriteVarShort((short)stuff);
        }
        public override void Deserialize(IDataReader reader)
        {
            base.Deserialize(reader);
            stuff = (short)reader.ReadVarShort();
        }


    }
}








