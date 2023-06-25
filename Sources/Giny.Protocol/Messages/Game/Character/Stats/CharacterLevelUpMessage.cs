using System.Collections.Generic;
using Giny.Core.Network.Messages;
using Giny.Protocol.Types;
using Giny.Core.IO.Interfaces;
using Giny.Protocol;
using Giny.Protocol.Enums;

namespace Giny.Protocol.Messages
{ 
    public class CharacterLevelUpMessage : NetworkMessage  
    { 
        public  const ushort Id = 2813;
        public override ushort MessageId => Id;

        public short newLevel;

        public CharacterLevelUpMessage()
        {
        }
        public CharacterLevelUpMessage(short newLevel)
        {
            this.newLevel = newLevel;
        }
        public override void Serialize(IDataWriter writer)
        {
            if (newLevel < 0)
            {
                throw new System.Exception("Forbidden value (" + newLevel + ") on element newLevel.");
            }

            writer.WriteVarShort((short)newLevel);
        }
        public override void Deserialize(IDataReader reader)
        {
            newLevel = (short)reader.ReadVarUhShort();
            if (newLevel < 0)
            {
                throw new System.Exception("Forbidden value (" + newLevel + ") on element of CharacterLevelUpMessage.newLevel.");
            }

        }


    }
}








