using System.Collections.Generic;
using Giny.Core.IO.Interfaces;
using Giny.Protocol;
using Giny.Protocol.Enums;

namespace Giny.Protocol.Types
{ 
    public class StatisticDataString : StatisticData  
    { 
        public new const ushort Id = 6838;
        public override ushort TypeId => Id;

        public string value;

        public StatisticDataString()
        {
        }
        public StatisticDataString(string value)
        {
            this.value = value;
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








