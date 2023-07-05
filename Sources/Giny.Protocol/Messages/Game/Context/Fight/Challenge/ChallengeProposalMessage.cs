using System.Collections.Generic;
using Giny.Core.Network.Messages;
using Giny.Protocol.Types;
using Giny.Core.IO.Interfaces;
using Giny.Protocol;
using Giny.Protocol.Enums;

namespace Giny.Protocol.Messages
{
    public class ChallengeProposalMessage : NetworkMessage
    {
        public const ushort Id = 1008;
        public override ushort MessageId => Id;

        public ChallengeInformation[] challengeProposals;
        public double timer;

        public ChallengeProposalMessage()
        {
        }
        public ChallengeProposalMessage(ChallengeInformation[] challengeProposals, double timer)
        {
            this.challengeProposals = challengeProposals;
            this.timer = timer;
        }
        public override void Serialize(IDataWriter writer)
        {
            writer.WriteShort((short)challengeProposals.Length);
            for (uint _i1 = 0; _i1 < challengeProposals.Length; _i1++)
            {
                (challengeProposals[_i1] as ChallengeInformation).Serialize(writer);
            }

            if (timer < 0 || timer > 9007199254740992)
            {
                throw new System.Exception("Forbidden value (" + timer + ") on element timer.");
            }

            writer.WriteDouble((double)timer);
        }
        public override void Deserialize(IDataReader reader)
        {
            ChallengeInformation _item1 = null;
            uint _challengeProposalsLen = (uint)reader.ReadUShort();
            for (uint _i1 = 0; _i1 < _challengeProposalsLen; _i1++)
            {
                _item1 = new ChallengeInformation();
                _item1.Deserialize(reader);
                challengeProposals[_i1] = _item1;
            }

            timer = (double)reader.ReadDouble();
            if (timer < 0 || timer > 9007199254740992)
            {
                throw new System.Exception("Forbidden value (" + timer + ") on element of ChallengeProposalMessage.timer.");
            }

        }

    }
}


