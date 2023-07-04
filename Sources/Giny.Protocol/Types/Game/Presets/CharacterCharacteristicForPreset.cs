using System.Collections.Generic;
using Giny.Core.IO.Interfaces;
using Giny.Protocol;
using Giny.Protocol.Enums;

namespace Giny.Protocol.Types
{
    public class CharacterCharacteristicForPreset : SimpleCharacterCharacteristicForPreset
    {
        public new const ushort Id = 6828;
        public override ushort TypeId => Id;

        public int stuff;

        public CharacterCharacteristicForPreset()
        {
        }
        public CharacterCharacteristicForPreset(int stuff, string keyword, int @base, int additionnal)
        {
            this.stuff = stuff;
            this.keyword = keyword;
            this.@base = @base;
            this.additionnal = additionnal;
        }
        public override void Serialize(IDataWriter writer)
        {
            base.Serialize(writer);
            writer.WriteVarInt((int)stuff);
        }
        public override void Deserialize(IDataReader reader)
        {
            base.Deserialize(reader);
            stuff = (int)reader.ReadVarInt();
        }


    }
}


