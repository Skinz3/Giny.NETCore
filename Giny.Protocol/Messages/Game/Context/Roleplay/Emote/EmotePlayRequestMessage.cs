using System.Collections.Generic;
using Giny.Core.Network.Messages;
using Giny.Protocol.Types;
using Giny.Core.IO.Interfaces;
using Giny.Protocol;
using Giny.Protocol.Enums;

namespace Giny.Protocol.Messages
{ 
    public class EmotePlayRequestMessage : NetworkMessage  
    { 
        public  const ushort Id = 3018;
        public override ushort MessageId => Id;

        public short emoteId;

        public EmotePlayRequestMessage()
        {
        }
        public EmotePlayRequestMessage(short emoteId)
        {
            this.emoteId = emoteId;
        }
        public override void Serialize(IDataWriter writer)
        {
            if (emoteId < 0 || emoteId > 65535)
            {
                throw new System.Exception("Forbidden value (" + emoteId + ") on element emoteId.");
            }

            writer.WriteShort((short)emoteId);
        }
        public override void Deserialize(IDataReader reader)
        {
            emoteId = (short)reader.ReadUShort();
            if (emoteId < 0 || emoteId > 65535)
            {
                throw new System.Exception("Forbidden value (" + emoteId + ") on element of EmotePlayRequestMessage.emoteId.");
            }

        }


    }
}








