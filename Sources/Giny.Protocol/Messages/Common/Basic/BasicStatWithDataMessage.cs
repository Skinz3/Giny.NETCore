using System.Collections.Generic;
using Giny.Core.Network.Messages;
using Giny.Protocol.Types;
using Giny.Core.IO.Interfaces;
using Giny.Protocol;
using Giny.Protocol.Enums;

namespace Giny.Protocol.Messages
{ 
    public class BasicStatWithDataMessage : BasicStatMessage  
    { 
        public new const ushort Id = 5377;
        public override ushort MessageId => Id;

        public StatisticData[] datas;

        public BasicStatWithDataMessage()
        {
        }
        public BasicStatWithDataMessage(StatisticData[] datas,double timeSpent,short statId)
        {
            this.datas = datas;
            this.timeSpent = timeSpent;
            this.statId = statId;
        }
        public override void Serialize(IDataWriter writer)
        {
            base.Serialize(writer);
            writer.WriteShort((short)datas.Length);
            for (uint _i1 = 0;_i1 < datas.Length;_i1++)
            {
                writer.WriteShort((short)(datas[_i1] as StatisticData).TypeId);
                (datas[_i1] as StatisticData).Serialize(writer);
            }

        }
        public override void Deserialize(IDataReader reader)
        {
            uint _id1 = 0;
            StatisticData _item1 = null;
            base.Deserialize(reader);
            uint _datasLen = (uint)reader.ReadUShort();
            for (uint _i1 = 0;_i1 < _datasLen;_i1++)
            {
                _id1 = (uint)reader.ReadUShort();
                _item1 = ProtocolTypeManager.GetInstance<StatisticData>((short)_id1);
                _item1.Deserialize(reader);
                datas[_i1] = _item1;
            }

        }


    }
}








