using System.Collections.Generic;
using Giny.Core.Network.Messages;
using Giny.Protocol.Types;
using Giny.Core.IO.Interfaces;
using Giny.Protocol;
using Giny.Protocol.Enums;

namespace Giny.Protocol.Messages
{
    public class GuildChestTabContributionMessage : NetworkMessage
    {
        public const ushort Id = 7525;
        public override ushort MessageId => Id;

        public int tabNumber;
        public long requiredAmount;
        public long currentAmount;
        public double chestContributionEnrollmentDelay;
        public double chestContributionDelay;

        public GuildChestTabContributionMessage()
        {
        }
        public GuildChestTabContributionMessage(int tabNumber, long requiredAmount, long currentAmount, double chestContributionEnrollmentDelay, double chestContributionDelay)
        {
            this.tabNumber = tabNumber;
            this.requiredAmount = requiredAmount;
            this.currentAmount = currentAmount;
            this.chestContributionEnrollmentDelay = chestContributionEnrollmentDelay;
            this.chestContributionDelay = chestContributionDelay;
        }
        public override void Serialize(IDataWriter writer)
        {
            if (tabNumber < 0)
            {
                throw new System.Exception("Forbidden value (" + tabNumber + ") on element tabNumber.");
            }

            writer.WriteVarInt((int)tabNumber);
            if (requiredAmount < 0 || requiredAmount > 9007199254740992)
            {
                throw new System.Exception("Forbidden value (" + requiredAmount + ") on element requiredAmount.");
            }

            writer.WriteVarLong((long)requiredAmount);
            if (currentAmount < 0 || currentAmount > 9007199254740992)
            {
                throw new System.Exception("Forbidden value (" + currentAmount + ") on element currentAmount.");
            }

            writer.WriteVarLong((long)currentAmount);
            if (chestContributionEnrollmentDelay < 0 || chestContributionEnrollmentDelay > 9007199254740992)
            {
                throw new System.Exception("Forbidden value (" + chestContributionEnrollmentDelay + ") on element chestContributionEnrollmentDelay.");
            }

            writer.WriteDouble((double)chestContributionEnrollmentDelay);
            if (chestContributionDelay < 0 || chestContributionDelay > 9007199254740992)
            {
                throw new System.Exception("Forbidden value (" + chestContributionDelay + ") on element chestContributionDelay.");
            }

            writer.WriteDouble((double)chestContributionDelay);
        }
        public override void Deserialize(IDataReader reader)
        {
            tabNumber = (int)reader.ReadVarUhInt();
            if (tabNumber < 0)
            {
                throw new System.Exception("Forbidden value (" + tabNumber + ") on element of GuildChestTabContributionMessage.tabNumber.");
            }

            requiredAmount = (long)reader.ReadVarUhLong();
            if (requiredAmount < 0 || requiredAmount > 9007199254740992)
            {
                throw new System.Exception("Forbidden value (" + requiredAmount + ") on element of GuildChestTabContributionMessage.requiredAmount.");
            }

            currentAmount = (long)reader.ReadVarUhLong();
            if (currentAmount < 0 || currentAmount > 9007199254740992)
            {
                throw new System.Exception("Forbidden value (" + currentAmount + ") on element of GuildChestTabContributionMessage.currentAmount.");
            }

            chestContributionEnrollmentDelay = (double)reader.ReadDouble();
            if (chestContributionEnrollmentDelay < 0 || chestContributionEnrollmentDelay > 9007199254740992)
            {
                throw new System.Exception("Forbidden value (" + chestContributionEnrollmentDelay + ") on element of GuildChestTabContributionMessage.chestContributionEnrollmentDelay.");
            }

            chestContributionDelay = (double)reader.ReadDouble();
            if (chestContributionDelay < 0 || chestContributionDelay > 9007199254740992)
            {
                throw new System.Exception("Forbidden value (" + chestContributionDelay + ") on element of GuildChestTabContributionMessage.chestContributionDelay.");
            }

        }

    }
}


