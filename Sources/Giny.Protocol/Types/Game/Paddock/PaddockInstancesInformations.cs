using System.Collections.Generic;
using Giny.Core.IO.Interfaces;
using Giny.Protocol;
using Giny.Protocol.Enums;

namespace Giny.Protocol.Types
{ 
    public class PaddockInstancesInformations : PaddockInformations  
    { 
        public new const ushort Id = 7192;
        public override ushort TypeId => Id;

        public PaddockBuyableInformations[] paddocks;

        public PaddockInstancesInformations()
        {
        }
        public PaddockInstancesInformations(PaddockBuyableInformations[] paddocks,short maxOutdoorMount,short maxItems)
        {
            this.paddocks = paddocks;
            this.maxOutdoorMount = maxOutdoorMount;
            this.maxItems = maxItems;
        }
        public override void Serialize(IDataWriter writer)
        {
            base.Serialize(writer);
            writer.WriteShort((short)paddocks.Length);
            for (uint _i1 = 0;_i1 < paddocks.Length;_i1++)
            {
                writer.WriteShort((short)(paddocks[_i1] as PaddockBuyableInformations).TypeId);
                (paddocks[_i1] as PaddockBuyableInformations).Serialize(writer);
            }

        }
        public override void Deserialize(IDataReader reader)
        {
            uint _id1 = 0;
            PaddockBuyableInformations _item1 = null;
            base.Deserialize(reader);
            uint _paddocksLen = (uint)reader.ReadUShort();
            for (uint _i1 = 0;_i1 < _paddocksLen;_i1++)
            {
                _id1 = (uint)reader.ReadUShort();
                _item1 = ProtocolTypeManager.GetInstance<PaddockBuyableInformations>((short)_id1);
                _item1.Deserialize(reader);
                paddocks[_i1] = _item1;
            }

        }


    }
}








