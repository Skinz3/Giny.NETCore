using System.Collections.Generic;
using Giny.Core.Network.Messages;
using Giny.Protocol.Types;
using Giny.Core.IO.Interfaces;
using Giny.Protocol;
using Giny.Protocol.Enums;

namespace Giny.Protocol.Messages
{ 
    public class GuildGetInformationsMessage : NetworkMessage  
    { 
        public  const ushort Id = 5912;
        public override ushort MessageId => Id;

        public byte infoType;

        public GuildGetInformationsMessage()
        {
        }
        public GuildGetInformationsMessage(byte infoType)
        {
            this.infoType = infoType;
        }
        public override void Serialize(IDataWriter writer)
        {
            writer.WriteByte((byte)infoType);
        }
        public override void Deserialize(IDataReader reader)
        {
            infoType = (byte)reader.ReadByte();
            if (infoType < 0)
            {
                throw new System.Exception("Forbidden value (" + infoType + ") on element of GuildGetInformationsMessage.infoType.");
            }

        }


    }
}








