using System.Collections.Generic;
using Giny.Core.Network.Messages;
using Giny.Protocol.Types;
using Giny.Core.IO.Interfaces;
using Giny.Protocol;
using Giny.Protocol.Enums;

namespace Giny.Protocol.Messages
{
    public class GuildChestTabContributionsMessage : NetworkMessage
    {
        public const ushort Id = 7093;
        public override ushort MessageId => Id;

        public Contribution[] contributions;

        public GuildChestTabContributionsMessage()
        {
        }
        public GuildChestTabContributionsMessage(Contribution[] contributions)
        {
            this.contributions = contributions;
        }
        public override void Serialize(IDataWriter writer)
        {
            writer.WriteShort((short)contributions.Length);
            for (uint _i1 = 0; _i1 < contributions.Length; _i1++)
            {
                (contributions[_i1] as Contribution).Serialize(writer);
            }

        }
        public override void Deserialize(IDataReader reader)
        {
            Contribution _item1 = null;
            uint _contributionsLen = (uint)reader.ReadUShort();
            for (uint _i1 = 0; _i1 < _contributionsLen; _i1++)
            {
                _item1 = new Contribution();
                _item1.Deserialize(reader);
                contributions[_i1] = _item1;
            }

        }

    }
}


