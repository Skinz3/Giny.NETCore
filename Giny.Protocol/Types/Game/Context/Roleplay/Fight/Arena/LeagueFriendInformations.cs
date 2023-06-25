using System.Collections.Generic;
using Giny.Core.IO.Interfaces;
using Giny.Protocol;
using Giny.Protocol.Enums;

namespace Giny.Protocol.Types
{ 
    public class LeagueFriendInformations : AbstractContactInformations  
    { 
        public new const ushort Id = 8933;
        public override ushort TypeId => Id;

        public long playerId;
        public string playerName;
        public byte breed;
        public bool sex;
        public short level;
        public short leagueId;
        public short totalLeaguePoints;
        public int ladderPosition;

        public LeagueFriendInformations()
        {
        }
        public LeagueFriendInformations(long playerId,string playerName,byte breed,bool sex,short level,short leagueId,short totalLeaguePoints,int ladderPosition,int accountId,AccountTagInformation accountTag)
        {
            this.playerId = playerId;
            this.playerName = playerName;
            this.breed = breed;
            this.sex = sex;
            this.level = level;
            this.leagueId = leagueId;
            this.totalLeaguePoints = totalLeaguePoints;
            this.ladderPosition = ladderPosition;
            this.accountId = accountId;
            this.accountTag = accountTag;
        }
        public override void Serialize(IDataWriter writer)
        {
            base.Serialize(writer);
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

            writer.WriteVarShort((short)level);
            writer.WriteVarShort((short)leagueId);
            writer.WriteVarShort((short)totalLeaguePoints);
            writer.WriteInt((int)ladderPosition);
        }
        public override void Deserialize(IDataReader reader)
        {
            base.Deserialize(reader);
            playerId = (long)reader.ReadVarUhLong();
            if (playerId < 0 || playerId > 9.00719925474099E+15)
            {
                throw new System.Exception("Forbidden value (" + playerId + ") on element of LeagueFriendInformations.playerId.");
            }

            playerName = (string)reader.ReadUTF();
            breed = (byte)reader.ReadByte();
            if (breed < (byte)PlayableBreedEnum.Feca || breed > (byte)PlayableBreedEnum.Ouginak)
            {
                throw new System.Exception("Forbidden value (" + breed + ") on element of LeagueFriendInformations.breed.");
            }

            sex = (bool)reader.ReadBoolean();
            level = (short)reader.ReadVarUhShort();
            if (level < 0)
            {
                throw new System.Exception("Forbidden value (" + level + ") on element of LeagueFriendInformations.level.");
            }

            leagueId = (short)reader.ReadVarShort();
            totalLeaguePoints = (short)reader.ReadVarShort();
            ladderPosition = (int)reader.ReadInt();
        }


    }
}








