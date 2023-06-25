using System.Collections.Generic;
using Giny.Core.Network.Messages;
using Giny.Protocol.Types;
using Giny.Core.IO.Interfaces;
using Giny.Protocol;
using Giny.Protocol.Enums;

namespace Giny.Protocol.Messages
{ 
    public class PortalUseRequestMessage : NetworkMessage  
    { 
        public  const ushort Id = 7046;
        public override ushort MessageId => Id;

        public int portalId;

        public PortalUseRequestMessage()
        {
        }
        public PortalUseRequestMessage(int portalId)
        {
            this.portalId = portalId;
        }
        public override void Serialize(IDataWriter writer)
        {
            if (portalId < 0)
            {
                throw new System.Exception("Forbidden value (" + portalId + ") on element portalId.");
            }

            writer.WriteVarInt((int)portalId);
        }
        public override void Deserialize(IDataReader reader)
        {
            portalId = (int)reader.ReadVarUhInt();
            if (portalId < 0)
            {
                throw new System.Exception("Forbidden value (" + portalId + ") on element of PortalUseRequestMessage.portalId.");
            }

        }


    }
}








