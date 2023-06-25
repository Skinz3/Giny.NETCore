using System.Collections.Generic;
using Giny.Core.Network.Messages;
using Giny.Protocol.Types;
using Giny.Core.IO.Interfaces;
using Giny.Protocol;
using Giny.Protocol.Enums;

namespace Giny.Protocol.Messages
{ 
    public class TaxCollectorMovementRemoveMessage : NetworkMessage  
    { 
        public  const ushort Id = 4853;
        public override ushort MessageId => Id;

        public double collectorId;

        public TaxCollectorMovementRemoveMessage()
        {
        }
        public TaxCollectorMovementRemoveMessage(double collectorId)
        {
            this.collectorId = collectorId;
        }
        public override void Serialize(IDataWriter writer)
        {
            if (collectorId < 0 || collectorId > 9.00719925474099E+15)
            {
                throw new System.Exception("Forbidden value (" + collectorId + ") on element collectorId.");
            }

            writer.WriteDouble((double)collectorId);
        }
        public override void Deserialize(IDataReader reader)
        {
            collectorId = (double)reader.ReadDouble();
            if (collectorId < 0 || collectorId > 9.00719925474099E+15)
            {
                throw new System.Exception("Forbidden value (" + collectorId + ") on element of TaxCollectorMovementRemoveMessage.collectorId.");
            }

        }


    }
}








