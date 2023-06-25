using System.Collections.Generic;
using Giny.Core.Network.Messages;
using Giny.Protocol.Types;
using Giny.Core.IO.Interfaces;
using Giny.Protocol;
using Giny.Protocol.Enums;

namespace Giny.Protocol.Messages
{ 
    public class BreachBonusMessage : NetworkMessage  
    { 
        public  const ushort Id = 1240;
        public override ushort MessageId => Id;

        public ObjectEffectInteger bonus;

        public BreachBonusMessage()
        {
        }
        public BreachBonusMessage(ObjectEffectInteger bonus)
        {
            this.bonus = bonus;
        }
        public override void Serialize(IDataWriter writer)
        {
            bonus.Serialize(writer);
        }
        public override void Deserialize(IDataReader reader)
        {
            bonus = new ObjectEffectInteger();
            bonus.Deserialize(reader);
        }


    }
}








