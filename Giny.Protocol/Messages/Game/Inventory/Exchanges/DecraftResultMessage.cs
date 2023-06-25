using System.Collections.Generic;
using Giny.Core.Network.Messages;
using Giny.Protocol.Types;
using Giny.Core.IO.Interfaces;
using Giny.Protocol;
using Giny.Protocol.Enums;

namespace Giny.Protocol.Messages
{ 
    public class DecraftResultMessage : NetworkMessage  
    { 
        public  const ushort Id = 3524;
        public override ushort MessageId => Id;

        public DecraftedItemStackInfo[] results;

        public DecraftResultMessage()
        {
        }
        public DecraftResultMessage(DecraftedItemStackInfo[] results)
        {
            this.results = results;
        }
        public override void Serialize(IDataWriter writer)
        {
            writer.WriteShort((short)results.Length);
            for (uint _i1 = 0;_i1 < results.Length;_i1++)
            {
                (results[_i1] as DecraftedItemStackInfo).Serialize(writer);
            }

        }
        public override void Deserialize(IDataReader reader)
        {
            DecraftedItemStackInfo _item1 = null;
            uint _resultsLen = (uint)reader.ReadUShort();
            for (uint _i1 = 0;_i1 < _resultsLen;_i1++)
            {
                _item1 = new DecraftedItemStackInfo();
                _item1.Deserialize(reader);
                results[_i1] = _item1;
            }

        }


    }
}








