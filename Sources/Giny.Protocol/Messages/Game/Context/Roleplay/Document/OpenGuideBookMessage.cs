using System.Collections.Generic;
using Giny.Core.Network.Messages;
using Giny.Protocol.Types;
using Giny.Core.IO.Interfaces;
using Giny.Protocol;
using Giny.Protocol.Enums;

namespace Giny.Protocol.Messages
{
    public class OpenGuideBookMessage : NetworkMessage
    {
        public const ushort Id = 3895;
        public override ushort MessageId => Id;

        public short articleId;

        public OpenGuideBookMessage()
        {
        }
        public OpenGuideBookMessage(short articleId)
        {
            this.articleId = articleId;
        }
        public override void Serialize(IDataWriter writer)
        {
            if (articleId < 0)
            {
                throw new System.Exception("Forbidden value (" + articleId + ") on element articleId.");
            }

            writer.WriteVarShort((short)articleId);
        }
        public override void Deserialize(IDataReader reader)
        {
            articleId = (short)reader.ReadVarUhShort();
            if (articleId < 0)
            {
                throw new System.Exception("Forbidden value (" + articleId + ") on element of OpenGuideBookMessage.articleId.");
            }

        }

    }
}


