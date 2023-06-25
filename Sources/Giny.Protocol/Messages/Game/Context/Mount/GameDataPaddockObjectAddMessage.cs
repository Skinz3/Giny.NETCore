using System.Collections.Generic;
using Giny.Core.Network.Messages;
using Giny.Protocol.Types;
using Giny.Core.IO.Interfaces;
using Giny.Protocol;
using Giny.Protocol.Enums;

namespace Giny.Protocol.Messages
{ 
    public class GameDataPaddockObjectAddMessage : NetworkMessage  
    { 
        public  const ushort Id = 2165;
        public override ushort MessageId => Id;

        public PaddockItem paddockItemDescription;

        public GameDataPaddockObjectAddMessage()
        {
        }
        public GameDataPaddockObjectAddMessage(PaddockItem paddockItemDescription)
        {
            this.paddockItemDescription = paddockItemDescription;
        }
        public override void Serialize(IDataWriter writer)
        {
            paddockItemDescription.Serialize(writer);
        }
        public override void Deserialize(IDataReader reader)
        {
            paddockItemDescription = new PaddockItem();
            paddockItemDescription.Deserialize(reader);
        }


    }
}








