using System.Collections.Generic;
using Giny.Core.Network.Messages;
using Giny.Protocol.Types;
using Giny.Core.IO.Interfaces;
using Giny.Protocol;
using Giny.Protocol.Enums;

namespace Giny.Protocol.Messages
{
    public class TaxCollectorRemovedMessage : NetworkMessage
    {
        public const ushort Id = 3362;
        public override ushort MessageId => Id;

        public double collectorId;

        public TaxCollectorRemovedMessage()
        {
        }
        public TaxCollectorRemovedMessage(double collectorId)
        {
            this.collectorId = collectorId;
        }
        public override void Serialize(IDataWriter writer)
        {
            if (collectorId < 0 || collectorId > 9007199254740992)
            {
                throw new System.Exception("Forbidden value (" + collectorId + ") on element collectorId.");
            }

            writer.WriteDouble((double)collectorId);
        }
        public override void Deserialize(IDataReader reader)
        {
            collectorId = (double)reader.ReadDouble();
            if (collectorId < 0 || collectorId > 9007199254740992)
            {
                throw new System.Exception("Forbidden value (" + collectorId + ") on element of TaxCollectorRemovedMessage.collectorId.");
            }

        }

    }
}


