using System.Collections.Generic;
using Giny.Core.Network.Messages;
using Giny.Protocol.Types;
using Giny.Core.IO.Interfaces;
using Giny.Protocol;
using Giny.Protocol.Enums;

namespace Giny.Protocol.Messages
{
    public class ChallengeListMessage : NetworkMessage
    {
        public const ushort Id = 6466;
        public override ushort MessageId => Id;

        public ChallengeInformation[] challengesInformation;

        public ChallengeListMessage()
        {
        }
        public ChallengeListMessage(ChallengeInformation[] challengesInformation)
        {
            this.challengesInformation = challengesInformation;
        }
        public override void Serialize(IDataWriter writer)
        {
            writer.WriteShort((short)challengesInformation.Length);
            for (uint _i1 = 0; _i1 < challengesInformation.Length; _i1++)
            {
                (challengesInformation[_i1] as ChallengeInformation).Serialize(writer);
            }

        }
        public override void Deserialize(IDataReader reader)
        {
            ChallengeInformation _item1 = null;
            uint _challengesInformationLen = (uint)reader.ReadUShort();
            for (uint _i1 = 0; _i1 < _challengesInformationLen; _i1++)
            {
                _item1 = new ChallengeInformation();
                _item1.Deserialize(reader);
                challengesInformation[_i1] = _item1;
            }

        }

    }
}


