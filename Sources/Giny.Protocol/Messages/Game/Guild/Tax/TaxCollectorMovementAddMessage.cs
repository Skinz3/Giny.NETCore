using System.Collections.Generic;
using Giny.Core.Network.Messages;
using Giny.Protocol.Types;
using Giny.Core.IO.Interfaces;
using Giny.Protocol;
using Giny.Protocol.Enums;

namespace Giny.Protocol.Messages
{ 
    public class TaxCollectorMovementAddMessage : NetworkMessage  
    { 
        public  const ushort Id = 6849;
        public override ushort MessageId => Id;

        public TaxCollectorInformations informations;

        public TaxCollectorMovementAddMessage()
        {
        }
        public TaxCollectorMovementAddMessage(TaxCollectorInformations informations)
        {
            this.informations = informations;
        }
        public override void Serialize(IDataWriter writer)
        {
            writer.WriteShort((short)informations.TypeId);
            informations.Serialize(writer);
        }
        public override void Deserialize(IDataReader reader)
        {
            uint _id1 = (uint)reader.ReadUShort();
            informations = ProtocolTypeManager.GetInstance<TaxCollectorInformations>((short)_id1);
            informations.Deserialize(reader);
        }


    }
}








