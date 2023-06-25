using System.Collections.Generic;
using Giny.Core.Network.Messages;
using Giny.Protocol.Types;
using Giny.Core.IO.Interfaces;
using Giny.Protocol;
using Giny.Protocol.Enums;

namespace Giny.Protocol.Messages
{ 
    public class GameActionFightStealKamaMessage : AbstractGameActionMessage  
    { 
        public new const ushort Id = 5660;
        public override ushort MessageId => Id;

        public double targetId;
        public long amount;

        public GameActionFightStealKamaMessage()
        {
        }
        public GameActionFightStealKamaMessage(double targetId,long amount,short actionId,double sourceId)
        {
            this.targetId = targetId;
            this.amount = amount;
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
            if (amount < 0 || amount > 9.00719925474099E+15)
            {
                throw new System.Exception("Forbidden value (" + amount + ") on element amount.");
            }

            writer.WriteVarLong((long)amount);
        }
        public override void Deserialize(IDataReader reader)
        {
            base.Deserialize(reader);
            targetId = (double)reader.ReadDouble();
            if (targetId < -9.00719925474099E+15 || targetId > 9.00719925474099E+15)
            {
                throw new System.Exception("Forbidden value (" + targetId + ") on element of GameActionFightStealKamaMessage.targetId.");
            }

            amount = (long)reader.ReadVarUhLong();
            if (amount < 0 || amount > 9.00719925474099E+15)
            {
                throw new System.Exception("Forbidden value (" + amount + ") on element of GameActionFightStealKamaMessage.amount.");
            }

        }


    }
}








