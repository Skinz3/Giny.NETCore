using System.Collections.Generic;
using Giny.Core.Network.Messages;
using Giny.Protocol.Types;
using Giny.Core.IO.Interfaces;
using Giny.Protocol;
using Giny.Protocol.Enums;

namespace Giny.Protocol.Messages
{
    public class GuildSelectChestTabRequestMessage : NetworkMessage
    {
        public const ushort Id = 1832;
        public override ushort MessageId => Id;

        public int tabNumber;

        public GuildSelectChestTabRequestMessage()
        {
        }
        public GuildSelectChestTabRequestMessage(int tabNumber)
        {
            this.tabNumber = tabNumber;
        }
        public override void Serialize(IDataWriter writer)
        {
            if (tabNumber < 0)
            {
                throw new System.Exception("Forbidden value (" + tabNumber + ") on element tabNumber.");
            }

            writer.WriteVarInt((int)tabNumber);
        }
        public override void Deserialize(IDataReader reader)
        {
            tabNumber = (int)reader.ReadVarUhInt();
            if (tabNumber < 0)
            {
                throw new System.Exception("Forbidden value (" + tabNumber + ") on element of GuildSelectChestTabRequestMessage.tabNumber.");
            }

        }

    }
}


