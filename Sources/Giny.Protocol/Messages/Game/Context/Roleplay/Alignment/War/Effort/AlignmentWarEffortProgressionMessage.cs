using System.Collections.Generic;
using Giny.Core.Network.Messages;
using Giny.Protocol.Types;
using Giny.Core.IO.Interfaces;
using Giny.Protocol;
using Giny.Protocol.Enums;

namespace Giny.Protocol.Messages
{ 
    public class AlignmentWarEffortProgressionMessage : NetworkMessage  
    { 
        public  const ushort Id = 4206;
        public override ushort MessageId => Id;

        public AlignmentWarEffortInformation[] effortProgressions;

        public AlignmentWarEffortProgressionMessage()
        {
        }
        public AlignmentWarEffortProgressionMessage(AlignmentWarEffortInformation[] effortProgressions)
        {
            this.effortProgressions = effortProgressions;
        }
        public override void Serialize(IDataWriter writer)
        {
            writer.WriteShort((short)effortProgressions.Length);
            for (uint _i1 = 0;_i1 < effortProgressions.Length;_i1++)
            {
                (effortProgressions[_i1] as AlignmentWarEffortInformation).Serialize(writer);
            }

        }
        public override void Deserialize(IDataReader reader)
        {
            AlignmentWarEffortInformation _item1 = null;
            uint _effortProgressionsLen = (uint)reader.ReadUShort();
            for (uint _i1 = 0;_i1 < _effortProgressionsLen;_i1++)
            {
                _item1 = new AlignmentWarEffortInformation();
                _item1.Deserialize(reader);
                effortProgressions[_i1] = _item1;
            }

        }


    }
}








