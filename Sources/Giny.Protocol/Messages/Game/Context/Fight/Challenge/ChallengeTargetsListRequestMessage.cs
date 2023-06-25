using System.Collections.Generic;
using Giny.Core.Network.Messages;
using Giny.Protocol.Types;
using Giny.Core.IO.Interfaces;
using Giny.Protocol;
using Giny.Protocol.Enums;

namespace Giny.Protocol.Messages
{ 
    public class ChallengeTargetsListRequestMessage : NetworkMessage  
    { 
        public  const ushort Id = 8620;
        public override ushort MessageId => Id;

        public short challengeId;

        public ChallengeTargetsListRequestMessage()
        {
        }
        public ChallengeTargetsListRequestMessage(short challengeId)
        {
            this.challengeId = challengeId;
        }
        public override void Serialize(IDataWriter writer)
        {
            if (challengeId < 0)
            {
                throw new System.Exception("Forbidden value (" + challengeId + ") on element challengeId.");
            }

            writer.WriteVarShort((short)challengeId);
        }
        public override void Deserialize(IDataReader reader)
        {
            challengeId = (short)reader.ReadVarUhShort();
            if (challengeId < 0)
            {
                throw new System.Exception("Forbidden value (" + challengeId + ") on element of ChallengeTargetsListRequestMessage.challengeId.");
            }

        }


    }
}








