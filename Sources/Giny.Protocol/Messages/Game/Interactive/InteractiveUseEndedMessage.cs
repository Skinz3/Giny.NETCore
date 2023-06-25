using System.Collections.Generic;
using Giny.Core.Network.Messages;
using Giny.Protocol.Types;
using Giny.Core.IO.Interfaces;
using Giny.Protocol;
using Giny.Protocol.Enums;

namespace Giny.Protocol.Messages
{ 
    public class InteractiveUseEndedMessage : NetworkMessage  
    { 
        public  const ushort Id = 4987;
        public override ushort MessageId => Id;

        public int elemId;
        public short skillId;

        public InteractiveUseEndedMessage()
        {
        }
        public InteractiveUseEndedMessage(int elemId,short skillId)
        {
            this.elemId = elemId;
            this.skillId = skillId;
        }
        public override void Serialize(IDataWriter writer)
        {
            if (elemId < 0)
            {
                throw new System.Exception("Forbidden value (" + elemId + ") on element elemId.");
            }

            writer.WriteVarInt((int)elemId);
            if (skillId < 0)
            {
                throw new System.Exception("Forbidden value (" + skillId + ") on element skillId.");
            }

            writer.WriteVarShort((short)skillId);
        }
        public override void Deserialize(IDataReader reader)
        {
            elemId = (int)reader.ReadVarUhInt();
            if (elemId < 0)
            {
                throw new System.Exception("Forbidden value (" + elemId + ") on element of InteractiveUseEndedMessage.elemId.");
            }

            skillId = (short)reader.ReadVarUhShort();
            if (skillId < 0)
            {
                throw new System.Exception("Forbidden value (" + skillId + ") on element of InteractiveUseEndedMessage.skillId.");
            }

        }


    }
}








