using System.Collections.Generic;
using Giny.Core.Network.Messages;
using Giny.Protocol.Types;
using Giny.Core.IO.Interfaces;
using Giny.Protocol;
using Giny.Protocol.Enums;

namespace Giny.Protocol.Messages
{ 
    public class GameActionFightNoSpellCastMessage : NetworkMessage  
    { 
        public  const ushort Id = 5692;
        public override ushort MessageId => Id;

        public int spellLevelId;

        public GameActionFightNoSpellCastMessage()
        {
        }
        public GameActionFightNoSpellCastMessage(int spellLevelId)
        {
            this.spellLevelId = spellLevelId;
        }
        public override void Serialize(IDataWriter writer)
        {
            if (spellLevelId < 0)
            {
                throw new System.Exception("Forbidden value (" + spellLevelId + ") on element spellLevelId.");
            }

            writer.WriteVarInt((int)spellLevelId);
        }
        public override void Deserialize(IDataReader reader)
        {
            spellLevelId = (int)reader.ReadVarUhInt();
            if (spellLevelId < 0)
            {
                throw new System.Exception("Forbidden value (" + spellLevelId + ") on element of GameActionFightNoSpellCastMessage.spellLevelId.");
            }

        }


    }
}








