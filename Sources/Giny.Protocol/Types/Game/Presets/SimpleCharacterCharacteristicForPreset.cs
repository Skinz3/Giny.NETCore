using System.Collections.Generic;
using Giny.Core.IO.Interfaces;
using Giny.Protocol;
using Giny.Protocol.Enums;

namespace Giny.Protocol.Types
{
    public class SimpleCharacterCharacteristicForPreset
    {
        public const ushort Id = 4462;
        public virtual ushort TypeId => Id;

        public string keyword;
        public int @base;
        public int additionnal;

        public SimpleCharacterCharacteristicForPreset()
        {
        }
        public SimpleCharacterCharacteristicForPreset(string keyword, int @base, int additionnal)
        {
            this.keyword = keyword;
            this.@base = @base;
            this.additionnal = additionnal;
        }
        public virtual void Serialize(IDataWriter writer)
        {
            writer.WriteUTF((string)keyword);
            writer.WriteVarInt((int)@base);
            writer.WriteVarInt((int)additionnal);
        }
        public virtual void Deserialize(IDataReader reader)
        {
            keyword = (string)reader.ReadUTF();
            @base = (int)reader.ReadVarInt();
            additionnal = (int)reader.ReadVarInt();
        }


    }
}


