using System.Collections.Generic;
using Giny.Core.Network.Messages;
using Giny.Protocol.Types;
using Giny.Core.IO.Interfaces;
using Giny.Protocol;
using Giny.Protocol.Enums;

namespace Giny.Protocol.Messages
{ 
    public class GameFightPlacementSwapPositionsRequestMessage : GameFightPlacementPositionRequestMessage  
    { 
        public new const ushort Id = 8289;
        public override ushort MessageId => Id;

        public double requestedId;

        public GameFightPlacementSwapPositionsRequestMessage()
        {
        }
        public GameFightPlacementSwapPositionsRequestMessage(double requestedId,short cellId)
        {
            this.requestedId = requestedId;
            this.cellId = cellId;
        }
        public override void Serialize(IDataWriter writer)
        {
            base.Serialize(writer);
            if (requestedId < -9.00719925474099E+15 || requestedId > 9.00719925474099E+15)
            {
                throw new System.Exception("Forbidden value (" + requestedId + ") on element requestedId.");
            }

            writer.WriteDouble((double)requestedId);
        }
        public override void Deserialize(IDataReader reader)
        {
            base.Deserialize(reader);
            requestedId = (double)reader.ReadDouble();
            if (requestedId < -9.00719925474099E+15 || requestedId > 9.00719925474099E+15)
            {
                throw new System.Exception("Forbidden value (" + requestedId + ") on element of GameFightPlacementSwapPositionsRequestMessage.requestedId.");
            }

        }


    }
}








