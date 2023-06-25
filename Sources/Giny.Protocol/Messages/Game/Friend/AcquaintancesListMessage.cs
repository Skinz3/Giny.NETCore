using System.Collections.Generic;
using Giny.Core.Network.Messages;
using Giny.Protocol.Types;
using Giny.Core.IO.Interfaces;
using Giny.Protocol;
using Giny.Protocol.Enums;

namespace Giny.Protocol.Messages
{ 
    public class AcquaintancesListMessage : NetworkMessage  
    { 
        public  const ushort Id = 6963;
        public override ushort MessageId => Id;

        public AcquaintanceInformation[] acquaintanceList;

        public AcquaintancesListMessage()
        {
        }
        public AcquaintancesListMessage(AcquaintanceInformation[] acquaintanceList)
        {
            this.acquaintanceList = acquaintanceList;
        }
        public override void Serialize(IDataWriter writer)
        {
            writer.WriteShort((short)acquaintanceList.Length);
            for (uint _i1 = 0;_i1 < acquaintanceList.Length;_i1++)
            {
                writer.WriteShort((short)(acquaintanceList[_i1] as AcquaintanceInformation).TypeId);
                (acquaintanceList[_i1] as AcquaintanceInformation).Serialize(writer);
            }

        }
        public override void Deserialize(IDataReader reader)
        {
            uint _id1 = 0;
            AcquaintanceInformation _item1 = null;
            uint _acquaintanceListLen = (uint)reader.ReadUShort();
            for (uint _i1 = 0;_i1 < _acquaintanceListLen;_i1++)
            {
                _id1 = (uint)reader.ReadUShort();
                _item1 = ProtocolTypeManager.GetInstance<AcquaintanceInformation>((short)_id1);
                _item1.Deserialize(reader);
                acquaintanceList[_i1] = _item1;
            }

        }


    }
}








