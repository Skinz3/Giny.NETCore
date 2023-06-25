using System.Collections.Generic;
using Giny.Core.Network.Messages;
using Giny.Protocol.Types;
using Giny.Core.IO.Interfaces;
using Giny.Protocol;
using Giny.Protocol.Enums;

namespace Giny.Protocol.Messages
{ 
    public class GameCautiousMapMovementRequestMessage : GameMapMovementRequestMessage  
    { 
        public new const ushort Id = 6590;
        public override ushort MessageId => Id;


        public GameCautiousMapMovementRequestMessage()
        {
        }
        public GameCautiousMapMovementRequestMessage(short[] keyMovements,double mapId)
        {
            this.keyMovements = keyMovements;
            this.mapId = mapId;
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








