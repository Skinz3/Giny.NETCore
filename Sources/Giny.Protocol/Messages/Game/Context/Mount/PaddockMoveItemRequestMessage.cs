using System.Collections.Generic;
using Giny.Core.Network.Messages;
using Giny.Protocol.Types;
using Giny.Core.IO.Interfaces;
using Giny.Protocol;
using Giny.Protocol.Enums;

namespace Giny.Protocol.Messages
{ 
    public class PaddockMoveItemRequestMessage : NetworkMessage  
    { 
        public  const ushort Id = 9794;
        public override ushort MessageId => Id;

        public short oldCellId;
        public short newCellId;

        public PaddockMoveItemRequestMessage()
        {
        }
        public PaddockMoveItemRequestMessage(short oldCellId,short newCellId)
        {
            this.oldCellId = oldCellId;
            this.newCellId = newCellId;
        }
        public override void Serialize(IDataWriter writer)
        {
            if (oldCellId < 0 || oldCellId > 559)
            {
                throw new System.Exception("Forbidden value (" + oldCellId + ") on element oldCellId.");
            }

            writer.WriteVarShort((short)oldCellId);
            if (newCellId < 0 || newCellId > 559)
            {
                throw new System.Exception("Forbidden value (" + newCellId + ") on element newCellId.");
            }

            writer.WriteVarShort((short)newCellId);
        }
        public override void Deserialize(IDataReader reader)
        {
            oldCellId = (short)reader.ReadVarUhShort();
            if (oldCellId < 0 || oldCellId > 559)
            {
                throw new System.Exception("Forbidden value (" + oldCellId + ") on element of PaddockMoveItemRequestMessage.oldCellId.");
            }

            newCellId = (short)reader.ReadVarUhShort();
            if (newCellId < 0 || newCellId > 559)
            {
                throw new System.Exception("Forbidden value (" + newCellId + ") on element of PaddockMoveItemRequestMessage.newCellId.");
            }

        }


    }
}








