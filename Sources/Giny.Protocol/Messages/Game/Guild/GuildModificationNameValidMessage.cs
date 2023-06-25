using System.Collections.Generic;
using Giny.Core.Network.Messages;
using Giny.Protocol.Types;
using Giny.Core.IO.Interfaces;
using Giny.Protocol;
using Giny.Protocol.Enums;

namespace Giny.Protocol.Messages
{ 
    public class GuildModificationNameValidMessage : NetworkMessage  
    { 
        public  const ushort Id = 866;
        public override ushort MessageId => Id;

        public string guildName;

        public GuildModificationNameValidMessage()
        {
        }
        public GuildModificationNameValidMessage(string guildName)
        {
            this.guildName = guildName;
        }
        public override void Serialize(IDataWriter writer)
        {
            writer.WriteUTF((string)guildName);
        }
        public override void Deserialize(IDataReader reader)
        {
            guildName = (string)reader.ReadUTF();
        }


    }
}








