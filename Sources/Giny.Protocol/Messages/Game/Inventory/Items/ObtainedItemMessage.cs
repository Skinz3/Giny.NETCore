using System.Collections.Generic;
using Giny.Core.Network.Messages;
using Giny.Protocol.Types;
using Giny.Core.IO.Interfaces;
using Giny.Protocol;
using Giny.Protocol.Enums;

namespace Giny.Protocol.Messages
{ 
    public class ObtainedItemMessage : NetworkMessage  
    { 
        public  const ushort Id = 8927;
        public override ushort MessageId => Id;

        public short genericId;
        public int baseQuantity;

        public ObtainedItemMessage()
        {
        }
        public ObtainedItemMessage(short genericId,int baseQuantity)
        {
            this.genericId = genericId;
            this.baseQuantity = baseQuantity;
        }
        public override void Serialize(IDataWriter writer)
        {
            if (genericId < 0)
            {
                throw new System.Exception("Forbidden value (" + genericId + ") on element genericId.");
            }

            writer.WriteVarShort((short)genericId);
            if (baseQuantity < 0)
            {
                throw new System.Exception("Forbidden value (" + baseQuantity + ") on element baseQuantity.");
            }

            writer.WriteVarInt((int)baseQuantity);
        }
        public override void Deserialize(IDataReader reader)
        {
            genericId = (short)reader.ReadVarUhShort();
            if (genericId < 0)
            {
                throw new System.Exception("Forbidden value (" + genericId + ") on element of ObtainedItemMessage.genericId.");
            }

            baseQuantity = (int)reader.ReadVarUhInt();
            if (baseQuantity < 0)
            {
                throw new System.Exception("Forbidden value (" + baseQuantity + ") on element of ObtainedItemMessage.baseQuantity.");
            }

        }


    }
}








