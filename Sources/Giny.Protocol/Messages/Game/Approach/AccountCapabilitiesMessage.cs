using System.Collections.Generic;
using Giny.Core.Network.Messages;
using Giny.Protocol.Types;
using Giny.Core.IO.Interfaces;
using Giny.Protocol;
using Giny.Protocol.Enums;

namespace Giny.Protocol.Messages
{
    public class AccountCapabilitiesMessage : NetworkMessage
    {
        public const ushort Id = 1358;
        public override ushort MessageId => Id;

        public int accountId;
        public bool tutorialAvailable;
        public byte status;
        public bool canCreateNewCharacter;

        public AccountCapabilitiesMessage()
        {
        }
        public AccountCapabilitiesMessage(int accountId, bool tutorialAvailable, byte status, bool canCreateNewCharacter)
        {
            this.accountId = accountId;
            this.tutorialAvailable = tutorialAvailable;
            this.status = status;
            this.canCreateNewCharacter = canCreateNewCharacter;
        }
        public override void Serialize(IDataWriter writer)
        {
            byte _box0 = 0;
            _box0 = BooleanByteWrapper.SetFlag(_box0, 0, tutorialAvailable);
            _box0 = BooleanByteWrapper.SetFlag(_box0, 1, canCreateNewCharacter);
            writer.WriteByte((byte)_box0);
            if (accountId < 0)
            {
                throw new System.Exception("Forbidden value (" + accountId + ") on element accountId.");
            }

            writer.WriteInt((int)accountId);
            writer.WriteByte((byte)status);
        }
        public override void Deserialize(IDataReader reader)
        {
            byte _box0 = reader.ReadByte();
            tutorialAvailable = BooleanByteWrapper.GetFlag(_box0, 0);
            canCreateNewCharacter = BooleanByteWrapper.GetFlag(_box0, 1);
            accountId = (int)reader.ReadInt();
            if (accountId < 0)
            {
                throw new System.Exception("Forbidden value (" + accountId + ") on element of AccountCapabilitiesMessage.accountId.");
            }

            status = (byte)reader.ReadByte();
        }

    }
}


