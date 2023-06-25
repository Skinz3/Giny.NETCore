using System.Collections.Generic;
using Giny.Core.Network.Messages;
using Giny.Protocol.Types;
using Giny.Core.IO.Interfaces;
using Giny.Protocol;
using Giny.Protocol.Enums;

namespace Giny.Protocol.Messages
{ 
    public class ExchangeCraftCountRequestMessage : NetworkMessage  
    { 
        public  const ushort Id = 1472;
        public override ushort MessageId => Id;

        public int count;

        public ExchangeCraftCountRequestMessage()
        {
        }
        public ExchangeCraftCountRequestMessage(int count)
        {
            this.count = count;
        }
        public override void Serialize(IDataWriter writer)
        {
            writer.WriteVarInt((int)count);
        }
        public override void Deserialize(IDataReader reader)
        {
            count = (int)reader.ReadVarInt();
        }


    }
}








