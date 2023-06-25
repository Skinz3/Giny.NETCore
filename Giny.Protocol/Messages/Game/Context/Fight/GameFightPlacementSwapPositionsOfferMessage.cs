using System.Collections.Generic;
using Giny.Core.Network.Messages;
using Giny.Protocol.Types;
using Giny.Core.IO.Interfaces;
using Giny.Protocol;
using Giny.Protocol.Enums;

namespace Giny.Protocol.Messages
{ 
    public class GameFightPlacementSwapPositionsOfferMessage : NetworkMessage  
    { 
        public  const ushort Id = 8968;
        public override ushort MessageId => Id;

        public int requestId;
        public double requesterId;
        public short requesterCellId;
        public double requestedId;
        public short requestedCellId;

        public GameFightPlacementSwapPositionsOfferMessage()
        {
        }
        public GameFightPlacementSwapPositionsOfferMessage(int requestId,double requesterId,short requesterCellId,double requestedId,short requestedCellId)
        {
            this.requestId = requestId;
            this.requesterId = requesterId;
            this.requesterCellId = requesterCellId;
            this.requestedId = requestedId;
            this.requestedCellId = requestedCellId;
        }
        public override void Serialize(IDataWriter writer)
        {
            if (requestId < 0)
            {
                throw new System.Exception("Forbidden value (" + requestId + ") on element requestId.");
            }

            writer.WriteInt((int)requestId);
            if (requesterId < -9.00719925474099E+15 || requesterId > 9.00719925474099E+15)
            {
                throw new System.Exception("Forbidden value (" + requesterId + ") on element requesterId.");
            }

            writer.WriteDouble((double)requesterId);
            if (requesterCellId < 0 || requesterCellId > 559)
            {
                throw new System.Exception("Forbidden value (" + requesterCellId + ") on element requesterCellId.");
            }

            writer.WriteVarShort((short)requesterCellId);
            if (requestedId < -9.00719925474099E+15 || requestedId > 9.00719925474099E+15)
            {
                throw new System.Exception("Forbidden value (" + requestedId + ") on element requestedId.");
            }

            writer.WriteDouble((double)requestedId);
            if (requestedCellId < 0 || requestedCellId > 559)
            {
                throw new System.Exception("Forbidden value (" + requestedCellId + ") on element requestedCellId.");
            }

            writer.WriteVarShort((short)requestedCellId);
        }
        public override void Deserialize(IDataReader reader)
        {
            requestId = (int)reader.ReadInt();
            if (requestId < 0)
            {
                throw new System.Exception("Forbidden value (" + requestId + ") on element of GameFightPlacementSwapPositionsOfferMessage.requestId.");
            }

            requesterId = (double)reader.ReadDouble();
            if (requesterId < -9.00719925474099E+15 || requesterId > 9.00719925474099E+15)
            {
                throw new System.Exception("Forbidden value (" + requesterId + ") on element of GameFightPlacementSwapPositionsOfferMessage.requesterId.");
            }

            requesterCellId = (short)reader.ReadVarUhShort();
            if (requesterCellId < 0 || requesterCellId > 559)
            {
                throw new System.Exception("Forbidden value (" + requesterCellId + ") on element of GameFightPlacementSwapPositionsOfferMessage.requesterCellId.");
            }

            requestedId = (double)reader.ReadDouble();
            if (requestedId < -9.00719925474099E+15 || requestedId > 9.00719925474099E+15)
            {
                throw new System.Exception("Forbidden value (" + requestedId + ") on element of GameFightPlacementSwapPositionsOfferMessage.requestedId.");
            }

            requestedCellId = (short)reader.ReadVarUhShort();
            if (requestedCellId < 0 || requestedCellId > 559)
            {
                throw new System.Exception("Forbidden value (" + requestedCellId + ") on element of GameFightPlacementSwapPositionsOfferMessage.requestedCellId.");
            }

        }


    }
}








