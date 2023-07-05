using System.Collections.Generic;
using Giny.Core.Network.Messages;
using Giny.Protocol.Types;
using Giny.Core.IO.Interfaces;
using Giny.Protocol;
using Giny.Protocol.Enums;

namespace Giny.Protocol.Messages
{
    public class AllianceListApplicationRequestMessage : PaginationRequestAbstractMessage
    {
        public new const ushort Id = 2579;
        public override ushort MessageId => Id;


        public AllianceListApplicationRequestMessage()
        {
        }
        public AllianceListApplicationRequestMessage(double offset, uint count)
        {
            this.offset = offset;
            this.count = count;
        }
        public override void Serialize(IDataWriter writer)
        {
            base.Serialize(writer);
        }
        public override void Deserialize(IDataReader reader)
        {
            base.Deserialize(reader);
        }

    }
}


