using System.Collections.Generic;
using Giny.Core.Network.Messages;
using Giny.Protocol.Types;
using Giny.Core.IO.Interfaces;
using Giny.Protocol;
using Giny.Protocol.Enums;

namespace Giny.Protocol.Messages
{ 
    public class PrismFightAddedMessage : NetworkMessage  
    { 
        public  const ushort Id = 8522;
        public override ushort MessageId => Id;

        public PrismFightersInformation fight;

        public PrismFightAddedMessage()
        {
        }
        public PrismFightAddedMessage(PrismFightersInformation fight)
        {
            this.fight = fight;
        }
        public override void Serialize(IDataWriter writer)
        {
            fight.Serialize(writer);
        }
        public override void Deserialize(IDataReader reader)
        {
            fight = new PrismFightersInformation();
            fight.Deserialize(reader);
        }


    }
}








