using System.Collections.Generic;
using Giny.Core.Network.Messages;
using Giny.Protocol.Types;
using Giny.Core.IO.Interfaces;
using Giny.Protocol;
using Giny.Protocol.Enums;

namespace Giny.Protocol.Messages
{
    public class ConsumeGameActionItemMessage : NetworkMessage
    {
        public const ushort Id = 2860;
        public override ushort MessageId => Id;

        public int actionId;
        public long characterId;

        public ConsumeGameActionItemMessage()
        {
        }
        public ConsumeGameActionItemMessage(int actionId, long characterId)
        {
            this.actionId = actionId;
            this.characterId = characterId;
        }
        public override void Serialize(IDataWriter writer)
        {
            if (actionId < 0)
            {
                throw new System.Exception("Forbidden value (" + actionId + ") on element actionId.");
            }

            writer.WriteInt((int)actionId);
            if (characterId < 0 || characterId > 9007199254740992)
            {
                throw new System.Exception("Forbidden value (" + characterId + ") on element characterId.");
            }

            writer.WriteVarLong((long)characterId);
        }
        public override void Deserialize(IDataReader reader)
        {
            actionId = (int)reader.ReadInt();
            if (actionId < 0)
            {
                throw new System.Exception("Forbidden value (" + actionId + ") on element of ConsumeGameActionItemMessage.actionId.");
            }

            characterId = (long)reader.ReadVarUhLong();
            if (characterId < 0 || characterId > 9007199254740992)
            {
                throw new System.Exception("Forbidden value (" + characterId + ") on element of ConsumeGameActionItemMessage.characterId.");
            }

        }

    }
}


