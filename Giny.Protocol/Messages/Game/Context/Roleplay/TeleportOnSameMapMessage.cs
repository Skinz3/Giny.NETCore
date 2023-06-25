using System.Collections.Generic;
using Giny.Core.Network.Messages;
using Giny.Protocol.Types;
using Giny.Core.IO.Interfaces;
using Giny.Protocol;
using Giny.Protocol.Enums;

namespace Giny.Protocol.Messages
{ 
    public class TeleportOnSameMapMessage : NetworkMessage  
    { 
        public  const ushort Id = 2720;
        public override ushort MessageId => Id;

        public double targetId;
        public short cellId;

        public TeleportOnSameMapMessage()
        {
        }
        public TeleportOnSameMapMessage(double targetId,short cellId)
        {
            this.targetId = targetId;
            this.cellId = cellId;
        }
        public override void Serialize(IDataWriter writer)
        {
            if (targetId < -9.00719925474099E+15 || targetId > 9.00719925474099E+15)
            {
                throw new System.Exception("Forbidden value (" + targetId + ") on element targetId.");
            }

            writer.WriteDouble((double)targetId);
            if (cellId < 0 || cellId > 559)
            {
                throw new System.Exception("Forbidden value (" + cellId + ") on element cellId.");
            }

            writer.WriteVarShort((short)cellId);
        }
        public override void Deserialize(IDataReader reader)
        {
            targetId = (double)reader.ReadDouble();
            if (targetId < -9.00719925474099E+15 || targetId > 9.00719925474099E+15)
            {
                throw new System.Exception("Forbidden value (" + targetId + ") on element of TeleportOnSameMapMessage.targetId.");
            }

            cellId = (short)reader.ReadVarUhShort();
            if (cellId < 0 || cellId > 559)
            {
                throw new System.Exception("Forbidden value (" + cellId + ") on element of TeleportOnSameMapMessage.cellId.");
            }

        }


    }
}








