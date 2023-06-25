using System.Collections.Generic;
using Giny.Core.Network.Messages;
using Giny.Protocol.Types;
using Giny.Core.IO.Interfaces;
using Giny.Protocol;
using Giny.Protocol.Enums;

namespace Giny.Protocol.Messages
{ 
    public class GameCautiousMapMovementMessage : GameMapMovementMessage  
    { 
        public new const ushort Id = 9601;
        public override ushort MessageId => Id;


        public GameCautiousMapMovementMessage()
        {
        }
        public GameCautiousMapMovementMessage(short[] keyMovements,short forcedDirection,double actorId)
        {
            this.keyMovements = keyMovements;
            this.forcedDirection = forcedDirection;
            this.actorId = actorId;
        }
        public override void Serialize(IDataWriter writer)
        {
            base.Serialize(writer);
        }
        public override void Deserialize(IDataReader reader)
        {
            base.Deserialize(reader);
        }


    }
}








