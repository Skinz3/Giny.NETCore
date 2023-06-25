using System.Collections.Generic;
using Giny.Core.Network.Messages;
using Giny.Protocol.Types;
using Giny.Core.IO.Interfaces;
using Giny.Protocol;
using Giny.Protocol.Enums;

namespace Giny.Protocol.Messages
{ 
    public class ListMapNpcsQuestStatusUpdateMessage : NetworkMessage  
    { 
        public  const ushort Id = 9407;
        public override ushort MessageId => Id;

        public MapNpcQuestInfo[] mapInfo;

        public ListMapNpcsQuestStatusUpdateMessage()
        {
        }
        public ListMapNpcsQuestStatusUpdateMessage(MapNpcQuestInfo[] mapInfo)
        {
            this.mapInfo = mapInfo;
        }
        public override void Serialize(IDataWriter writer)
        {
            writer.WriteShort((short)mapInfo.Length);
            for (uint _i1 = 0;_i1 < mapInfo.Length;_i1++)
            {
                (mapInfo[_i1] as MapNpcQuestInfo).Serialize(writer);
            }

        }
        public override void Deserialize(IDataReader reader)
        {
            MapNpcQuestInfo _item1 = null;
            uint _mapInfoLen = (uint)reader.ReadUShort();
            for (uint _i1 = 0;_i1 < _mapInfoLen;_i1++)
            {
                _item1 = new MapNpcQuestInfo();
                _item1.Deserialize(reader);
                mapInfo[_i1] = _item1;
            }

        }


    }
}








