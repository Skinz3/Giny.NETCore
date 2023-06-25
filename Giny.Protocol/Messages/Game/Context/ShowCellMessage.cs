using System.Collections.Generic;
using Giny.Core.Network.Messages;
using Giny.Protocol.Types;
using Giny.Core.IO.Interfaces;
using Giny.Protocol;
using Giny.Protocol.Enums;

namespace Giny.Protocol.Messages
{ 
    public class ShowCellMessage : NetworkMessage  
    { 
        public  const ushort Id = 3204;
        public override ushort MessageId => Id;

        public double sourceId;
        public short cellId;

        public ShowCellMessage()
        {
        }
        public ShowCellMessage(double sourceId,short cellId)
        {
            this.sourceId = sourceId;
            this.cellId = cellId;
        }
        public override void Serialize(IDataWriter writer)
        {
            if (sourceId < -9.00719925474099E+15 || sourceId > 9.00719925474099E+15)
            {
                throw new System.Exception("Forbidden value (" + sourceId + ") on element sourceId.");
            }

            writer.WriteDouble((double)sourceId);
            if (cellId < 0 || cellId > 559)
            {
                throw new System.Exception("Forbidden value (" + cellId + ") on element cellId.");
            }

            writer.WriteVarShort((short)cellId);
        }
        public override void Deserialize(IDataReader reader)
        {
            sourceId = (double)reader.ReadDouble();
            if (sourceId < -9.00719925474099E+15 || sourceId > 9.00719925474099E+15)
            {
                throw new System.Exception("Forbidden value (" + sourceId + ") on element of ShowCellMessage.sourceId.");
            }

            cellId = (short)reader.ReadVarUhShort();
            if (cellId < 0 || cellId > 559)
            {
                throw new System.Exception("Forbidden value (" + cellId + ") on element of ShowCellMessage.cellId.");
            }

        }


    }
}








