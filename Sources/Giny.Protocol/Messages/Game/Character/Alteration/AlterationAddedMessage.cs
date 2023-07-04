using System.Collections.Generic;
using Giny.Core.Network.Messages;
using Giny.Protocol.Types;
using Giny.Core.IO.Interfaces;
using Giny.Protocol;
using Giny.Protocol.Enums;

namespace Giny.Protocol.Messages
{
    public class AlterationAddedMessage : NetworkMessage
    {
        public const ushort Id = 6493;
        public override ushort MessageId => Id;

        public AlterationInfo alteration;

        public AlterationAddedMessage()
        {
        }
        public AlterationAddedMessage(AlterationInfo alteration)
        {
            this.alteration = alteration;
        }
        public override void Serialize(IDataWriter writer)
        {
            alteration.Serialize(writer);
        }
        public override void Deserialize(IDataReader reader)
        {
            alteration = new AlterationInfo();
            alteration.Deserialize(reader);
        }

    }
}


