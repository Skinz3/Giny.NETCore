using System.Collections.Generic;
using Giny.Core.Network.Messages;
using Giny.Protocol.Types;
using Giny.Core.IO.Interfaces;
using Giny.Protocol;
using Giny.Protocol.Enums;

namespace Giny.Protocol.Messages
{ 
    public class ExchangeStoppedMessage : NetworkMessage  
    { 
        public  const ushort Id = 6845;
        public override ushort MessageId => Id;

        public long id;

        public ExchangeStoppedMessage()
        {
        }
        public ExchangeStoppedMessage(long id)
        {
            this.id = id;
        }
        public override void Serialize(IDataWriter writer)
        {
            if (id < 0 || id > 9.00719925474099E+15)
            {
                throw new System.Exception("Forbidden value (" + id + ") on element id.");
            }

            writer.WriteVarLong((long)id);
        }
        public override void Deserialize(IDataReader reader)
        {
            id = (long)reader.ReadVarUhLong();
            if (id < 0 || id > 9.00719925474099E+15)
            {
                throw new System.Exception("Forbidden value (" + id + ") on element of ExchangeStoppedMessage.id.");
            }

        }


    }
}








