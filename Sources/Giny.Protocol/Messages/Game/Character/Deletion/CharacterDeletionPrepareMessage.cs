using System.Collections.Generic;
using Giny.Core.Network.Messages;
using Giny.Protocol.Types;
using Giny.Core.IO.Interfaces;
using Giny.Protocol;
using Giny.Protocol.Enums;

namespace Giny.Protocol.Messages
{
    public class CharacterDeletionPrepareMessage : NetworkMessage
    {
        public const ushort Id = 7478;
        public override ushort MessageId => Id;

        public long characterId;
        public string characterName;
        public string secretQuestion;
        public bool needSecretAnswer;

        public CharacterDeletionPrepareMessage()
        {
        }
        public CharacterDeletionPrepareMessage(long characterId, string characterName, string secretQuestion, bool needSecretAnswer)
        {
            this.characterId = characterId;
            this.characterName = characterName;
            this.secretQuestion = secretQuestion;
            this.needSecretAnswer = needSecretAnswer;
        }
        public override void Serialize(IDataWriter writer)
        {
            if (characterId < 0 || characterId > 9007199254740992)
            {
                throw new System.Exception("Forbidden value (" + characterId + ") on element characterId.");
            }

            writer.WriteVarLong((long)characterId);
            writer.WriteUTF((string)characterName);
            writer.WriteUTF((string)secretQuestion);
            writer.WriteBoolean((bool)needSecretAnswer);
        }
        public override void Deserialize(IDataReader reader)
        {
            characterId = (long)reader.ReadVarUhLong();
            if (characterId < 0 || characterId > 9007199254740992)
            {
                throw new System.Exception("Forbidden value (" + characterId + ") on element of CharacterDeletionPrepareMessage.characterId.");
            }

            characterName = (string)reader.ReadUTF();
            secretQuestion = (string)reader.ReadUTF();
            needSecretAnswer = (bool)reader.ReadBoolean();
        }

    }
}


