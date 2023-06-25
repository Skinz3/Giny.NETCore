using System.Collections.Generic;
using Giny.Core.Network.Messages;
using Giny.Protocol.Types;
using Giny.Core.IO.Interfaces;
using Giny.Protocol;
using Giny.Protocol.Enums;

namespace Giny.Protocol.Messages
{ 
    public class BreachKickRequestMessage : NetworkMessage  
    { 
        public  const ushort Id = 155;
        public override ushort MessageId => Id;

        public long target;

        public BreachKickRequestMessage()
        {
        }
        public BreachKickRequestMessage(long target)
        {
            this.target = target;
        }
        public override void Serialize(IDataWriter writer)
        {
            if (target < 0 || target > 9.00719925474099E+15)
            {
                throw new System.Exception("Forbidden value (" + target + ") on element target.");
            }

            writer.WriteVarLong((long)target);
        }
        public override void Deserialize(IDataReader reader)
        {
            target = (long)reader.ReadVarUhLong();
            if (target < 0 || target > 9.00719925474099E+15)
            {
                throw new System.Exception("Forbidden value (" + target + ") on element of BreachKickRequestMessage.target.");
            }

        }


    }
}








