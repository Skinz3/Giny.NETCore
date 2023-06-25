using System.Collections.Generic;
using Giny.Core.Network.Messages;
using Giny.Protocol.Types;
using Giny.Core.IO.Interfaces;
using Giny.Protocol;
using Giny.Protocol.Enums;

namespace Giny.Protocol.Messages
{ 
    public class GameActionFightDispellMessage : AbstractGameActionMessage  
    { 
        public new const ushort Id = 8988;
        public override ushort MessageId => Id;

        public double targetId;
        public bool verboseCast;

        public GameActionFightDispellMessage()
        {
        }
        public GameActionFightDispellMessage(double targetId,bool verboseCast,short actionId,double sourceId)
        {
            this.targetId = targetId;
            this.verboseCast = verboseCast;
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
            writer.WriteBoolean((bool)verboseCast);
        }
        public override void Deserialize(IDataReader reader)
        {
            base.Deserialize(reader);
            targetId = (double)reader.ReadDouble();
            if (targetId < -9.00719925474099E+15 || targetId > 9.00719925474099E+15)
            {
                throw new System.Exception("Forbidden value (" + targetId + ") on element of GameActionFightDispellMessage.targetId.");
            }

            verboseCast = (bool)reader.ReadBoolean();
        }


    }
}








