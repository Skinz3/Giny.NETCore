using System.Collections.Generic;
using Giny.Core.Network.Messages;
using Giny.Protocol.Types;
using Giny.Core.IO.Interfaces;
using Giny.Protocol;
using Giny.Protocol.Enums;

namespace Giny.Protocol.Messages
{ 
    public class ChatSmileyRequestMessage : NetworkMessage  
    { 
        public  const ushort Id = 943;
        public override ushort MessageId => Id;

        public short smileyId;

        public ChatSmileyRequestMessage()
        {
        }
        public ChatSmileyRequestMessage(short smileyId)
        {
            this.smileyId = smileyId;
        }
        public override void Serialize(IDataWriter writer)
        {
            if (smileyId < 0)
            {
                throw new System.Exception("Forbidden value (" + smileyId + ") on element smileyId.");
            }

            writer.WriteVarShort((short)smileyId);
        }
        public override void Deserialize(IDataReader reader)
        {
            smileyId = (short)reader.ReadVarUhShort();
            if (smileyId < 0)
            {
                throw new System.Exception("Forbidden value (" + smileyId + ") on element of ChatSmileyRequestMessage.smileyId.");
            }

        }


    }
}








