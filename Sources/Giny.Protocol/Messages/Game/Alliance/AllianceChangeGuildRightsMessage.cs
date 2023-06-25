using System.Collections.Generic;
using Giny.Core.Network.Messages;
using Giny.Protocol.Types;
using Giny.Core.IO.Interfaces;
using Giny.Protocol;
using Giny.Protocol.Enums;

namespace Giny.Protocol.Messages
{ 
    public class AllianceChangeGuildRightsMessage : NetworkMessage  
    { 
        public  const ushort Id = 6091;
        public override ushort MessageId => Id;

        public int guildId;
        public byte rights;

        public AllianceChangeGuildRightsMessage()
        {
        }
        public AllianceChangeGuildRightsMessage(int guildId,byte rights)
        {
            this.guildId = guildId;
            this.rights = rights;
        }
        public override void Serialize(IDataWriter writer)
        {
            if (guildId < 0)
            {
                throw new System.Exception("Forbidden value (" + guildId + ") on element guildId.");
            }

            writer.WriteVarInt((int)guildId);
            if (rights < 0)
            {
                throw new System.Exception("Forbidden value (" + rights + ") on element rights.");
            }

            writer.WriteByte((byte)rights);
        }
        public override void Deserialize(IDataReader reader)
        {
            guildId = (int)reader.ReadVarUhInt();
            if (guildId < 0)
            {
                throw new System.Exception("Forbidden value (" + guildId + ") on element of AllianceChangeGuildRightsMessage.guildId.");
            }

            rights = (byte)reader.ReadByte();
            if (rights < 0)
            {
                throw new System.Exception("Forbidden value (" + rights + ") on element of AllianceChangeGuildRightsMessage.rights.");
            }

        }


    }
}








