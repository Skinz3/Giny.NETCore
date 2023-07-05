using System.Collections.Generic;
using Giny.Core.Network.Messages;
using Giny.Protocol.Types;
using Giny.Core.IO.Interfaces;
using Giny.Protocol;
using Giny.Protocol.Enums;

namespace Giny.Protocol.Messages
{
    public class IdentificationSuccessMessage : NetworkMessage
    {
        public const ushort Id = 3876;
        public override ushort MessageId => Id;

        public string login;
        public AccountTagInformation accountTag;
        public int accountId;
        public byte communityId;
        public bool hasRights;
        public bool hasForceRight;
        public double accountCreation;
        public double subscriptionEndDate;
        public bool wasAlreadyConnected;
        public byte havenbagAvailableRoom;

        public IdentificationSuccessMessage()
        {
        }
        public IdentificationSuccessMessage(string login, AccountTagInformation accountTag, int accountId, byte communityId, bool hasRights, bool hasForceRight, double accountCreation, double subscriptionEndDate, bool wasAlreadyConnected, byte havenbagAvailableRoom)
        {
            this.login = login;
            this.accountTag = accountTag;
            this.accountId = accountId;
            this.communityId = communityId;
            this.hasRights = hasRights;
            this.hasForceRight = hasForceRight;
            this.accountCreation = accountCreation;
            this.subscriptionEndDate = subscriptionEndDate;
            this.wasAlreadyConnected = wasAlreadyConnected;
            this.havenbagAvailableRoom = havenbagAvailableRoom;
        }
        public override void Serialize(IDataWriter writer)
        {
            byte _box0 = 0;
            _box0 = BooleanByteWrapper.SetFlag(_box0, 0, hasRights);
            _box0 = BooleanByteWrapper.SetFlag(_box0, 1, hasForceRight);
            _box0 = BooleanByteWrapper.SetFlag(_box0, 2, wasAlreadyConnected);
            writer.WriteByte((byte)_box0);
            writer.WriteUTF((string)login);
            accountTag.Serialize(writer);
            if (accountId < 0)
            {
                throw new System.Exception("Forbidden value (" + accountId + ") on element accountId.");
            }

            writer.WriteInt((int)accountId);
            if (communityId < 0)
            {
                throw new System.Exception("Forbidden value (" + communityId + ") on element communityId.");
            }

            writer.WriteByte((byte)communityId);
            if (accountCreation < 0 || accountCreation > 9007199254740992)
            {
                throw new System.Exception("Forbidden value (" + accountCreation + ") on element accountCreation.");
            }

            writer.WriteDouble((double)accountCreation);
            if (subscriptionEndDate < 0 || subscriptionEndDate > 9007199254740992)
            {
                throw new System.Exception("Forbidden value (" + subscriptionEndDate + ") on element subscriptionEndDate.");
            }

            writer.WriteDouble((double)subscriptionEndDate);
            if (havenbagAvailableRoom < 0 || havenbagAvailableRoom > 255)
            {
                throw new System.Exception("Forbidden value (" + havenbagAvailableRoom + ") on element havenbagAvailableRoom.");
            }

            writer.WriteByte((byte)havenbagAvailableRoom);
        }
        public override void Deserialize(IDataReader reader)
        {
            byte _box0 = reader.ReadByte();
            hasRights = BooleanByteWrapper.GetFlag(_box0, 0);
            hasForceRight = BooleanByteWrapper.GetFlag(_box0, 1);
            wasAlreadyConnected = BooleanByteWrapper.GetFlag(_box0, 2);
            login = (string)reader.ReadUTF();
            accountTag = new AccountTagInformation();
            accountTag.Deserialize(reader);
            accountId = (int)reader.ReadInt();
            if (accountId < 0)
            {
                throw new System.Exception("Forbidden value (" + accountId + ") on element of IdentificationSuccessMessage.accountId.");
            }

            communityId = (byte)reader.ReadByte();
            if (communityId < 0)
            {
                throw new System.Exception("Forbidden value (" + communityId + ") on element of IdentificationSuccessMessage.communityId.");
            }

            accountCreation = (double)reader.ReadDouble();
            if (accountCreation < 0 || accountCreation > 9007199254740992)
            {
                throw new System.Exception("Forbidden value (" + accountCreation + ") on element of IdentificationSuccessMessage.accountCreation.");
            }

            subscriptionEndDate = (double)reader.ReadDouble();
            if (subscriptionEndDate < 0 || subscriptionEndDate > 9007199254740992)
            {
                throw new System.Exception("Forbidden value (" + subscriptionEndDate + ") on element of IdentificationSuccessMessage.subscriptionEndDate.");
            }

            havenbagAvailableRoom = (byte)reader.ReadSByte();
            if (havenbagAvailableRoom < 0 || havenbagAvailableRoom > 255)
            {
                throw new System.Exception("Forbidden value (" + havenbagAvailableRoom + ") on element of IdentificationSuccessMessage.havenbagAvailableRoom.");
            }

        }

    }
}


