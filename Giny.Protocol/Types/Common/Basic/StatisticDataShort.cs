using System.Collections.Generic;
using Giny.Core.IO.Interfaces;
using Giny.Protocol;
using Giny.Protocol.Enums;

namespace Giny.Protocol.Types
{ 
    public class StatisticDataShort : StatisticData  
    { 
        public new const ushort Id = 7316;
        public override ushort TypeId => Id;

        public short value;

        public StatisticDataShort()
        {
        }
        public StatisticDataShort(short value)
        {
            this.value = value;
        }
        public override void Serialize(IDataWriter writer)
        {
            base.Serialize(writer);
            writer.WriteShort((short)value);
        }
        public override void Deserialize(IDataReader reader)
        {
            base.Deserialize(reader);
            value = (short)reader.ReadShort();
        }


    }
}








