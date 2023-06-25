using System.Collections.Generic;
using Giny.Core.IO.Interfaces;
using Giny.Protocol;
using Giny.Protocol.Enums;

namespace Giny.Protocol.Types
{ 
    public class FriendOnlineInformations : FriendInformations  
    { 
        public new const ushort Id = 3423;
        public override ushort TypeId => Id;

        public long playerId;
        public string playerName;
        public short level;
        public byte alignmentSide;
        public byte breed;
        public bool sex;
        public GuildInformations guildInfo;
        public short moodSmileyId;
        public PlayerStatus status;
        public bool havenBagShared;

        public FriendOnlineInformations()
        {
        }
        public FriendOnlineInformations(long playerId,string playerName,short level,byte alignmentSide,byte breed,bool sex,GuildInformations guildInfo,short moodSmileyId,PlayerStatus status,bool havenBagShared,int accountId,AccountTagInformation accountTag,byte playerState,short lastConnection,int achievementPoints,short leagueId,int ladderPosition)
        {
            this.playerId = playerId;
            this.playerName = playerName;
            this.level = level;
            this.alignmentSide = alignmentSide;
            this.breed = breed;
            this.sex = sex;
            this.guildInfo = guildInfo;
            this.moodSmileyId = moodSmileyId;
            this.status = status;
            this.havenBagShared = havenBagShared;
            this.accountId = accountId;
            this.accountTag = accountTag;
            this.playerState = playerState;
            this.lastConnection = lastConnection;
            this.achievementPoints = achievementPoints;
            this.leagueId = leagueId;
            this.ladderPosition = ladderPosition;
        }
        public override void Serialize(IDataWriter writer)
        {
            base.Serialize(writer);
            byte _box0 = 0;
            _box0 = BooleanByteWrapper.SetFlag(_box0,0,sex);
            _box0 = BooleanByteWrapper.SetFlag(_box0,1,havenBagShared);
            writer.WriteByte((byte)_box0);
            if (playerId < 0 || playerId > 9.00719925474099E+15)
            {
                throw new System.Exception("Forbidden value (" + playerId + ") on element playerId.");
            }

            writer.WriteVarLong((long)playerId);
            writer.WriteUTF((string)playerName);
            if (level < 0)
            {
                throw new System.Exception("Forbidden value (" + level + ") on element level.");
            }

            writer.WriteVarShort((short)level);
            writer.WriteByte((byte)alignmentSide);
            writer.WriteByte((byte)breed);
            guildInfo.Serialize(writer);
            if (moodSmileyId < 0)
            {
                throw new System.Exception("Forbidden value (" + moodSmileyId + ") on element moodSmileyId.");
            }

            writer.WriteVarShort((short)moodSmileyId);
            writer.WriteShort((short)status.TypeId);
            status.Serialize(writer);
        }
        public override void Deserialize(IDataReader reader)
        {
            base.Deserialize(reader);
            byte _box0 = reader.ReadByte();
            sex = BooleanByteWrapper.GetFlag(_box0,0);
            havenBagShared = BooleanByteWrapper.GetFlag(_box0,1);
            playerId = (long)reader.ReadVarUhLong();
            if (playerId < 0 || playerId > 9.00719925474099E+15)
            {
                throw new System.Exception("Forbidden value (" + playerId + ") on element of FriendOnlineInformations.playerId.");
            }

            playerName = (string)reader.ReadUTF();
            level = (short)reader.ReadVarUhShort();
            if (level < 0)
            {
                throw new System.Exception("Forbidden value (" + level + ") on element of FriendOnlineInformations.level.");
            }

            alignmentSide = (byte)reader.ReadByte();
            breed = (byte)reader.ReadByte();
            if (breed < (byte)PlayableBreedEnum.Feca || breed > (byte)PlayableBreedEnum.Ouginak)
            {
                throw new System.Exception("Forbidden value (" + breed + ") on element of FriendOnlineInformations.breed.");
            }

            guildInfo = new GuildInformations();
            guildInfo.Deserialize(reader);
            moodSmileyId = (short)reader.ReadVarUhShort();
            if (moodSmileyId < 0)
            {
                throw new System.Exception("Forbidden value (" + moodSmileyId + ") on element of FriendOnlineInformations.moodSmileyId.");
            }

            uint _id9 = (uint)reader.ReadUShort();
            status = ProtocolTypeManager.GetInstance<PlayerStatus>((short)_id9);
            status.Deserialize(reader);
        }


    }
}








