using System.Collections.Generic;
using Giny.Core.Network.Messages;
using Giny.Protocol.Types;
using Giny.Core.IO.Interfaces;
using Giny.Protocol;
using Giny.Protocol.Enums;

namespace Giny.Protocol.Messages
{
    public class GuildChestTabLastContributionMessage : NetworkMessage
    {
        public const ushort Id = 9676;
        public override ushort MessageId => Id;

        public double lastContributionDate;

        public GuildChestTabLastContributionMessage()
        {
        }
        public GuildChestTabLastContributionMessage(double lastContributionDate)
        {
            this.lastContributionDate = lastContributionDate;
        }
        public override void Serialize(IDataWriter writer)
        {
            if (lastContributionDate < 0 || lastContributionDate > 9007199254740992)
            {
                throw new System.Exception("Forbidden value (" + lastContributionDate + ") on element lastContributionDate.");
            }

            writer.WriteDouble((double)lastContributionDate);
        }
        public override void Deserialize(IDataReader reader)
        {
            lastContributionDate = (double)reader.ReadDouble();
            if (lastContributionDate < 0 || lastContributionDate > 9007199254740992)
            {
                throw new System.Exception("Forbidden value (" + lastContributionDate + ") on element of GuildChestTabLastContributionMessage.lastContributionDate.");
            }

        }

    }
}


