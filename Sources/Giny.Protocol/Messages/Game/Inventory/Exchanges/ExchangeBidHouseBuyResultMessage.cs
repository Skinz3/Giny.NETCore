using System.Collections.Generic;
using Giny.Core.Network.Messages;
using Giny.Protocol.Types;
using Giny.Core.IO.Interfaces;
using Giny.Protocol;
using Giny.Protocol.Enums;

namespace Giny.Protocol.Messages
{ 
    public class ExchangeBidHouseBuyResultMessage : NetworkMessage  
    { 
        public  const ushort Id = 1678;
        public override ushort MessageId => Id;

        public int uid;
        public bool bought;

        public ExchangeBidHouseBuyResultMessage()
        {
        }
        public ExchangeBidHouseBuyResultMessage(int uid,bool bought)
        {
            this.uid = uid;
            this.bought = bought;
        }
        public override void Serialize(IDataWriter writer)
        {
            if (uid < 0)
            {
                throw new System.Exception("Forbidden value (" + uid + ") on element uid.");
            }

            writer.WriteVarInt((int)uid);
            writer.WriteBoolean((bool)bought);
        }
        public override void Deserialize(IDataReader reader)
        {
            uid = (int)reader.ReadVarUhInt();
            if (uid < 0)
            {
                throw new System.Exception("Forbidden value (" + uid + ") on element of ExchangeBidHouseBuyResultMessage.uid.");
            }

            bought = (bool)reader.ReadBoolean();
        }


    }
}








