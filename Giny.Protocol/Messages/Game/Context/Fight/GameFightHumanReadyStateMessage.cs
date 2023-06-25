using System.Collections.Generic;
using Giny.Core.Network.Messages;
using Giny.Protocol.Types;
using Giny.Core.IO.Interfaces;
using Giny.Protocol;
using Giny.Protocol.Enums;

namespace Giny.Protocol.Messages
{ 
    public class GameFightHumanReadyStateMessage : NetworkMessage  
    { 
        public  const ushort Id = 2605;
        public override ushort MessageId => Id;

        public long characterId;
        public bool isReady;

        public GameFightHumanReadyStateMessage()
        {
        }
        public GameFightHumanReadyStateMessage(long characterId,bool isReady)
        {
            this.characterId = characterId;
            this.isReady = isReady;
        }
        public override void Serialize(IDataWriter writer)
        {
            if (characterId < 0 || characterId > 9.00719925474099E+15)
            {
                throw new System.Exception("Forbidden value (" + characterId + ") on element characterId.");
            }

            writer.WriteVarLong((long)characterId);
            writer.WriteBoolean((bool)isReady);
        }
        public override void Deserialize(IDataReader reader)
        {
            characterId = (long)reader.ReadVarUhLong();
            if (characterId < 0 || characterId > 9.00719925474099E+15)
            {
                throw new System.Exception("Forbidden value (" + characterId + ") on element of GameFightHumanReadyStateMessage.characterId.");
            }

            isReady = (bool)reader.ReadBoolean();
        }


    }
}








