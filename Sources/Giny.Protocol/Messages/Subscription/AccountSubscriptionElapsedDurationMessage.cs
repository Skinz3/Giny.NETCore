using System.Collections.Generic;
using Giny.Core.Network.Messages;
using Giny.Protocol.Types;
using Giny.Core.IO.Interfaces;
using Giny.Protocol;
using Giny.Protocol.Enums;

namespace Giny.Protocol.Messages
{
    public class AccountSubscriptionElapsedDurationMessage : NetworkMessage
    {
        public const ushort Id = 667;
        public override ushort MessageId => Id;

        public double subscriptionElapsedDuration;

        public AccountSubscriptionElapsedDurationMessage()
        {
        }
        public AccountSubscriptionElapsedDurationMessage(double subscriptionElapsedDuration)
        {
            this.subscriptionElapsedDuration = subscriptionElapsedDuration;
        }
        public override void Serialize(IDataWriter writer)
        {
            if (subscriptionElapsedDuration < 0 || subscriptionElapsedDuration > 9007199254740992)
            {
                throw new System.Exception("Forbidden value (" + subscriptionElapsedDuration + ") on element subscriptionElapsedDuration.");
            }

            writer.WriteDouble((double)subscriptionElapsedDuration);
        }
        public override void Deserialize(IDataReader reader)
        {
            subscriptionElapsedDuration = (double)reader.ReadDouble();
            if (subscriptionElapsedDuration < 0 || subscriptionElapsedDuration > 9007199254740992)
            {
                throw new System.Exception("Forbidden value (" + subscriptionElapsedDuration + ") on element of AccountSubscriptionElapsedDurationMessage.subscriptionElapsedDuration.");
            }

        }

    }
}


