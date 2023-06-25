using System.Collections.Generic;
using Giny.Core.Network.Messages;
using Giny.Protocol.Types;
using Giny.Core.IO.Interfaces;
using Giny.Protocol;
using Giny.Protocol.Enums;

namespace Giny.Protocol.Messages
{ 
    public class HouseKickIndoorMerchantRequestMessage : NetworkMessage  
    { 
        public  const ushort Id = 9016;
        public override ushort MessageId => Id;

        public short cellId;

        public HouseKickIndoorMerchantRequestMessage()
        {
        }
        public HouseKickIndoorMerchantRequestMessage(short cellId)
        {
            this.cellId = cellId;
        }
        public override void Serialize(IDataWriter writer)
        {
            if (cellId < 0 || cellId > 559)
            {
                throw new System.Exception("Forbidden value (" + cellId + ") on element cellId.");
            }

            writer.WriteVarShort((short)cellId);
        }
        public override void Deserialize(IDataReader reader)
        {
            cellId = (short)reader.ReadVarUhShort();
            if (cellId < 0 || cellId > 559)
            {
                throw new System.Exception("Forbidden value (" + cellId + ") on element of HouseKickIndoorMerchantRequestMessage.cellId.");
            }

        }


    }
}








