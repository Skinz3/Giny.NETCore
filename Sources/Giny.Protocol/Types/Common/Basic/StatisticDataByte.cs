using System.Collections.Generic;
using Giny.Core.IO.Interfaces;
using Giny.Protocol;
using Giny.Protocol.Enums;

namespace Giny.Protocol.Types
{ 
    public class StatisticDataByte : StatisticData  
    { 
        public new const ushort Id = 7113;
        public override ushort TypeId => Id;

        public byte value;

        public StatisticDataByte()
        {
        }
        public StatisticDataByte(byte value)
        {
            this.value = value;
        }
        public override void Serialize(IDataWriter writer)
        {
            base.Serialize(writer);
            writer.WriteByte((byte)value);
        }
        public override void Deserialize(IDataReader reader)
        {
            base.Deserialize(reader);
            value = (byte)reader.ReadByte();
        }


    }
}








