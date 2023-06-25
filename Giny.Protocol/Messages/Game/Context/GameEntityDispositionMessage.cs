using System.Collections.Generic;
using Giny.Core.Network.Messages;
using Giny.Protocol.Types;
using Giny.Core.IO.Interfaces;
using Giny.Protocol;
using Giny.Protocol.Enums;

namespace Giny.Protocol.Messages
{ 
    public class GameEntityDispositionMessage : NetworkMessage  
    { 
        public  const ushort Id = 64;
        public override ushort MessageId => Id;

        public IdentifiedEntityDispositionInformations disposition;

        public GameEntityDispositionMessage()
        {
        }
        public GameEntityDispositionMessage(IdentifiedEntityDispositionInformations disposition)
        {
            this.disposition = disposition;
        }
        public override void Serialize(IDataWriter writer)
        {
            disposition.Serialize(writer);
        }
        public override void Deserialize(IDataReader reader)
        {
            disposition = new IdentifiedEntityDispositionInformations();
            disposition.Deserialize(reader);
        }


    }
}








