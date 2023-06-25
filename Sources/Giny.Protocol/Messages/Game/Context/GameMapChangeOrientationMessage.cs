using System.Collections.Generic;
using Giny.Core.Network.Messages;
using Giny.Protocol.Types;
using Giny.Core.IO.Interfaces;
using Giny.Protocol;
using Giny.Protocol.Enums;

namespace Giny.Protocol.Messages
{ 
    public class GameMapChangeOrientationMessage : NetworkMessage  
    { 
        public  const ushort Id = 1116;
        public override ushort MessageId => Id;

        public ActorOrientation orientation;

        public GameMapChangeOrientationMessage()
        {
        }
        public GameMapChangeOrientationMessage(ActorOrientation orientation)
        {
            this.orientation = orientation;
        }
        public override void Serialize(IDataWriter writer)
        {
            orientation.Serialize(writer);
        }
        public override void Deserialize(IDataReader reader)
        {
            orientation = new ActorOrientation();
            orientation.Deserialize(reader);
        }


    }
}








