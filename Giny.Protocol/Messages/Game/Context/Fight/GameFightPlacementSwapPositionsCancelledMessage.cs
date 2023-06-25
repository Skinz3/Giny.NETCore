using System.Collections.Generic;
using Giny.Core.Network.Messages;
using Giny.Protocol.Types;
using Giny.Core.IO.Interfaces;
using Giny.Protocol;
using Giny.Protocol.Enums;

namespace Giny.Protocol.Messages
{ 
    public class GameFightPlacementSwapPositionsCancelledMessage : NetworkMessage  
    { 
        public  const ushort Id = 5655;
        public override ushort MessageId => Id;

        public int requestId;
        public double cancellerId;

        public GameFightPlacementSwapPositionsCancelledMessage()
        {
        }
        public GameFightPlacementSwapPositionsCancelledMessage(int requestId,double cancellerId)
        {
            this.requestId = requestId;
            this.cancellerId = cancellerId;
        }
        public override void Serialize(IDataWriter writer)
        {
            if (requestId < 0)
            {
                throw new System.Exception("Forbidden value (" + requestId + ") on element requestId.");
            }

            writer.WriteInt((int)requestId);
            if (cancellerId < -9.00719925474099E+15 || cancellerId > 9.00719925474099E+15)
            {
                throw new System.Exception("Forbidden value (" + cancellerId + ") on element cancellerId.");
            }

            writer.WriteDouble((double)cancellerId);
        }
        public override void Deserialize(IDataReader reader)
        {
            requestId = (int)reader.ReadInt();
            if (requestId < 0)
            {
                throw new System.Exception("Forbidden value (" + requestId + ") on element of GameFightPlacementSwapPositionsCancelledMessage.requestId.");
            }

            cancellerId = (double)reader.ReadDouble();
            if (cancellerId < -9.00719925474099E+15 || cancellerId > 9.00719925474099E+15)
            {
                throw new System.Exception("Forbidden value (" + cancellerId + ") on element of GameFightPlacementSwapPositionsCancelledMessage.cancellerId.");
            }

        }


    }
}








