using System.Collections.Generic;
using Giny.Core.Network.Messages;
using Giny.Protocol.Types;
using Giny.Core.IO.Interfaces;
using Giny.Protocol;
using Giny.Protocol.Enums;

namespace Giny.Protocol.Messages
{ 
    public class InteractiveUseRequestMessage : NetworkMessage  
    { 
        public  const ushort Id = 7841;
        public override ushort MessageId => Id;

        public int elemId;
        public int skillInstanceUid;

        public InteractiveUseRequestMessage()
        {
        }
        public InteractiveUseRequestMessage(int elemId,int skillInstanceUid)
        {
            this.elemId = elemId;
            this.skillInstanceUid = skillInstanceUid;
        }
        public override void Serialize(IDataWriter writer)
        {
            if (elemId < 0)
            {
                throw new System.Exception("Forbidden value (" + elemId + ") on element elemId.");
            }

            writer.WriteVarInt((int)elemId);
            if (skillInstanceUid < 0)
            {
                throw new System.Exception("Forbidden value (" + skillInstanceUid + ") on element skillInstanceUid.");
            }

            writer.WriteVarInt((int)skillInstanceUid);
        }
        public override void Deserialize(IDataReader reader)
        {
            elemId = (int)reader.ReadVarUhInt();
            if (elemId < 0)
            {
                throw new System.Exception("Forbidden value (" + elemId + ") on element of InteractiveUseRequestMessage.elemId.");
            }

            skillInstanceUid = (int)reader.ReadVarUhInt();
            if (skillInstanceUid < 0)
            {
                throw new System.Exception("Forbidden value (" + skillInstanceUid + ") on element of InteractiveUseRequestMessage.skillInstanceUid.");
            }

        }


    }
}








