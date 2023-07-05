using System.Collections.Generic;
using Giny.Core.Network.Messages;
using Giny.Protocol.Types;
using Giny.Core.IO.Interfaces;
using Giny.Protocol;
using Giny.Protocol.Enums;

namespace Giny.Protocol.Messages
{
    public class AlterationsMessage : NetworkMessage
    {
        public const ushort Id = 3851;
        public override ushort MessageId => Id;

        public AlterationInfo[] alterations;

        public AlterationsMessage()
        {
        }
        public AlterationsMessage(AlterationInfo[] alterations)
        {
            this.alterations = alterations;
        }
        public override void Serialize(IDataWriter writer)
        {
            writer.WriteShort((short)alterations.Length);
            for (uint _i1 = 0; _i1 < alterations.Length; _i1++)
            {
                (alterations[_i1] as AlterationInfo).Serialize(writer);
            }

        }
        public override void Deserialize(IDataReader reader)
        {
            AlterationInfo _item1 = null;
            uint _alterationsLen = (uint)reader.ReadUShort();
            for (uint _i1 = 0; _i1 < _alterationsLen; _i1++)
            {
                _item1 = new AlterationInfo();
                _item1.Deserialize(reader);
                alterations[_i1] = _item1;
            }

        }

    }
}


