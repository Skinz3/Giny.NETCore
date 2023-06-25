using System.Collections.Generic;
using Giny.Core.Network.Messages;
using Giny.Protocol.Types;
using Giny.Core.IO.Interfaces;
using Giny.Protocol;
using Giny.Protocol.Enums;

namespace Giny.Protocol.Messages
{ 
    public class ServerSelectionMessage : NetworkMessage  
    { 
        public  const ushort Id = 8531;
        public override ushort MessageId => Id;

        public short serverId;

        public ServerSelectionMessage()
        {
        }
        public ServerSelectionMessage(short serverId)
        {
            this.serverId = serverId;
        }
        public override void Serialize(IDataWriter writer)
        {
            if (serverId < 0)
            {
                throw new System.Exception("Forbidden value (" + serverId + ") on element serverId.");
            }

            writer.WriteVarShort((short)serverId);
        }
        public override void Deserialize(IDataReader reader)
        {
            serverId = (short)reader.ReadVarUhShort();
            if (serverId < 0)
            {
                throw new System.Exception("Forbidden value (" + serverId + ") on element of ServerSelectionMessage.serverId.");
            }

        }


    }
}








