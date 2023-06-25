using System.Collections.Generic;
using Giny.Core.Network.Messages;
using Giny.Protocol.Types;
using Giny.Core.IO.Interfaces;
using Giny.Protocol;
using Giny.Protocol.Enums;

namespace Giny.Protocol.Messages
{ 
    public class ObtainedItemWithBonusMessage : ObtainedItemMessage  
    { 
        public new const ushort Id = 9026;
        public override ushort MessageId => Id;

        public int bonusQuantity;

        public ObtainedItemWithBonusMessage()
        {
        }
        public ObtainedItemWithBonusMessage(int bonusQuantity,short genericId,int baseQuantity)
        {
            this.bonusQuantity = bonusQuantity;
            this.genericId = genericId;
            this.baseQuantity = baseQuantity;
        }
        public override void Serialize(IDataWriter writer)
        {
            base.Serialize(writer);
            if (bonusQuantity < 0)
            {
                throw new System.Exception("Forbidden value (" + bonusQuantity + ") on element bonusQuantity.");
            }

            writer.WriteVarInt((int)bonusQuantity);
        }
        public override void Deserialize(IDataReader reader)
        {
            base.Deserialize(reader);
            bonusQuantity = (int)reader.ReadVarUhInt();
            if (bonusQuantity < 0)
            {
                throw new System.Exception("Forbidden value (" + bonusQuantity + ") on element of ObtainedItemWithBonusMessage.bonusQuantity.");
            }

        }


    }
}








