using System.Collections.Generic;
using Giny.Core.Network.Messages;
using Giny.Protocol.Types;
using Giny.Core.IO.Interfaces;
using Giny.Protocol;
using Giny.Protocol.Enums;

namespace Giny.Protocol.Messages
{
    public class ChallengeNumberMessage : NetworkMessage
    {
        public const ushort Id = 2139;
        public override ushort MessageId => Id;

        public int challengeNumber;

        public ChallengeNumberMessage()
        {
        }
        public ChallengeNumberMessage(int challengeNumber)
        {
            this.challengeNumber = challengeNumber;
        }
        public override void Serialize(IDataWriter writer)
        {
            if (challengeNumber < 0)
            {
                throw new System.Exception("Forbidden value (" + challengeNumber + ") on element challengeNumber.");
            }

            writer.WriteVarInt((int)challengeNumber);
        }
        public override void Deserialize(IDataReader reader)
        {
            challengeNumber = (int)reader.ReadVarUhInt();
            if (challengeNumber < 0)
            {
                throw new System.Exception("Forbidden value (" + challengeNumber + ") on element of ChallengeNumberMessage.challengeNumber.");
            }

        }

    }
}


