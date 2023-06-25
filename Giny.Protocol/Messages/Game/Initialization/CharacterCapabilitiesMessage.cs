using System.Collections.Generic;
using Giny.Core.Network.Messages;
using Giny.Protocol.Types;
using Giny.Core.IO.Interfaces;
using Giny.Protocol;
using Giny.Protocol.Enums;

namespace Giny.Protocol.Messages
{ 
    public class CharacterCapabilitiesMessage : NetworkMessage  
    { 
        public  const ushort Id = 1887;
        public override ushort MessageId => Id;

        public int guildEmblemSymbolCategories;

        public CharacterCapabilitiesMessage()
        {
        }
        public CharacterCapabilitiesMessage(int guildEmblemSymbolCategories)
        {
            this.guildEmblemSymbolCategories = guildEmblemSymbolCategories;
        }
        public override void Serialize(IDataWriter writer)
        {
            if (guildEmblemSymbolCategories < 0)
            {
                throw new System.Exception("Forbidden value (" + guildEmblemSymbolCategories + ") on element guildEmblemSymbolCategories.");
            }

            writer.WriteVarInt((int)guildEmblemSymbolCategories);
        }
        public override void Deserialize(IDataReader reader)
        {
            guildEmblemSymbolCategories = (int)reader.ReadVarUhInt();
            if (guildEmblemSymbolCategories < 0)
            {
                throw new System.Exception("Forbidden value (" + guildEmblemSymbolCategories + ") on element of CharacterCapabilitiesMessage.guildEmblemSymbolCategories.");
            }

        }


    }
}








