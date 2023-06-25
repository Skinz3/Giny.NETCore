using System.Collections.Generic;
using Giny.Core.Network.Messages;
using Giny.Protocol.Types;
using Giny.Core.IO.Interfaces;
using Giny.Protocol;
using Giny.Protocol.Enums;

namespace Giny.Protocol.Messages
{ 
    public class GameFightPlacementSwapPositionsAcceptMessage : NetworkMessage  
    { 
        public  const ushort Id = 1149;
        public override ushort MessageId => Id;

        public int requestId;

        public GameFightPlacementSwapPositionsAcceptMessage()
        {
        }
        public GameFightPlacementSwapPositionsAcceptMessage(int requestId)
        {
            this.requestId = requestId;
        }
        public override void Serialize(IDataWriter writer)
        {
            if (requestId < 0)
            {
                throw new System.Exception("Forbidden value (" + requestId + ") on element requestId.");
            }

            writer.WriteInt((int)requestId);
        }
        public override void Deserialize(IDataReader reader)
        {
            requestId = (int)reader.ReadInt();
            if (requestId < 0)
            {
                throw new System.Exception("Forbidden value (" + requestId + ") on element of GameFightPlacementSwapPositionsAcceptMessage.requestId.");
            }

        }


    }
}








