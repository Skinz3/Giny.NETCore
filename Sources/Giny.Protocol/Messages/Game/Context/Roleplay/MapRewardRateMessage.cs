using System.Collections.Generic;
using Giny.Core.Network.Messages;
using Giny.Protocol.Types;
using Giny.Core.IO.Interfaces;
using Giny.Protocol;
using Giny.Protocol.Enums;

namespace Giny.Protocol.Messages
{ 
    public class MapRewardRateMessage : NetworkMessage  
    { 
        public  const ushort Id = 3202;
        public override ushort MessageId => Id;

        public short mapRate;
        public short subAreaRate;
        public short totalRate;

        public MapRewardRateMessage()
        {
        }
        public MapRewardRateMessage(short mapRate,short subAreaRate,short totalRate)
        {
            this.mapRate = mapRate;
            this.subAreaRate = subAreaRate;
            this.totalRate = totalRate;
        }
        public override void Serialize(IDataWriter writer)
        {
            writer.WriteVarShort((short)mapRate);
            writer.WriteVarShort((short)subAreaRate);
            writer.WriteVarShort((short)totalRate);
        }
        public override void Deserialize(IDataReader reader)
        {
            mapRate = (short)reader.ReadVarShort();
            subAreaRate = (short)reader.ReadVarShort();
            totalRate = (short)reader.ReadVarShort();
        }


    }
}








