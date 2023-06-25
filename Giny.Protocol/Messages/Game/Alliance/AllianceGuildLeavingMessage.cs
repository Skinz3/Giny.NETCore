using System.Collections.Generic;
using Giny.Core.Network.Messages;
using Giny.Protocol.Types;
using Giny.Core.IO.Interfaces;
using Giny.Protocol;
using Giny.Protocol.Enums;

namespace Giny.Protocol.Messages
{ 
    public class AllianceGuildLeavingMessage : NetworkMessage  
    { 
        public  const ushort Id = 5586;
        public override ushort MessageId => Id;

        public bool kicked;
        public int guildId;

        public AllianceGuildLeavingMessage()
        {
        }
        public AllianceGuildLeavingMessage(bool kicked,int guildId)
        {
            this.kicked = kicked;
            this.guildId = guildId;
        }
        public override void Serialize(IDataWriter writer)
        {
            writer.WriteBoolean((bool)kicked);
            if (guildId < 0)
            {
                throw new System.Exception("Forbidden value (" + guildId + ") on element guildId.");
            }

            writer.WriteVarInt((int)guildId);
        }
        public override void Deserialize(IDataReader reader)
        {
            kicked = (bool)reader.ReadBoolean();
            guildId = (int)reader.ReadVarUhInt();
            if (guildId < 0)
            {
                throw new System.Exception("Forbidden value (" + guildId + ") on element of AllianceGuildLeavingMessage.guildId.");
            }

        }


    }
}








