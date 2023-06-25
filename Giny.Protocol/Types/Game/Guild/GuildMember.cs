using System.Collections.Generic;
using Giny.Core.IO.Interfaces;
using Giny.Protocol;
using Giny.Protocol.Enums;

namespace Giny.Protocol.Types
{ 
    public class GuildMember : CharacterMinimalInformations  
    { 
        public new const ushort Id = 9147;
        public override ushort TypeId => Id;

        public byte breed;
        public bool sex;
        public int rankId;
        public long givenExperience;
        public byte experienceGivenPercent;
        public byte connected;
        public byte alignmentSide;
        public short hoursSinceLastConnection;
        public short moodSmileyId;
        public int accountId;
        public int achievementPoints;
        public PlayerStatus status;
        public bool havenBagShared;
        public PlayerNote note;

        public GuildMember()
        {
        }
        public GuildMember(byte breed,bool sex,int rankId,long givenExperience,byte experienceGivenPercent,byte connected,byte alignmentSide,short hoursSinceLastConnection,short moodSmileyId,int accountId,int achievementPoints,PlayerStatus status,bool havenBagShared,PlayerNote note,long id,string name,short level)
        {
            this.breed = breed;
            this.sex = sex;
            this.rankId = rankId;
            this.givenExperience = givenExperience;
            this.experienceGivenPercent = experienceGivenPercent;
            this.connected = connected;
            this.alignmentSide = alignmentSide;
            this.hoursSinceLastConnection = hoursSinceLastConnection;
            this.moodSmileyId = moodSmileyId;
            this.accountId = accountId;
            this.achievementPoints = achievementPoints;
            this.status = status;
            this.havenBagShared = havenBagShared;
            this.note = note;
            this.id = id;
            this.name = name;
            this.level = level;
        }
        public override void Serialize(IDataWriter writer)
        {
            base.Serialize(writer);
            byte _box0 = 0;
            _box0 = BooleanByteWrapper.SetFlag(_box0,0,sex);
            _box0 = BooleanByteWrapper.SetFlag(_box0,1,havenBagShared);
            writer.WriteByte((byte)_box0);
            writer.WriteByte((byte)breed);
            if (rankId < 0)
            {
                throw new System.Exception("Forbidden value (" + rankId + ") on element rankId.");
            }

            writer.WriteVarInt((int)rankId);
            if (givenExperience < 0 || givenExperience > 9.00719925474099E+15)
            {
                throw new System.Exception("Forbidden value (" + givenExperience + ") on element givenExperience.");
            }

            writer.WriteVarLong((long)givenExperience);
            if (experienceGivenPercent < 0 || experienceGivenPercent > 100)
            {
                throw new System.Exception("Forbidden value (" + experienceGivenPercent + ") on element experienceGivenPercent.");
            }

            writer.WriteByte((byte)experienceGivenPercent);
            writer.WriteByte((byte)connected);
            writer.WriteByte((byte)alignmentSide);
            if (hoursSinceLastConnection < 0 || hoursSinceLastConnection > 65535)
            {
                throw new System.Exception("Forbidden value (" + hoursSinceLastConnection + ") on element hoursSinceLastConnection.");
            }

            writer.WriteShort((short)hoursSinceLastConnection);
            if (moodSmileyId < 0)
            {
                throw new System.Exception("Forbidden value (" + moodSmileyId + ") on element moodSmileyId.");
            }

            writer.WriteVarShort((short)moodSmileyId);
            if (accountId < 0)
            {
                throw new System.Exception("Forbidden value (" + accountId + ") on element accountId.");
            }

            writer.WriteInt((int)accountId);
            writer.WriteInt((int)achievementPoints);
            writer.WriteShort((short)status.TypeId);
            status.Serialize(writer);
            note.Serialize(writer);
        }
        public override void Deserialize(IDataReader reader)
        {
            base.Deserialize(reader);
            byte _box0 = reader.ReadByte();
            sex = BooleanByteWrapper.GetFlag(_box0,0);
            havenBagShared = BooleanByteWrapper.GetFlag(_box0,1);
            breed = (byte)reader.ReadByte();
            rankId = (int)reader.ReadVarUhInt();
            if (rankId < 0)
            {
                throw new System.Exception("Forbidden value (" + rankId + ") on element of GuildMember.rankId.");
            }

            givenExperience = (long)reader.ReadVarUhLong();
            if (givenExperience < 0 || givenExperience > 9.00719925474099E+15)
            {
                throw new System.Exception("Forbidden value (" + givenExperience + ") on element of GuildMember.givenExperience.");
            }

            experienceGivenPercent = (byte)reader.ReadByte();
            if (experienceGivenPercent < 0 || experienceGivenPercent > 100)
            {
                throw new System.Exception("Forbidden value (" + experienceGivenPercent + ") on element of GuildMember.experienceGivenPercent.");
            }

            connected = (byte)reader.ReadByte();
            if (connected < 0)
            {
                throw new System.Exception("Forbidden value (" + connected + ") on element of GuildMember.connected.");
            }

            alignmentSide = (byte)reader.ReadByte();
            hoursSinceLastConnection = (short)reader.ReadUShort();
            if (hoursSinceLastConnection < 0 || hoursSinceLastConnection > 65535)
            {
                throw new System.Exception("Forbidden value (" + hoursSinceLastConnection + ") on element of GuildMember.hoursSinceLastConnection.");
            }

            moodSmileyId = (short)reader.ReadVarUhShort();
            if (moodSmileyId < 0)
            {
                throw new System.Exception("Forbidden value (" + moodSmileyId + ") on element of GuildMember.moodSmileyId.");
            }

            accountId = (int)reader.ReadInt();
            if (accountId < 0)
            {
                throw new System.Exception("Forbidden value (" + accountId + ") on element of GuildMember.accountId.");
            }

            achievementPoints = (int)reader.ReadInt();
            uint _id12 = (uint)reader.ReadUShort();
            status = ProtocolTypeManager.GetInstance<PlayerStatus>((short)_id12);
            status.Deserialize(reader);
            note = new PlayerNote();
            note.Deserialize(reader);
        }


    }
}








