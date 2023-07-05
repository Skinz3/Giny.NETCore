using System.Collections.Generic;
using Giny.Core.Network.Messages;
using Giny.Protocol.Types;
using Giny.Core.IO.Interfaces;
using Giny.Protocol;
using Giny.Protocol.Enums;

namespace Giny.Protocol.Messages
{
    public class NuggetsInformationMessage : NetworkMessage
    {
        public const ushort Id = 7361;
        public override ushort MessageId => Id;

        public int nuggetsQuantity;

        public NuggetsInformationMessage()
        {
        }
        public NuggetsInformationMessage(int nuggetsQuantity)
        {
            this.nuggetsQuantity = nuggetsQuantity;
        }
        public override void Serialize(IDataWriter writer)
        {
            writer.WriteInt((int)nuggetsQuantity);
        }
        public override void Deserialize(IDataReader reader)
        {
            nuggetsQuantity = (int)reader.ReadInt();
        }

    }
}


