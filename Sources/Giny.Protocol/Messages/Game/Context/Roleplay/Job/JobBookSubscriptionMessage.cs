using System.Collections.Generic;
using Giny.Core.Network.Messages;
using Giny.Protocol.Types;
using Giny.Core.IO.Interfaces;
using Giny.Protocol;
using Giny.Protocol.Enums;

namespace Giny.Protocol.Messages
{ 
    public class JobBookSubscriptionMessage : NetworkMessage  
    { 
        public  const ushort Id = 1188;
        public override ushort MessageId => Id;

        public JobBookSubscription[] subscriptions;

        public JobBookSubscriptionMessage()
        {
        }
        public JobBookSubscriptionMessage(JobBookSubscription[] subscriptions)
        {
            this.subscriptions = subscriptions;
        }
        public override void Serialize(IDataWriter writer)
        {
            writer.WriteShort((short)subscriptions.Length);
            for (uint _i1 = 0;_i1 < subscriptions.Length;_i1++)
            {
                (subscriptions[_i1] as JobBookSubscription).Serialize(writer);
            }

        }
        public override void Deserialize(IDataReader reader)
        {
            JobBookSubscription _item1 = null;
            uint _subscriptionsLen = (uint)reader.ReadUShort();
            for (uint _i1 = 0;_i1 < _subscriptionsLen;_i1++)
            {
                _item1 = new JobBookSubscription();
                _item1.Deserialize(reader);
                subscriptions[_i1] = _item1;
            }

        }


    }
}








