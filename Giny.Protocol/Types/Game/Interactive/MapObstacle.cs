using System.Collections.Generic;
using Giny.Core.IO.Interfaces;
using Giny.Protocol;
using Giny.Protocol.Enums;

namespace Giny.Protocol.Types
{ 
    public class MapObstacle  
    { 
        public const ushort Id = 7300;
        public virtual ushort TypeId => Id;

        public short obstacleCellId;
        public byte state;

        public MapObstacle()
        {
        }
        public MapObstacle(short obstacleCellId,byte state)
        {
            this.obstacleCellId = obstacleCellId;
            this.state = state;
        }
        public virtual void Serialize(IDataWriter writer)
        {
            if (obstacleCellId < 0 || obstacleCellId > 559)
            {
                throw new System.Exception("Forbidden value (" + obstacleCellId + ") on element obstacleCellId.");
            }

            writer.WriteVarShort((short)obstacleCellId);
            writer.WriteByte((byte)state);
        }
        public virtual void Deserialize(IDataReader reader)
        {
            obstacleCellId = (short)reader.ReadVarUhShort();
            if (obstacleCellId < 0 || obstacleCellId > 559)
            {
                throw new System.Exception("Forbidden value (" + obstacleCellId + ") on element of MapObstacle.obstacleCellId.");
            }

            state = (byte)reader.ReadByte();
            if (state < 0)
            {
                throw new System.Exception("Forbidden value (" + state + ") on element of MapObstacle.state.");
            }

        }


    }
}








