using System.Collections.Generic;
using Giny.Core.Network.Messages;
using Giny.Protocol.Types;
using Giny.Core.IO.Interfaces;
using Giny.Protocol;
using Giny.Protocol.Enums;

namespace Giny.Protocol.Messages
{ 
    public class BreachEnterMessage : NetworkMessage  
    { 
        public  const ushort Id = 4929;
        public override ushort MessageId => Id;

        public long owner;

        public BreachEnterMessage()
        {
        }
        public BreachEnterMessage(long owner)
        {
            this.owner = owner;
        }
        public override void Serialize(IDataWriter writer)
        {
            if (owner < 0 || owner > 9.00719925474099E+15)
            {
                throw new System.Exception("Forbidden value (" + owner + ") on element owner.");
            }

            writer.WriteVarLong((long)owner);
        }
        public override void Deserialize(IDataReader reader)
        {
            owner = (long)reader.ReadVarUhLong();
            if (owner < 0 || owner > 9.00719925474099E+15)
            {
                throw new System.Exception("Forbidden value (" + owner + ") on element of BreachEnterMessage.owner.");
            }

        }


    }
}








