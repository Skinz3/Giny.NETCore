using System.Collections.Generic;
using Giny.Core.Network.Messages;
using Giny.Protocol.Types;
using Giny.Core.IO.Interfaces;
using Giny.Protocol;
using Giny.Protocol.Enums;

namespace Giny.Protocol.Messages
{ 
    public class TitleSelectRequestMessage : NetworkMessage  
    { 
        public  const ushort Id = 1528;
        public override ushort MessageId => Id;

        public short titleId;

        public TitleSelectRequestMessage()
        {
        }
        public TitleSelectRequestMessage(short titleId)
        {
            this.titleId = titleId;
        }
        public override void Serialize(IDataWriter writer)
        {
            if (titleId < 0)
            {
                throw new System.Exception("Forbidden value (" + titleId + ") on element titleId.");
            }

            writer.WriteVarShort((short)titleId);
        }
        public override void Deserialize(IDataReader reader)
        {
            titleId = (short)reader.ReadVarUhShort();
            if (titleId < 0)
            {
                throw new System.Exception("Forbidden value (" + titleId + ") on element of TitleSelectRequestMessage.titleId.");
            }

        }


    }
}








