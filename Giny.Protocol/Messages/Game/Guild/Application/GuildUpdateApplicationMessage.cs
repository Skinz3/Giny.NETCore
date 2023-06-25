using System.Collections.Generic;
using Giny.Core.Network.Messages;
using Giny.Protocol.Types;
using Giny.Core.IO.Interfaces;
using Giny.Protocol;
using Giny.Protocol.Enums;

namespace Giny.Protocol.Messages
{ 
    public class GuildUpdateApplicationMessage : NetworkMessage  
    { 
        public  const ushort Id = 2139;
        public override ushort MessageId => Id;

        public string applyText;
        public int guildId;

        public GuildUpdateApplicationMessage()
        {
        }
        public GuildUpdateApplicationMessage(string applyText,int guildId)
        {
            this.applyText = applyText;
            this.guildId = guildId;
        }
        public override void Serialize(IDataWriter writer)
        {
            writer.WriteUTF((string)applyText);
            if (guildId < 0)
            {
                throw new System.Exception("Forbidden value (" + guildId + ") on element guildId.");
            }

            writer.WriteVarInt((int)guildId);
        }
        public override void Deserialize(IDataReader reader)
        {
            applyText = (string)reader.ReadUTF();
            guildId = (int)reader.ReadVarUhInt();
            if (guildId < 0)
            {
                throw new System.Exception("Forbidden value (" + guildId + ") on element of GuildUpdateApplicationMessage.guildId.");
            }

        }


    }
}








