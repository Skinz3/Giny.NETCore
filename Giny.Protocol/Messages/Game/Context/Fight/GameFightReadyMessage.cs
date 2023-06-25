using System.Collections.Generic;
using Giny.Core.Network.Messages;
using Giny.Protocol.Types;
using Giny.Core.IO.Interfaces;
using Giny.Protocol;
using Giny.Protocol.Enums;

namespace Giny.Protocol.Messages
{ 
    public class GameFightReadyMessage : NetworkMessage  
    { 
        public  const ushort Id = 6127;
        public override ushort MessageId => Id;

        public bool isReady;

        public GameFightReadyMessage()
        {
        }
        public GameFightReadyMessage(bool isReady)
        {
            this.isReady = isReady;
        }
        public override void Serialize(IDataWriter writer)
        {
            writer.WriteBoolean((bool)isReady);
        }
        public override void Deserialize(IDataReader reader)
        {
            isReady = (bool)reader.ReadBoolean();
        }


    }
}








