using System.Collections.Generic;
using Giny.Core.Network.Messages;
using Giny.Protocol.Types;
using Giny.Core.IO.Interfaces;
using Giny.Protocol;
using Giny.Protocol.Enums;

namespace Giny.Protocol.Messages
{ 
    public class ExchangeMultiCraftSetCrafterCanUseHisRessourcesMessage : NetworkMessage  
    { 
        public  const ushort Id = 9785;
        public override ushort MessageId => Id;

        public bool allow;

        public ExchangeMultiCraftSetCrafterCanUseHisRessourcesMessage()
        {
        }
        public ExchangeMultiCraftSetCrafterCanUseHisRessourcesMessage(bool allow)
        {
            this.allow = allow;
        }
        public override void Serialize(IDataWriter writer)
        {
            writer.WriteBoolean((bool)allow);
        }
        public override void Deserialize(IDataReader reader)
        {
            allow = (bool)reader.ReadBoolean();
        }


    }
}








