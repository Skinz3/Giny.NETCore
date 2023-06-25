using System.Collections.Generic;
using Giny.Core.Network.Messages;
using Giny.Protocol.Types;
using Giny.Core.IO.Interfaces;
using Giny.Protocol;
using Giny.Protocol.Enums;

namespace Giny.Protocol.Messages
{ 
    public class ExchangeWaitingResultMessage : NetworkMessage  
    { 
        public  const ushort Id = 1456;
        public override ushort MessageId => Id;

        public bool bwait;

        public ExchangeWaitingResultMessage()
        {
        }
        public ExchangeWaitingResultMessage(bool bwait)
        {
            this.bwait = bwait;
        }
        public override void Serialize(IDataWriter writer)
        {
            writer.WriteBoolean((bool)bwait);
        }
        public override void Deserialize(IDataReader reader)
        {
            bwait = (bool)reader.ReadBoolean();
        }


    }
}








