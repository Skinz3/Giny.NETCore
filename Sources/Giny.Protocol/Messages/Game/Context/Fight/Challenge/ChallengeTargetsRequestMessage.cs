using System.Collections.Generic;
using Giny.Core.Network.Messages;
using Giny.Protocol.Types;
using Giny.Core.IO.Interfaces;
using Giny.Protocol;
using Giny.Protocol.Enums;

namespace Giny.Protocol.Messages
{
    public class ChallengeTargetsRequestMessage : NetworkMessage
    {
        public const ushort Id = 4777;
        public override ushort MessageId => Id;

        public int challengeId;

        public ChallengeTargetsRequestMessage()
        {
        }
        public ChallengeTargetsRequestMessage(int challengeId)
        {
            this.challengeId = challengeId;
        }
        public override void Serialize(IDataWriter writer)
        {
            if (challengeId < 0)
            {
                throw new System.Exception("Forbidden value (" + challengeId + ") on element challengeId.");
            }

            writer.WriteVarInt((int)challengeId);
        }
        public override void Deserialize(IDataReader reader)
        {
            challengeId = (int)reader.ReadVarUhInt();
            if (challengeId < 0)
            {
                throw new System.Exception("Forbidden value (" + challengeId + ") on element of ChallengeTargetsRequestMessage.challengeId.");
            }

        }

    }
}


