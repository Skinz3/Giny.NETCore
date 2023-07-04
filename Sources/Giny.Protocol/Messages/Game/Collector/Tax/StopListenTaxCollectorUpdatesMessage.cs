using System.Collections.Generic;
using Giny.Core.Network.Messages;
using Giny.Protocol.Types;
using Giny.Core.IO.Interfaces;
using Giny.Protocol;
using Giny.Protocol.Enums;

namespace Giny.Protocol.Messages
{
    public class StopListenTaxCollectorUpdatesMessage : NetworkMessage
    {
        public const ushort Id = 2841;
        public override ushort MessageId => Id;

        public double taxCollectorId;

        public StopListenTaxCollectorUpdatesMessage()
        {
        }
        public StopListenTaxCollectorUpdatesMessage(double taxCollectorId)
        {
            this.taxCollectorId = taxCollectorId;
        }
        public override void Serialize(IDataWriter writer)
        {
            if (taxCollectorId < 0 || taxCollectorId > 9007199254740992)
            {
                throw new System.Exception("Forbidden value (" + taxCollectorId + ") on element taxCollectorId.");
            }

            writer.WriteDouble((double)taxCollectorId);
        }
        public override void Deserialize(IDataReader reader)
        {
            taxCollectorId = (double)reader.ReadDouble();
            if (taxCollectorId < 0 || taxCollectorId > 9007199254740992)
            {
                throw new System.Exception("Forbidden value (" + taxCollectorId + ") on element of StopListenTaxCollectorUpdatesMessage.taxCollectorId.");
            }

        }

    }
}


