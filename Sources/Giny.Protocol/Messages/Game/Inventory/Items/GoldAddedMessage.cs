using System.Collections.Generic;
using Giny.Core.Network.Messages;
using Giny.Protocol.Types;
using Giny.Core.IO.Interfaces;
using Giny.Protocol;
using Giny.Protocol.Enums;

namespace Giny.Protocol.Messages
{ 
    public class GoldAddedMessage : NetworkMessage  
    { 
        public  const ushort Id = 5700;
        public override ushort MessageId => Id;

        public GoldItem gold;

        public GoldAddedMessage()
        {
        }
        public GoldAddedMessage(GoldItem gold)
        {
            this.gold = gold;
        }
        public override void Serialize(IDataWriter writer)
        {
            gold.Serialize(writer);
        }
        public override void Deserialize(IDataReader reader)
        {
            gold = new GoldItem();
            gold.Deserialize(reader);
        }


    }
}








