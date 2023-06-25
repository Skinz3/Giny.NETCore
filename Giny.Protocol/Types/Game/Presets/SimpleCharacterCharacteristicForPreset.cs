using System.Collections.Generic;
using Giny.Core.IO.Interfaces;
using Giny.Protocol;
using Giny.Protocol.Enums;

namespace Giny.Protocol.Types
{ 
    public class SimpleCharacterCharacteristicForPreset  
    { 
        public const ushort Id = 7594;
        public virtual ushort TypeId => Id;

        public string keyword;
        public short @base;
        public short additionnal;

        public SimpleCharacterCharacteristicForPreset()
        {
        }
        public SimpleCharacterCharacteristicForPreset(string keyword,short @base,short additionnal)
        {
            this.keyword = keyword;
            this.@base = @base;
            this.additionnal = additionnal;
        }
        public virtual void Serialize(IDataWriter writer)
        {
            writer.WriteUTF((string)keyword);
            writer.WriteVarShort((short)@base);
            writer.WriteVarShort((short)additionnal);
        }
        public virtual void Deserialize(IDataReader reader)
        {
            keyword = (string)reader.ReadUTF();
            @base = (short)reader.ReadVarShort();
            additionnal = (short)reader.ReadVarShort();
        }


    }
}








