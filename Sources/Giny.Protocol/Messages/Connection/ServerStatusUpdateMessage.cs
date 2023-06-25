using System.Collections.Generic;
using Giny.Core.Network.Messages;
using Giny.Protocol.Types;
using Giny.Core.IO.Interfaces;
using Giny.Protocol;
using Giny.Protocol.Enums;

namespace Giny.Protocol.Messages
{ 
    public class ServerStatusUpdateMessage : NetworkMessage  
    { 
        public  const ushort Id = 7668;
        public override ushort MessageId => Id;

        public GameServerInformations server;

        public ServerStatusUpdateMessage()
        {
        }
        public ServerStatusUpdateMessage(GameServerInformations server)
        {
            this.server = server;
        }
        public override void Serialize(IDataWriter writer)
        {
            server.Serialize(writer);
        }
        public override void Deserialize(IDataReader reader)
        {
            server = new GameServerInformations();
            server.Deserialize(reader);
        }


    }
}








