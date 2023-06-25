using System.Collections.Generic;
using Giny.Core.Network.Messages;
using Giny.Protocol.Types;
using Giny.Core.IO.Interfaces;
using Giny.Protocol;
using Giny.Protocol.Enums;

namespace Giny.Protocol.Messages
{ 
    public class PrismUseRequestMessage : NetworkMessage  
    { 
        public  const ushort Id = 5796;
        public override ushort MessageId => Id;

        public byte moduleToUse;

        public PrismUseRequestMessage()
        {
        }
        public PrismUseRequestMessage(byte moduleToUse)
        {
            this.moduleToUse = moduleToUse;
        }
        public override void Serialize(IDataWriter writer)
        {
            writer.WriteByte((byte)moduleToUse);
        }
        public override void Deserialize(IDataReader reader)
        {
            moduleToUse = (byte)reader.ReadByte();
            if (moduleToUse < 0)
            {
                throw new System.Exception("Forbidden value (" + moduleToUse + ") on element of PrismUseRequestMessage.moduleToUse.");
            }

        }


    }
}








