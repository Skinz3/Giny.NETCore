using System.Collections.Generic;
using Giny.Core.Network.Messages;
using Giny.Protocol.Types;
using Giny.Core.IO.Interfaces;
using Giny.Protocol;
using Giny.Protocol.Enums;

namespace Giny.Protocol.Messages
{ 
    public class GameActionFightPointsVariationMessage : AbstractGameActionMessage  
    { 
        public new const ushort Id = 272;
        public override ushort MessageId => Id;

        public double targetId;
        public short delta;

        public GameActionFightPointsVariationMessage()
        {
        }
        public GameActionFightPointsVariationMessage(double targetId,short delta,short actionId,double sourceId)
        {
            this.targetId = targetId;
            this.delta = delta;
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
            writer.WriteShort((short)delta);
        }
        public override void Deserialize(IDataReader reader)
        {
            base.Deserialize(reader);
            targetId = (double)reader.ReadDouble();
            if (targetId < -9.00719925474099E+15 || targetId > 9.00719925474099E+15)
            {
                throw new System.Exception("Forbidden value (" + targetId + ") on element of GameActionFightPointsVariationMessage.targetId.");
            }

            delta = (short)reader.ReadShort();
        }


    }
}








