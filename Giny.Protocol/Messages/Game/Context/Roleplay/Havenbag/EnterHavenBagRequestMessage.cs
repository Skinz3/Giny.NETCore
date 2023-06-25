using System.Collections.Generic;
using Giny.Core.Network.Messages;
using Giny.Protocol.Types;
using Giny.Core.IO.Interfaces;
using Giny.Protocol;
using Giny.Protocol.Enums;

namespace Giny.Protocol.Messages
{ 
    public class EnterHavenBagRequestMessage : NetworkMessage  
    { 
        public  const ushort Id = 180;
        public override ushort MessageId => Id;

        public long havenBagOwner;

        public EnterHavenBagRequestMessage()
        {
        }
        public EnterHavenBagRequestMessage(long havenBagOwner)
        {
            this.havenBagOwner = havenBagOwner;
        }
        public override void Serialize(IDataWriter writer)
        {
            if (havenBagOwner < 0 || havenBagOwner > 9.00719925474099E+15)
            {
                throw new System.Exception("Forbidden value (" + havenBagOwner + ") on element havenBagOwner.");
            }

            writer.WriteVarLong((long)havenBagOwner);
        }
        public override void Deserialize(IDataReader reader)
        {
            havenBagOwner = (long)reader.ReadVarUhLong();
            if (havenBagOwner < 0 || havenBagOwner > 9.00719925474099E+15)
            {
                throw new System.Exception("Forbidden value (" + havenBagOwner + ") on element of EnterHavenBagRequestMessage.havenBagOwner.");
            }

        }


    }
}








