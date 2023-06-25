using System.Collections.Generic;
using Giny.Core.IO.Interfaces;
using Giny.Protocol;
using Giny.Protocol.Enums;

namespace Giny.Protocol.Types
{ 
    public class ServerSessionConstantString : ServerSessionConstant  
    { 
        public new const ushort Id = 3340;
        public override ushort TypeId => Id;

        public string value;

        public ServerSessionConstantString()
        {
        }
        public ServerSessionConstantString(string value,short id)
        {
            this.value = value;
            this.id = id;
        }
        public override void Serialize(IDataWriter writer)
        {
            base.Serialize(writer);
            writer.WriteUTF((string)value);
        }
        public override void Deserialize(IDataReader reader)
        {
            base.Deserialize(reader);
            value = (string)reader.ReadUTF();
        }


    }
}








