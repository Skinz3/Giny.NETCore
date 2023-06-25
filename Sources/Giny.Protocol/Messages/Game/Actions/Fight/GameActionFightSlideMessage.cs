using System.Collections.Generic;
using Giny.Core.Network.Messages;
using Giny.Protocol.Types;
using Giny.Core.IO.Interfaces;
using Giny.Protocol;
using Giny.Protocol.Enums;

namespace Giny.Protocol.Messages
{ 
    public class GameActionFightSlideMessage : AbstractGameActionMessage  
    { 
        public new const ushort Id = 2323;
        public override ushort MessageId => Id;

        public double targetId;
        public short startCellId;
        public short endCellId;

        public GameActionFightSlideMessage()
        {
        }
        public GameActionFightSlideMessage(double targetId,short startCellId,short endCellId,short actionId,double sourceId)
        {
            this.targetId = targetId;
            this.startCellId = startCellId;
            this.endCellId = endCellId;
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
            if (startCellId < -1 || startCellId > 559)
            {
                throw new System.Exception("Forbidden value (" + startCellId + ") on element startCellId.");
            }

            writer.WriteShort((short)startCellId);
            if (endCellId < -1 || endCellId > 559)
            {
                throw new System.Exception("Forbidden value (" + endCellId + ") on element endCellId.");
            }

            writer.WriteShort((short)endCellId);
        }
        public override void Deserialize(IDataReader reader)
        {
            base.Deserialize(reader);
            targetId = (double)reader.ReadDouble();
            if (targetId < -9.00719925474099E+15 || targetId > 9.00719925474099E+15)
            {
                throw new System.Exception("Forbidden value (" + targetId + ") on element of GameActionFightSlideMessage.targetId.");
            }

            startCellId = (short)reader.ReadShort();
            if (startCellId < -1 || startCellId > 559)
            {
                throw new System.Exception("Forbidden value (" + startCellId + ") on element of GameActionFightSlideMessage.startCellId.");
            }

            endCellId = (short)reader.ReadShort();
            if (endCellId < -1 || endCellId > 559)
            {
                throw new System.Exception("Forbidden value (" + endCellId + ") on element of GameActionFightSlideMessage.endCellId.");
            }

        }


    }
}








