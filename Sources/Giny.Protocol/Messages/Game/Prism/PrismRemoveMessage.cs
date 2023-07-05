using System.Collections.Generic;
using Giny.Core.Network.Messages;
using Giny.Protocol.Types;
using Giny.Core.IO.Interfaces;
using Giny.Protocol;
using Giny.Protocol.Enums;

namespace Giny.Protocol.Messages
{
    public class PrismRemoveMessage : NetworkMessage
    {
        public const ushort Id = 1921;
        public override ushort MessageId => Id;

        public PrismGeolocalizedInformation prism;

        public PrismRemoveMessage()
        {
        }
        public PrismRemoveMessage(PrismGeolocalizedInformation prism)
        {
            this.prism = prism;
        }
        public override void Serialize(IDataWriter writer)
        {
            prism.Serialize(writer);
        }
        public override void Deserialize(IDataReader reader)
        {
            prism = new PrismGeolocalizedInformation();
            prism.Deserialize(reader);
        }

    }
}


