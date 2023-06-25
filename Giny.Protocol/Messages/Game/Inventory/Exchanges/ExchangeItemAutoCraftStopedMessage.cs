using System.Collections.Generic;
using Giny.Core.Network.Messages;
using Giny.Protocol.Types;
using Giny.Core.IO.Interfaces;
using Giny.Protocol;
using Giny.Protocol.Enums;

namespace Giny.Protocol.Messages
{ 
    public class ExchangeItemAutoCraftStopedMessage : NetworkMessage  
    { 
        public  const ushort Id = 321;
        public override ushort MessageId => Id;

        public byte reason;

        public ExchangeItemAutoCraftStopedMessage()
        {
        }
        public ExchangeItemAutoCraftStopedMessage(byte reason)
        {
            this.reason = reason;
        }
        public override void Serialize(IDataWriter writer)
        {
            writer.WriteByte((byte)reason);
        }
        public override void Deserialize(IDataReader reader)
        {
            reason = (byte)reader.ReadByte();
        }


    }
}








