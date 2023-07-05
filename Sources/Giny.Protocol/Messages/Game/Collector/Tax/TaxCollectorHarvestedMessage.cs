using System.Collections.Generic;
using Giny.Core.Network.Messages;
using Giny.Protocol.Types;
using Giny.Core.IO.Interfaces;
using Giny.Protocol;
using Giny.Protocol.Enums;

namespace Giny.Protocol.Messages
{
    public class TaxCollectorHarvestedMessage : NetworkMessage
    {
        public const ushort Id = 9412;
        public override ushort MessageId => Id;

        public double taxCollectorId;
        public long harvesterId;
        public string harvesterName;

        public TaxCollectorHarvestedMessage()
        {
        }
        public TaxCollectorHarvestedMessage(double taxCollectorId, long harvesterId, string harvesterName)
        {
            this.taxCollectorId = taxCollectorId;
            this.harvesterId = harvesterId;
            this.harvesterName = harvesterName;
        }
        public override void Serialize(IDataWriter writer)
        {
            if (taxCollectorId < 0 || taxCollectorId > 9007199254740992)
            {
                throw new System.Exception("Forbidden value (" + taxCollectorId + ") on element taxCollectorId.");
            }

            writer.WriteDouble((double)taxCollectorId);
            if (harvesterId < 0 || harvesterId > 9007199254740992)
            {
                throw new System.Exception("Forbidden value (" + harvesterId + ") on element harvesterId.");
            }

            writer.WriteVarLong((long)harvesterId);
            writer.WriteUTF((string)harvesterName);
        }
        public override void Deserialize(IDataReader reader)
        {
            taxCollectorId = (double)reader.ReadDouble();
            if (taxCollectorId < 0 || taxCollectorId > 9007199254740992)
            {
                throw new System.Exception("Forbidden value (" + taxCollectorId + ") on element of TaxCollectorHarvestedMessage.taxCollectorId.");
            }

            harvesterId = (long)reader.ReadVarUhLong();
            if (harvesterId < 0 || harvesterId > 9007199254740992)
            {
                throw new System.Exception("Forbidden value (" + harvesterId + ") on element of TaxCollectorHarvestedMessage.harvesterId.");
            }

            harvesterName = (string)reader.ReadUTF();
        }

    }
}


