using System.Collections.Generic;
using Giny.Core.IO.Interfaces;
using Giny.Protocol;
using Giny.Protocol.Enums;

namespace Giny.Protocol.Types
{ 
    public class ApplicationPlayerInformation  
    { 
        public const ushort Id = 6686;
        public virtual ushort TypeId => Id;

        public long playerId;
        public string playerName;
        public byte breed;
        public bool sex;
        public int level;
        public int accountId;
        public string accountTag;
        public string accountNickname;
        public PlayerStatus status;

        public ApplicationPlayerInformation()
        {
        }
        public ApplicationPlayerInformation(long playerId,string playerName,byte breed,bool sex,int level,int accountId,string accountTag,string accountNickname,PlayerStatus status)
        {
            this.playerId = playerId;
            this.playerName = playerName;
            this.breed = breed;
            this.sex = sex;
            this.level = level;
            this.accountId = accountId;
            this.accountTag = accountTag;
            this.accountNickname = accountNickname;
            this.status = status;
        }
        public virtual void Serialize(IDataWriter writer)
        {
            if (playerId < 0 || playerId > 9.00719925474099E+15)
            {
                throw new System.Exception("Forbidden value (" + playerId + ") on element playerId.");
            }

            writer.WriteVarLong((long)playerId);
            writer.WriteUTF((string)playerName);
            writer.WriteByte((byte)breed);
            writer.WriteBoolean((bool)sex);
            if (level < 0)
            {
                throw new System.Exception("Forbidden value (" + level + ") on element level.");
            }

            writer.WriteVarInt((int)level);
            if (accountId < 0)
            {
                throw new System.Exception("Forbidden value (" + accountId + ") on element accountId.");
            }

            writer.WriteVarInt((int)accountId);
            writer.WriteUTF((string)accountTag);
            writer.WriteUTF((string)accountNickname);
            status.Serialize(writer);
        }
        public virtual void Deserialize(IDataReader reader)
        {
            playerId = (long)reader.ReadVarUhLong();
            if (playerId < 0 || playerId > 9.00719925474099E+15)
            {
                throw new System.Exception("Forbidden value (" + playerId + ") on element of ApplicationPlayerInformation.playerId.");
            }

            playerName = (string)reader.ReadUTF();
            breed = (byte)reader.ReadByte();
            if (breed < (byte)PlayableBreedEnum.Feca || breed > (byte)PlayableBreedEnum.Ouginak)
            {
                throw new System.Exception("Forbidden value (" + breed + ") on element of ApplicationPlayerInformation.breed.");
            }

            sex = (bool)reader.ReadBoolean();
            level = (int)reader.ReadVarUhInt();
            if (level < 0)
            {
                throw new System.Exception("Forbidden value (" + level + ") on element of ApplicationPlayerInformation.level.");
            }

            accountId = (int)reader.ReadVarUhInt();
            if (accountId < 0)
            {
                throw new System.Exception("Forbidden value (" + accountId + ") on element of ApplicationPlayerInformation.accountId.");
            }

            accountTag = (string)reader.ReadUTF();
            accountNickname = (string)reader.ReadUTF();
            status = new PlayerStatus();
            status.Deserialize(reader);
        }


    }
}








