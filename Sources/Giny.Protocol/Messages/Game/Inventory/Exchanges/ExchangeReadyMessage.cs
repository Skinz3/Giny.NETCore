using System.Collections.Generic;
using Giny.Core.Network.Messages;
using Giny.Protocol.Types;
using Giny.Core.IO.Interfaces;
using Giny.Protocol;
using Giny.Protocol.Enums;

namespace Giny.Protocol.Messages
{ 
    public class ExchangeReadyMessage : NetworkMessage  
    { 
        public  const ushort Id = 8150;
        public override ushort MessageId => Id;

        public bool ready;
        public short step;

        public ExchangeReadyMessage()
        {
        }
        public ExchangeReadyMessage(bool ready,short step)
        {
            this.ready = ready;
            this.step = step;
        }
        public override void Serialize(IDataWriter writer)
        {
            writer.WriteBoolean((bool)ready);
            if (step < 0)
            {
                throw new System.Exception("Forbidden value (" + step + ") on element step.");
            }

            writer.WriteVarShort((short)step);
        }
        public override void Deserialize(IDataReader reader)
        {
            ready = (bool)reader.ReadBoolean();
            step = (short)reader.ReadVarUhShort();
            if (step < 0)
            {
                throw new System.Exception("Forbidden value (" + step + ") on element of ExchangeReadyMessage.step.");
            }

        }


    }
}








