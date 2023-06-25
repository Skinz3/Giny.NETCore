using System.Collections.Generic;
using Giny.Core.Network.Messages;
using Giny.Protocol.Types;
using Giny.Core.IO.Interfaces;
using Giny.Protocol;
using Giny.Protocol.Enums;

namespace Giny.Protocol.Messages
{ 
    public class StorageKamasUpdateMessage : NetworkMessage  
    { 
        public  const ushort Id = 8712;
        public override ushort MessageId => Id;

        public long kamasTotal;

        public StorageKamasUpdateMessage()
        {
        }
        public StorageKamasUpdateMessage(long kamasTotal)
        {
            this.kamasTotal = kamasTotal;
        }
        public override void Serialize(IDataWriter writer)
        {
            if (kamasTotal < 0 || kamasTotal > 9.00719925474099E+15)
            {
                throw new System.Exception("Forbidden value (" + kamasTotal + ") on element kamasTotal.");
            }

            writer.WriteVarLong((long)kamasTotal);
        }
        public override void Deserialize(IDataReader reader)
        {
            kamasTotal = (long)reader.ReadVarUhLong();
            if (kamasTotal < 0 || kamasTotal > 9.00719925474099E+15)
            {
                throw new System.Exception("Forbidden value (" + kamasTotal + ") on element of StorageKamasUpdateMessage.kamasTotal.");
            }

        }


    }
}








