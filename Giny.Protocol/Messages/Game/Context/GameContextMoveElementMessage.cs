using System.Collections.Generic;
using Giny.Core.Network.Messages;
using Giny.Protocol.Types;
using Giny.Core.IO.Interfaces;
using Giny.Protocol;
using Giny.Protocol.Enums;

namespace Giny.Protocol.Messages
{ 
    public class GameContextMoveElementMessage : NetworkMessage  
    { 
        public  const ushort Id = 1110;
        public override ushort MessageId => Id;

        public EntityMovementInformations movement;

        public GameContextMoveElementMessage()
        {
        }
        public GameContextMoveElementMessage(EntityMovementInformations movement)
        {
            this.movement = movement;
        }
        public override void Serialize(IDataWriter writer)
        {
            movement.Serialize(writer);
        }
        public override void Deserialize(IDataReader reader)
        {
            movement = new EntityMovementInformations();
            movement.Deserialize(reader);
        }


    }
}








