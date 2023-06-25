using System.Collections.Generic;
using Giny.Core.Network.Messages;
using Giny.Protocol.Types;
using Giny.Core.IO.Interfaces;
using Giny.Protocol;
using Giny.Protocol.Enums;

namespace Giny.Protocol.Messages
{ 
    public class MimicryObjectPreviewMessage : NetworkMessage  
    { 
        public  const ushort Id = 6331;
        public override ushort MessageId => Id;

        public ObjectItem result;

        public MimicryObjectPreviewMessage()
        {
        }
        public MimicryObjectPreviewMessage(ObjectItem result)
        {
            this.result = result;
        }
        public override void Serialize(IDataWriter writer)
        {
            result.Serialize(writer);
        }
        public override void Deserialize(IDataReader reader)
        {
            result = new ObjectItem();
            result.Deserialize(reader);
        }


    }
}








