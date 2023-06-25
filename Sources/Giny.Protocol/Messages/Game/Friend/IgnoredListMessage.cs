using System.Collections.Generic;
using Giny.Core.Network.Messages;
using Giny.Protocol.Types;
using Giny.Core.IO.Interfaces;
using Giny.Protocol;
using Giny.Protocol.Enums;

namespace Giny.Protocol.Messages
{ 
    public class IgnoredListMessage : NetworkMessage  
    { 
        public  const ushort Id = 802;
        public override ushort MessageId => Id;

        public IgnoredInformations[] ignoredList;

        public IgnoredListMessage()
        {
        }
        public IgnoredListMessage(IgnoredInformations[] ignoredList)
        {
            this.ignoredList = ignoredList;
        }
        public override void Serialize(IDataWriter writer)
        {
            writer.WriteShort((short)ignoredList.Length);
            for (uint _i1 = 0;_i1 < ignoredList.Length;_i1++)
            {
                writer.WriteShort((short)(ignoredList[_i1] as IgnoredInformations).TypeId);
                (ignoredList[_i1] as IgnoredInformations).Serialize(writer);
            }

        }
        public override void Deserialize(IDataReader reader)
        {
            uint _id1 = 0;
            IgnoredInformations _item1 = null;
            uint _ignoredListLen = (uint)reader.ReadUShort();
            for (uint _i1 = 0;_i1 < _ignoredListLen;_i1++)
            {
                _id1 = (uint)reader.ReadUShort();
                _item1 = ProtocolTypeManager.GetInstance<IgnoredInformations>((short)_id1);
                _item1.Deserialize(reader);
                ignoredList[_i1] = _item1;
            }

        }


    }
}








