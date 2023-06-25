using System.Collections.Generic;
using Giny.Core.Network.Messages;
using Giny.Protocol.Types;
using Giny.Core.IO.Interfaces;
using Giny.Protocol;
using Giny.Protocol.Enums;

namespace Giny.Protocol.Messages
{ 
    public class ChallengeTargetUpdateMessage : NetworkMessage  
    { 
        public  const ushort Id = 748;
        public override ushort MessageId => Id;

        public short challengeId;
        public double targetId;

        public ChallengeTargetUpdateMessage()
        {
        }
        public ChallengeTargetUpdateMessage(short challengeId,double targetId)
        {
            this.challengeId = challengeId;
            this.targetId = targetId;
        }
        public override void Serialize(IDataWriter writer)
        {
            if (challengeId < 0)
            {
                throw new System.Exception("Forbidden value (" + challengeId + ") on element challengeId.");
            }

            writer.WriteVarShort((short)challengeId);
            if (targetId < -9.00719925474099E+15 || targetId > 9.00719925474099E+15)
            {
                throw new System.Exception("Forbidden value (" + targetId + ") on element targetId.");
            }

            writer.WriteDouble((double)targetId);
        }
        public override void Deserialize(IDataReader reader)
        {
            challengeId = (short)reader.ReadVarUhShort();
            if (challengeId < 0)
            {
                throw new System.Exception("Forbidden value (" + challengeId + ") on element of ChallengeTargetUpdateMessage.challengeId.");
            }

            targetId = (double)reader.ReadDouble();
            if (targetId < -9.00719925474099E+15 || targetId > 9.00719925474099E+15)
            {
                throw new System.Exception("Forbidden value (" + targetId + ") on element of ChallengeTargetUpdateMessage.targetId.");
            }

        }


    }
}








