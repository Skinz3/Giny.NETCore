using System.Collections.Generic;
using Giny.Core.Network.Messages;
using Giny.Protocol.Types;
using Giny.Core.IO.Interfaces;
using Giny.Protocol;
using Giny.Protocol.Enums;

namespace Giny.Protocol.Messages
{ 
    public class AllianceKickRequestMessage : NetworkMessage  
    { 
        public  const ushort Id = 2717;
        public override ushort MessageId => Id;

        public int kickedId;

        public AllianceKickRequestMessage()
        {
        }
        public AllianceKickRequestMessage(int kickedId)
        {
            this.kickedId = kickedId;
        }
        public override void Serialize(IDataWriter writer)
        {
            if (kickedId < 0)
            {
                throw new System.Exception("Forbidden value (" + kickedId + ") on element kickedId.");
            }

            writer.WriteVarInt((int)kickedId);
        }
        public override void Deserialize(IDataReader reader)
        {
            kickedId = (int)reader.ReadVarUhInt();
            if (kickedId < 0)
            {
                throw new System.Exception("Forbidden value (" + kickedId + ") on element of AllianceKickRequestMessage.kickedId.");
            }

        }


    }
}








