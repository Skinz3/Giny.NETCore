using System.Collections.Generic;
using Giny.Core.Network.Messages;
using Giny.Protocol.Types;
using Giny.Core.IO.Interfaces;
using Giny.Protocol;
using Giny.Protocol.Enums;

namespace Giny.Protocol.Messages
{ 
    public class EvolutiveObjectRecycleResultMessage : NetworkMessage  
    { 
        public  const ushort Id = 8653;
        public override ushort MessageId => Id;

        public RecycledItem[] recycledItems;

        public EvolutiveObjectRecycleResultMessage()
        {
        }
        public EvolutiveObjectRecycleResultMessage(RecycledItem[] recycledItems)
        {
            this.recycledItems = recycledItems;
        }
        public override void Serialize(IDataWriter writer)
        {
            writer.WriteShort((short)recycledItems.Length);
            for (uint _i1 = 0;_i1 < recycledItems.Length;_i1++)
            {
                (recycledItems[_i1] as RecycledItem).Serialize(writer);
            }

        }
        public override void Deserialize(IDataReader reader)
        {
            RecycledItem _item1 = null;
            uint _recycledItemsLen = (uint)reader.ReadUShort();
            for (uint _i1 = 0;_i1 < _recycledItemsLen;_i1++)
            {
                _item1 = new RecycledItem();
                _item1.Deserialize(reader);
                recycledItems[_i1] = _item1;
            }

        }


    }
}








