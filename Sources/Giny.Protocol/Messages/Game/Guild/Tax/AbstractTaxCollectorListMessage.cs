using System.Collections.Generic;
using Giny.Core.Network.Messages;
using Giny.Protocol.Types;
using Giny.Core.IO.Interfaces;
using Giny.Protocol;
using Giny.Protocol.Enums;

namespace Giny.Protocol.Messages
{ 
    public class AbstractTaxCollectorListMessage : NetworkMessage  
    { 
        public  const ushort Id = 1669;
        public override ushort MessageId => Id;

        public TaxCollectorInformations[] informations;

        public AbstractTaxCollectorListMessage()
        {
        }
        public AbstractTaxCollectorListMessage(TaxCollectorInformations[] informations)
        {
            this.informations = informations;
        }
        public override void Serialize(IDataWriter writer)
        {
            writer.WriteShort((short)informations.Length);
            for (uint _i1 = 0;_i1 < informations.Length;_i1++)
            {
                writer.WriteShort((short)(informations[_i1] as TaxCollectorInformations).TypeId);
                (informations[_i1] as TaxCollectorInformations).Serialize(writer);
            }

        }
        public override void Deserialize(IDataReader reader)
        {
            uint _id1 = 0;
            TaxCollectorInformations _item1 = null;
            uint _informationsLen = (uint)reader.ReadUShort();
            for (uint _i1 = 0;_i1 < _informationsLen;_i1++)
            {
                _id1 = (uint)reader.ReadUShort();
                _item1 = ProtocolTypeManager.GetInstance<TaxCollectorInformations>((short)_id1);
                _item1.Deserialize(reader);
                informations[_i1] = _item1;
            }

        }


    }
}








