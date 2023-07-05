using System.Collections.Generic;
using Giny.Core.Network.Messages;
using Giny.Protocol.Types;
using Giny.Core.IO.Interfaces;
using Giny.Protocol;
using Giny.Protocol.Enums;

namespace Giny.Protocol.Messages
{
    public class CharacterReplayRequestMessage : NetworkMessage
    {
        public const ushort Id = 3908;
        public override ushort MessageId => Id;

        public long characterId;

        public CharacterReplayRequestMessage()
        {
        }
        public CharacterReplayRequestMessage(long characterId)
        {
            this.characterId = characterId;
        }
        public override void Serialize(IDataWriter writer)
        {
            if (characterId < 0 || characterId > 9007199254740992)
            {
                throw new System.Exception("Forbidden value (" + characterId + ") on element characterId.");
            }

            writer.WriteVarLong((long)characterId);
        }
        public override void Deserialize(IDataReader reader)
        {
            characterId = (long)reader.ReadVarUhLong();
            if (characterId < 0 || characterId > 9007199254740992)
            {
                throw new System.Exception("Forbidden value (" + characterId + ") on element of CharacterReplayRequestMessage.characterId.");
            }

        }

    }
}


