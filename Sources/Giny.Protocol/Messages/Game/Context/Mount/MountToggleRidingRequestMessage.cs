using System.Collections.Generic;
using Giny.Core.Network.Messages;
using Giny.Protocol.Types;
using Giny.Core.IO.Interfaces;
using Giny.Protocol;
using Giny.Protocol.Enums;

namespace Giny.Protocol.Messages
{
    public class MountToggleRidingRequestMessage : NetworkMessage
    {
        public const ushort Id = 5154;
        public override ushort MessageId => Id;


        public MountToggleRidingRequestMessage()
        {
        }
        public override void Serialize(IDataWriter writer)
        {
        }
        public override void Deserialize(IDataReader reader)
        {
        }

    }
}


