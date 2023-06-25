using System.Collections.Generic;
using Giny.Core.Network.Messages;
using Giny.Protocol.Types;
using Giny.Core.IO.Interfaces;
using Giny.Protocol;
using Giny.Protocol.Enums;

namespace Giny.Protocol.Messages
{ 
    public class SymbioticObjectAssociatedMessage : NetworkMessage  
    { 
        public  const ushort Id = 6447;
        public override ushort MessageId => Id;

        public int hostUID;

        public SymbioticObjectAssociatedMessage()
        {
        }
        public SymbioticObjectAssociatedMessage(int hostUID)
        {
            this.hostUID = hostUID;
        }
        public override void Serialize(IDataWriter writer)
        {
            if (hostUID < 0)
            {
                throw new System.Exception("Forbidden value (" + hostUID + ") on element hostUID.");
            }

            writer.WriteVarInt((int)hostUID);
        }
        public override void Deserialize(IDataReader reader)
        {
            hostUID = (int)reader.ReadVarUhInt();
            if (hostUID < 0)
            {
                throw new System.Exception("Forbidden value (" + hostUID + ") on element of SymbioticObjectAssociatedMessage.hostUID.");
            }

        }


    }
}








