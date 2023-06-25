using System.Collections.Generic;
using Giny.Core.Network.Messages;
using Giny.Protocol.Types;
using Giny.Core.IO.Interfaces;
using Giny.Protocol;
using Giny.Protocol.Enums;

namespace Giny.Protocol.Messages
{ 
    public class GameMapNoMovementMessage : NetworkMessage  
    { 
        public  const ushort Id = 6929;
        public override ushort MessageId => Id;

        public short cellX;
        public short cellY;

        public GameMapNoMovementMessage()
        {
        }
        public GameMapNoMovementMessage(short cellX,short cellY)
        {
            this.cellX = cellX;
            this.cellY = cellY;
        }
        public override void Serialize(IDataWriter writer)
        {
            writer.WriteShort((short)cellX);
            writer.WriteShort((short)cellY);
        }
        public override void Deserialize(IDataReader reader)
        {
            cellX = (short)reader.ReadShort();
            cellY = (short)reader.ReadShort();
        }


    }
}








