using System.Collections.Generic;
using Giny.Core.Network.Messages;
using Giny.Protocol.Types;
using Giny.Core.IO.Interfaces;
using Giny.Protocol;
using Giny.Protocol.Enums;

namespace Giny.Protocol.Messages
{ 
    public class GameActionFightExchangePositionsMessage : AbstractGameActionMessage  
    { 
        public new const ushort Id = 7475;
        public override ushort MessageId => Id;

        public double targetId;
        public short casterCellId;
        public short targetCellId;

        public GameActionFightExchangePositionsMessage()
        {
        }
        public GameActionFightExchangePositionsMessage(double targetId,short casterCellId,short targetCellId,short actionId,double sourceId)
        {
            this.targetId = targetId;
            this.casterCellId = casterCellId;
            this.targetCellId = targetCellId;
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
            if (casterCellId < -1 || casterCellId > 559)
            {
                throw new System.Exception("Forbidden value (" + casterCellId + ") on element casterCellId.");
            }

            writer.WriteShort((short)casterCellId);
            if (targetCellId < -1 || targetCellId > 559)
            {
                throw new System.Exception("Forbidden value (" + targetCellId + ") on element targetCellId.");
            }

            writer.WriteShort((short)targetCellId);
        }
        public override void Deserialize(IDataReader reader)
        {
            base.Deserialize(reader);
            targetId = (double)reader.ReadDouble();
            if (targetId < -9.00719925474099E+15 || targetId > 9.00719925474099E+15)
            {
                throw new System.Exception("Forbidden value (" + targetId + ") on element of GameActionFightExchangePositionsMessage.targetId.");
            }

            casterCellId = (short)reader.ReadShort();
            if (casterCellId < -1 || casterCellId > 559)
            {
                throw new System.Exception("Forbidden value (" + casterCellId + ") on element of GameActionFightExchangePositionsMessage.casterCellId.");
            }

            targetCellId = (short)reader.ReadShort();
            if (targetCellId < -1 || targetCellId > 559)
            {
                throw new System.Exception("Forbidden value (" + targetCellId + ") on element of GameActionFightExchangePositionsMessage.targetCellId.");
            }

        }


    }
}








