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
        public const ushort Id = 3512;
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
            if (kamasTotal < 0 || kamasTotal > 9007199254740992)
            {
                throw new System.Exception("Forbidden value (" + kamasTotal + ") on element kamasTotal.");
            }

            writer.WriteVarLong((long)kamasTotal);
        }
        public override void Deserialize(IDataReader reader)
        {
            kamasTotal = (long)reader.ReadVarUhLong();
            if (kamasTotal < 0 || kamasTotal > 9007199254740992)
            {
                throw new System.Exception("Forbidden value (" + kamasTotal + ") on element of StorageKamasUpdateMessage.kamasTotal.");
            }

        }

    }
}


