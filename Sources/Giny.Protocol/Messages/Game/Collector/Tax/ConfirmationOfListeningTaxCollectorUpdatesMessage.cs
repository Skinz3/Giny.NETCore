using System.Collections.Generic;
using Giny.Core.Network.Messages;
using Giny.Protocol.Types;
using Giny.Core.IO.Interfaces;
using Giny.Protocol;
using Giny.Protocol.Enums;

namespace Giny.Protocol.Messages
{
    public class ConfirmationOfListeningTaxCollectorUpdatesMessage : NetworkMessage
    {
        public const ushort Id = 7568;
        public override ushort MessageId => Id;

        public TaxCollectorInformations information;

        public ConfirmationOfListeningTaxCollectorUpdatesMessage()
        {
        }
        public ConfirmationOfListeningTaxCollectorUpdatesMessage(TaxCollectorInformations information)
        {
            this.information = information;
        }
        public override void Serialize(IDataWriter writer)
        {
            information.Serialize(writer);
        }
        public override void Deserialize(IDataReader reader)
        {
            information = new TaxCollectorInformations();
            information.Deserialize(reader);
        }

    }
}


