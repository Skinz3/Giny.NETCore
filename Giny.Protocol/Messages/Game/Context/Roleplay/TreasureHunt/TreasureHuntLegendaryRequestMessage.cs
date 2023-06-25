using System.Collections.Generic;
using Giny.Core.Network.Messages;
using Giny.Protocol.Types;
using Giny.Core.IO.Interfaces;
using Giny.Protocol;
using Giny.Protocol.Enums;

namespace Giny.Protocol.Messages
{ 
    public class TreasureHuntLegendaryRequestMessage : NetworkMessage  
    { 
        public  const ushort Id = 1909;
        public override ushort MessageId => Id;

        public short legendaryId;

        public TreasureHuntLegendaryRequestMessage()
        {
        }
        public TreasureHuntLegendaryRequestMessage(short legendaryId)
        {
            this.legendaryId = legendaryId;
        }
        public override void Serialize(IDataWriter writer)
        {
            if (legendaryId < 0)
            {
                throw new System.Exception("Forbidden value (" + legendaryId + ") on element legendaryId.");
            }

            writer.WriteVarShort((short)legendaryId);
        }
        public override void Deserialize(IDataReader reader)
        {
            legendaryId = (short)reader.ReadVarUhShort();
            if (legendaryId < 0)
            {
                throw new System.Exception("Forbidden value (" + legendaryId + ") on element of TreasureHuntLegendaryRequestMessage.legendaryId.");
            }

        }


    }
}








