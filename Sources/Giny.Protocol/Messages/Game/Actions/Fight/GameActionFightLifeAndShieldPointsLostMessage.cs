using System.Collections.Generic;
using Giny.Core.Network.Messages;
using Giny.Protocol.Types;
using Giny.Core.IO.Interfaces;
using Giny.Protocol;
using Giny.Protocol.Enums;

namespace Giny.Protocol.Messages
{ 
    public class GameActionFightLifeAndShieldPointsLostMessage : GameActionFightLifePointsLostMessage  
    { 
        public new const ushort Id = 894;
        public override ushort MessageId => Id;

        public short shieldLoss;

        public GameActionFightLifeAndShieldPointsLostMessage()
        {
        }
        public GameActionFightLifeAndShieldPointsLostMessage(short shieldLoss,short actionId,double sourceId,double targetId,int loss,int permanentDamages,int elementId)
        {
            this.shieldLoss = shieldLoss;
            this.actionId = actionId;
            this.sourceId = sourceId;
            this.targetId = targetId;
            this.loss = loss;
            this.permanentDamages = permanentDamages;
            this.elementId = elementId;
        }
        public override void Serialize(IDataWriter writer)
        {
            base.Serialize(writer);
            if (shieldLoss < 0)
            {
                throw new System.Exception("Forbidden value (" + shieldLoss + ") on element shieldLoss.");
            }

            writer.WriteVarShort((short)shieldLoss);
        }
        public override void Deserialize(IDataReader reader)
        {
            base.Deserialize(reader);
            shieldLoss = (short)reader.ReadVarUhShort();
            if (shieldLoss < 0)
            {
                throw new System.Exception("Forbidden value (" + shieldLoss + ") on element of GameActionFightLifeAndShieldPointsLostMessage.shieldLoss.");
            }

        }


    }
}








