using System.Collections.Generic;
using Giny.Core.Network.Messages;
using Giny.Protocol.Types;
using Giny.Core.IO.Interfaces;
using Giny.Protocol;
using Giny.Protocol.Enums;

namespace Giny.Protocol.Messages
{ 
    public class GameActionFightTriggerEffectMessage : GameActionFightDispellEffectMessage  
    { 
        public new const ushort Id = 6678;
        public override ushort MessageId => Id;


        public GameActionFightTriggerEffectMessage()
        {
        }
        public GameActionFightTriggerEffectMessage(short actionId,double sourceId,double targetId,bool verboseCast,int boostUID)
        {
            this.actionId = actionId;
            this.sourceId = sourceId;
            this.targetId = targetId;
            this.verboseCast = verboseCast;
            this.boostUID = boostUID;
        }
        public override void Serialize(IDataWriter writer)
        {
            base.Serialize(writer);
        }
        public override void Deserialize(IDataReader reader)
        {
            base.Deserialize(reader);
        }


    }
}








