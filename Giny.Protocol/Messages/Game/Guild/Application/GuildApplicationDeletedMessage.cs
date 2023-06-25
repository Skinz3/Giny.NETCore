using System.Collections.Generic;
using Giny.Core.Network.Messages;
using Giny.Protocol.Types;
using Giny.Core.IO.Interfaces;
using Giny.Protocol;
using Giny.Protocol.Enums;

namespace Giny.Protocol.Messages
{ 
    public class GuildApplicationDeletedMessage : NetworkMessage  
    { 
        public  const ushort Id = 2156;
        public override ushort MessageId => Id;

        public bool deleted;

        public GuildApplicationDeletedMessage()
        {
        }
        public GuildApplicationDeletedMessage(bool deleted)
        {
            this.deleted = deleted;
        }
        public override void Serialize(IDataWriter writer)
        {
            writer.WriteBoolean((bool)deleted);
        }
        public override void Deserialize(IDataReader reader)
        {
            deleted = (bool)reader.ReadBoolean();
        }


    }
}








