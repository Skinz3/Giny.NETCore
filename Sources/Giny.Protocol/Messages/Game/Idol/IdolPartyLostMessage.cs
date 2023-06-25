using System.Collections.Generic;
using Giny.Core.Network.Messages;
using Giny.Protocol.Types;
using Giny.Core.IO.Interfaces;
using Giny.Protocol;
using Giny.Protocol.Enums;

namespace Giny.Protocol.Messages
{ 
    public class IdolPartyLostMessage : NetworkMessage  
    { 
        public  const ushort Id = 4969;
        public override ushort MessageId => Id;

        public short idolId;

        public IdolPartyLostMessage()
        {
        }
        public IdolPartyLostMessage(short idolId)
        {
            this.idolId = idolId;
        }
        public override void Serialize(IDataWriter writer)
        {
            if (idolId < 0)
            {
                throw new System.Exception("Forbidden value (" + idolId + ") on element idolId.");
            }

            writer.WriteVarShort((short)idolId);
        }
        public override void Deserialize(IDataReader reader)
        {
            idolId = (short)reader.ReadVarUhShort();
            if (idolId < 0)
            {
                throw new System.Exception("Forbidden value (" + idolId + ") on element of IdolPartyLostMessage.idolId.");
            }

        }


    }
}








