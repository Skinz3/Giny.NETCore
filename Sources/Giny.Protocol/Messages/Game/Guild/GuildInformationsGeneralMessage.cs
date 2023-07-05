using System.Collections.Generic;
using Giny.Core.Network.Messages;
using Giny.Protocol.Types;
using Giny.Core.IO.Interfaces;
using Giny.Protocol;
using Giny.Protocol.Enums;

namespace Giny.Protocol.Messages
{
    public class GuildInformationsGeneralMessage : NetworkMessage
    {
        public const ushort Id = 4169;
        public override ushort MessageId => Id;

        public bool abandonnedPaddock;
        public byte level;
        public long expLevelFloor;
        public long experience;
        public long expNextLevelFloor;
        public int creationDate;

        public GuildInformationsGeneralMessage()
        {
        }
        public GuildInformationsGeneralMessage(bool abandonnedPaddock, byte level, long expLevelFloor, long experience, long expNextLevelFloor, int creationDate)
        {
            this.abandonnedPaddock = abandonnedPaddock;
            this.level = level;
            this.expLevelFloor = expLevelFloor;
            this.experience = experience;
            this.expNextLevelFloor = expNextLevelFloor;
            this.creationDate = creationDate;
        }
        public override void Serialize(IDataWriter writer)
        {
            writer.WriteBoolean((bool)abandonnedPaddock);
            if (level < 0 || level > 255)
            {
                throw new System.Exception("Forbidden value (" + level + ") on element level.");
            }

            writer.WriteByte((byte)level);
            if (expLevelFloor < 0 || expLevelFloor > 9007199254740992)
            {
                throw new System.Exception("Forbidden value (" + expLevelFloor + ") on element expLevelFloor.");
            }

            writer.WriteVarLong((long)expLevelFloor);
            if (experience < 0 || experience > 9007199254740992)
            {
                throw new System.Exception("Forbidden value (" + experience + ") on element experience.");
            }

            writer.WriteVarLong((long)experience);
            if (expNextLevelFloor < 0 || expNextLevelFloor > 9007199254740992)
            {
                throw new System.Exception("Forbidden value (" + expNextLevelFloor + ") on element expNextLevelFloor.");
            }

            writer.WriteVarLong((long)expNextLevelFloor);
            if (creationDate < 0)
            {
                throw new System.Exception("Forbidden value (" + creationDate + ") on element creationDate.");
            }

            writer.WriteInt((int)creationDate);
        }
        public override void Deserialize(IDataReader reader)
        {
            abandonnedPaddock = (bool)reader.ReadBoolean();
            level = (byte)reader.ReadSByte();
            if (level < 0 || level > 255)
            {
                throw new System.Exception("Forbidden value (" + level + ") on element of GuildInformationsGeneralMessage.level.");
            }

            expLevelFloor = (long)reader.ReadVarUhLong();
            if (expLevelFloor < 0 || expLevelFloor > 9007199254740992)
            {
                throw new System.Exception("Forbidden value (" + expLevelFloor + ") on element of GuildInformationsGeneralMessage.expLevelFloor.");
            }

            experience = (long)reader.ReadVarUhLong();
            if (experience < 0 || experience > 9007199254740992)
            {
                throw new System.Exception("Forbidden value (" + experience + ") on element of GuildInformationsGeneralMessage.experience.");
            }

            expNextLevelFloor = (long)reader.ReadVarUhLong();
            if (expNextLevelFloor < 0 || expNextLevelFloor > 9007199254740992)
            {
                throw new System.Exception("Forbidden value (" + expNextLevelFloor + ") on element of GuildInformationsGeneralMessage.expNextLevelFloor.");
            }

            creationDate = (int)reader.ReadInt();
            if (creationDate < 0)
            {
                throw new System.Exception("Forbidden value (" + creationDate + ") on element of GuildInformationsGeneralMessage.creationDate.");
            }

        }

    }
}


