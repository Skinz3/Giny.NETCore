using System.Collections.Generic;
using Giny.Core.Network.Messages;
using Giny.Protocol.Types;
using Giny.Core.IO.Interfaces;
using Giny.Protocol;
using Giny.Protocol.Enums;

namespace Giny.Protocol.Messages
{ 
    public class ChallengeInfoMessage : NetworkMessage  
    { 
        public  const ushort Id = 5420;
        public override ushort MessageId => Id;

        public short challengeId;
        public double targetId;
        public int xpBonus;
        public int dropBonus;

        public ChallengeInfoMessage()
        {
        }
        public ChallengeInfoMessage(short challengeId,double targetId,int xpBonus,int dropBonus)
        {
            this.challengeId = challengeId;
            this.targetId = targetId;
            this.xpBonus = xpBonus;
            this.dropBonus = dropBonus;
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
            if (xpBonus < 0)
            {
                throw new System.Exception("Forbidden value (" + xpBonus + ") on element xpBonus.");
            }

            writer.WriteVarInt((int)xpBonus);
            if (dropBonus < 0)
            {
                throw new System.Exception("Forbidden value (" + dropBonus + ") on element dropBonus.");
            }

            writer.WriteVarInt((int)dropBonus);
        }
        public override void Deserialize(IDataReader reader)
        {
            challengeId = (short)reader.ReadVarUhShort();
            if (challengeId < 0)
            {
                throw new System.Exception("Forbidden value (" + challengeId + ") on element of ChallengeInfoMessage.challengeId.");
            }

            targetId = (double)reader.ReadDouble();
            if (targetId < -9.00719925474099E+15 || targetId > 9.00719925474099E+15)
            {
                throw new System.Exception("Forbidden value (" + targetId + ") on element of ChallengeInfoMessage.targetId.");
            }

            xpBonus = (int)reader.ReadVarUhInt();
            if (xpBonus < 0)
            {
                throw new System.Exception("Forbidden value (" + xpBonus + ") on element of ChallengeInfoMessage.xpBonus.");
            }

            dropBonus = (int)reader.ReadVarUhInt();
            if (dropBonus < 0)
            {
                throw new System.Exception("Forbidden value (" + dropBonus + ") on element of ChallengeInfoMessage.dropBonus.");
            }

        }


    }
}








