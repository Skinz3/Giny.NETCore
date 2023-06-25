using System.Collections.Generic;
using Giny.Core.Network.Messages;
using Giny.Protocol.Types;
using Giny.Core.IO.Interfaces;
using Giny.Protocol;
using Giny.Protocol.Enums;

namespace Giny.Protocol.Messages
{ 
    public class AllianceFactsRequestMessage : NetworkMessage  
    { 
        public  const ushort Id = 1439;
        public override ushort MessageId => Id;

        public int allianceId;

        public AllianceFactsRequestMessage()
        {
        }
        public AllianceFactsRequestMessage(int allianceId)
        {
            this.allianceId = allianceId;
        }
        public override void Serialize(IDataWriter writer)
        {
            if (allianceId < 0)
            {
                throw new System.Exception("Forbidden value (" + allianceId + ") on element allianceId.");
            }

            writer.WriteVarInt((int)allianceId);
        }
        public override void Deserialize(IDataReader reader)
        {
            allianceId = (int)reader.ReadVarUhInt();
            if (allianceId < 0)
            {
                throw new System.Exception("Forbidden value (" + allianceId + ") on element of AllianceFactsRequestMessage.allianceId.");
            }

        }


    }
}








