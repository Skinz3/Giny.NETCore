using System.Collections.Generic;
using Giny.Core.Network.Messages;
using Giny.Protocol.Types;
using Giny.Core.IO.Interfaces;
using Giny.Protocol;
using Giny.Protocol.Enums;

namespace Giny.Protocol.Messages
{ 
    public class GameActionFightLifePointsLostMessage : AbstractGameActionMessage  
    { 
        public new const ushort Id = 3628;
        public override ushort MessageId => Id;

        public double targetId;
        public int loss;
        public int permanentDamages;
        public int elementId;

        public GameActionFightLifePointsLostMessage()
        {
        }
        public GameActionFightLifePointsLostMessage(double targetId,int loss,int permanentDamages,int elementId,short actionId,double sourceId)
        {
            this.targetId = targetId;
            this.loss = loss;
            this.permanentDamages = permanentDamages;
            this.elementId = elementId;
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
            if (loss < 0)
            {
                throw new System.Exception("Forbidden value (" + loss + ") on element loss.");
            }

            writer.WriteVarInt((int)loss);
            if (permanentDamages < 0)
            {
                throw new System.Exception("Forbidden value (" + permanentDamages + ") on element permanentDamages.");
            }

            writer.WriteVarInt((int)permanentDamages);
            writer.WriteVarInt((int)elementId);
        }
        public override void Deserialize(IDataReader reader)
        {
            base.Deserialize(reader);
            targetId = (double)reader.ReadDouble();
            if (targetId < -9.00719925474099E+15 || targetId > 9.00719925474099E+15)
            {
                throw new System.Exception("Forbidden value (" + targetId + ") on element of GameActionFightLifePointsLostMessage.targetId.");
            }

            loss = (int)reader.ReadVarUhInt();
            if (loss < 0)
            {
                throw new System.Exception("Forbidden value (" + loss + ") on element of GameActionFightLifePointsLostMessage.loss.");
            }

            permanentDamages = (int)reader.ReadVarUhInt();
            if (permanentDamages < 0)
            {
                throw new System.Exception("Forbidden value (" + permanentDamages + ") on element of GameActionFightLifePointsLostMessage.permanentDamages.");
            }

            elementId = (int)reader.ReadVarInt();
        }


    }
}








