using System.Collections.Generic;
using Giny.Core.Network.Messages;
using Giny.Protocol.Types;
using Giny.Core.IO.Interfaces;
using Giny.Protocol;
using Giny.Protocol.Enums;

namespace Giny.Protocol.Messages
{ 
    public class AtlasPointInformationsMessage : NetworkMessage  
    { 
        public  const ushort Id = 5051;
        public override ushort MessageId => Id;

        public AtlasPointsInformations type;

        public AtlasPointInformationsMessage()
        {
        }
        public AtlasPointInformationsMessage(AtlasPointsInformations type)
        {
            this.type = type;
        }
        public override void Serialize(IDataWriter writer)
        {
            type.Serialize(writer);
        }
        public override void Deserialize(IDataReader reader)
        {
            type = new AtlasPointsInformations();
            type.Deserialize(reader);
        }


    }
}








