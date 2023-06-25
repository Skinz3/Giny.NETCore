using System.Collections.Generic;
using Giny.Core.Network.Messages;
using Giny.Protocol.Types;
using Giny.Core.IO.Interfaces;
using Giny.Protocol;
using Giny.Protocol.Enums;

namespace Giny.Protocol.Messages
{ 
    public class GameActionFightCarryCharacterMessage : AbstractGameActionMessage  
    { 
        public new const ushort Id = 3830;
        public override ushort MessageId => Id;

        public double targetId;
        public short cellId;

        public GameActionFightCarryCharacterMessage()
        {
        }
        public GameActionFightCarryCharacterMessage(double targetId,short cellId,short actionId,double sourceId)
        {
            this.targetId = targetId;
            this.cellId = cellId;
            this.actionId = actionId;
            this.sourceId = sourceId;
        }
        public override void Serialize(IDataWriter writer)
        {
            base.Serialize(writer);
            if (targetId < -9.00719925474099E+15 || targetId > 9.00719925474099E+15)
            {
                throw new System.Exception("Forbidden value (" + targetId + ") on element targetId.");
            }

            writer.WriteDouble((double)targetId);
            if (cellId < -1 || cellId > 559)
            {
                throw new System.Exception("Forbidden value (" + cellId + ") on element cellId.");
            }

            writer.WriteShort((short)cellId);
        }
        public override void Deserialize(IDataReader reader)
        {
            base.Deserialize(reader);
            targetId = (double)reader.ReadDouble();
            if (targetId < -9.00719925474099E+15 || targetId > 9.00719925474099E+15)
            {
                throw new System.Exception("Forbidden value (" + targetId + ") on element of GameActionFightCarryCharacterMessage.targetId.");
            }

            cellId = (short)reader.ReadShort();
            if (cellId < -1 || cellId > 559)
            {
                throw new System.Exception("Forbidden value (" + cellId + ") on element of GameActionFightCarryCharacterMessage.cellId.");
            }

        }


    }
}








