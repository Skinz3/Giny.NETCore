using System.Collections.Generic;
using Giny.Core.Network.Messages;
using Giny.Protocol.Types;
using Giny.Core.IO.Interfaces;
using Giny.Protocol;
using Giny.Protocol.Enums;

namespace Giny.Protocol.Messages
{ 
    public class DocumentReadingBeginMessage : NetworkMessage  
    { 
        public  const ushort Id = 2676;
        public override ushort MessageId => Id;

        public short documentId;

        public DocumentReadingBeginMessage()
        {
        }
        public DocumentReadingBeginMessage(short documentId)
        {
            this.documentId = documentId;
        }
        public override void Serialize(IDataWriter writer)
        {
            if (documentId < 0)
            {
                throw new System.Exception("Forbidden value (" + documentId + ") on element documentId.");
            }

            writer.WriteVarShort((short)documentId);
        }
        public override void Deserialize(IDataReader reader)
        {
            documentId = (short)reader.ReadVarUhShort();
            if (documentId < 0)
            {
                throw new System.Exception("Forbidden value (" + documentId + ") on element of DocumentReadingBeginMessage.documentId.");
            }

        }


    }
}








