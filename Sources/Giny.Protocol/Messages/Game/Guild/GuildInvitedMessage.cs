using System.Collections.Generic;
using Giny.Core.Network.Messages;
using Giny.Protocol.Types;
using Giny.Core.IO.Interfaces;
using Giny.Protocol;
using Giny.Protocol.Enums;

namespace Giny.Protocol.Messages
{ 
    public class GuildInvitedMessage : NetworkMessage  
    { 
        public  const ushort Id = 9234;
        public override ushort MessageId => Id;

        public long recruterId;
        public string recruterName;
        public BasicGuildInformations guildInfo;

        public GuildInvitedMessage()
        {
        }
        public GuildInvitedMessage(long recruterId,string recruterName,BasicGuildInformations guildInfo)
        {
            this.recruterId = recruterId;
            this.recruterName = recruterName;
            this.guildInfo = guildInfo;
        }
        public override void Serialize(IDataWriter writer)
        {
            if (recruterId < 0 || recruterId > 9.00719925474099E+15)
            {
                throw new System.Exception("Forbidden value (" + recruterId + ") on element recruterId.");
            }

            writer.WriteVarLong((long)recruterId);
            writer.WriteUTF((string)recruterName);
            guildInfo.Serialize(writer);
        }
        public override void Deserialize(IDataReader reader)
        {
            recruterId = (long)reader.ReadVarUhLong();
            if (recruterId < 0 || recruterId > 9.00719925474099E+15)
            {
                throw new System.Exception("Forbidden value (" + recruterId + ") on element of GuildInvitedMessage.recruterId.");
            }

            recruterName = (string)reader.ReadUTF();
            guildInfo = new BasicGuildInformations();
            guildInfo.Deserialize(reader);
        }


    }
}








