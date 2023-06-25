using System.Collections.Generic;
using Giny.Core.IO.Interfaces;
using Giny.Protocol;
using Giny.Protocol.Enums;

namespace Giny.Protocol.Types
{ 
    public class FightResultExperienceData : FightResultAdditionalData  
    { 
        public new const ushort Id = 2223;
        public override ushort TypeId => Id;

        public long experience;
        public bool showExperience;
        public long experienceLevelFloor;
        public bool showExperienceLevelFloor;
        public long experienceNextLevelFloor;
        public bool showExperienceNextLevelFloor;
        public long experienceFightDelta;
        public bool showExperienceFightDelta;
        public long experienceForGuild;
        public bool showExperienceForGuild;
        public long experienceForMount;
        public bool showExperienceForMount;
        public bool isIncarnationExperience;
        public byte rerollExperienceMul;

        public FightResultExperienceData()
        {
        }
        public FightResultExperienceData(long experience,bool showExperience,long experienceLevelFloor,bool showExperienceLevelFloor,long experienceNextLevelFloor,bool showExperienceNextLevelFloor,long experienceFightDelta,bool showExperienceFightDelta,long experienceForGuild,bool showExperienceForGuild,long experienceForMount,bool showExperienceForMount,bool isIncarnationExperience,byte rerollExperienceMul)
        {
            this.experience = experience;
            this.showExperience = showExperience;
            this.experienceLevelFloor = experienceLevelFloor;
            this.showExperienceLevelFloor = showExperienceLevelFloor;
            this.experienceNextLevelFloor = experienceNextLevelFloor;
            this.showExperienceNextLevelFloor = showExperienceNextLevelFloor;
            this.experienceFightDelta = experienceFightDelta;
            this.showExperienceFightDelta = showExperienceFightDelta;
            this.experienceForGuild = experienceForGuild;
            this.showExperienceForGuild = showExperienceForGuild;
            this.experienceForMount = experienceForMount;
            this.showExperienceForMount = showExperienceForMount;
            this.isIncarnationExperience = isIncarnationExperience;
            this.rerollExperienceMul = rerollExperienceMul;
        }
        public override void Serialize(IDataWriter writer)
        {
            base.Serialize(writer);
            byte _box0 = 0;
            _box0 = BooleanByteWrapper.SetFlag(_box0,0,showExperience);
            _box0 = BooleanByteWrapper.SetFlag(_box0,1,showExperienceLevelFloor);
            _box0 = BooleanByteWrapper.SetFlag(_box0,2,showExperienceNextLevelFloor);
            _box0 = BooleanByteWrapper.SetFlag(_box0,3,showExperienceFightDelta);
            _box0 = BooleanByteWrapper.SetFlag(_box0,4,showExperienceForGuild);
            _box0 = BooleanByteWrapper.SetFlag(_box0,5,showExperienceForMount);
            _box0 = BooleanByteWrapper.SetFlag(_box0,6,isIncarnationExperience);
            writer.WriteByte((byte)_box0);
            if (experience < 0 || experience > 9.00719925474099E+15)
            {
                throw new System.Exception("Forbidden value (" + experience + ") on element experience.");
            }

            writer.WriteVarLong((long)experience);
            if (experienceLevelFloor < 0 || experienceLevelFloor > 9.00719925474099E+15)
            {
                throw new System.Exception("Forbidden value (" + experienceLevelFloor + ") on element experienceLevelFloor.");
            }

            writer.WriteVarLong((long)experienceLevelFloor);
            if (experienceNextLevelFloor < 0 || experienceNextLevelFloor > 9.00719925474099E+15)
            {
                throw new System.Exception("Forbidden value (" + experienceNextLevelFloor + ") on element experienceNextLevelFloor.");
            }

            writer.WriteVarLong((long)experienceNextLevelFloor);
            if (experienceFightDelta < 0 || experienceFightDelta > 9.00719925474099E+15)
            {
                throw new System.Exception("Forbidden value (" + experienceFightDelta + ") on element experienceFightDelta.");
            }

            writer.WriteVarLong((long)experienceFightDelta);
            if (experienceForGuild < 0 || experienceForGuild > 9.00719925474099E+15)
            {
                throw new System.Exception("Forbidden value (" + experienceForGuild + ") on element experienceForGuild.");
            }

            writer.WriteVarLong((long)experienceForGuild);
            if (experienceForMount < 0 || experienceForMount > 9.00719925474099E+15)
            {
                throw new System.Exception("Forbidden value (" + experienceForMount + ") on element experienceForMount.");
            }

            writer.WriteVarLong((long)experienceForMount);
            if (rerollExperienceMul < 0)
            {
                throw new System.Exception("Forbidden value (" + rerollExperienceMul + ") on element rerollExperienceMul.");
            }

            writer.WriteByte((byte)rerollExperienceMul);
        }
        public override void Deserialize(IDataReader reader)
        {
            base.Deserialize(reader);
            byte _box0 = reader.ReadByte();
            showExperience = BooleanByteWrapper.GetFlag(_box0,0);
            showExperienceLevelFloor = BooleanByteWrapper.GetFlag(_box0,1);
            showExperienceNextLevelFloor = BooleanByteWrapper.GetFlag(_box0,2);
            showExperienceFightDelta = BooleanByteWrapper.GetFlag(_box0,3);
            showExperienceForGuild = BooleanByteWrapper.GetFlag(_box0,4);
            showExperienceForMount = BooleanByteWrapper.GetFlag(_box0,5);
            isIncarnationExperience = BooleanByteWrapper.GetFlag(_box0,6);
            experience = (long)reader.ReadVarUhLong();
            if (experience < 0 || experience > 9.00719925474099E+15)
            {
                throw new System.Exception("Forbidden value (" + experience + ") on element of FightResultExperienceData.experience.");
            }

            experienceLevelFloor = (long)reader.ReadVarUhLong();
            if (experienceLevelFloor < 0 || experienceLevelFloor > 9.00719925474099E+15)
            {
                throw new System.Exception("Forbidden value (" + experienceLevelFloor + ") on element of FightResultExperienceData.experienceLevelFloor.");
            }

            experienceNextLevelFloor = (long)reader.ReadVarUhLong();
            if (experienceNextLevelFloor < 0 || experienceNextLevelFloor > 9.00719925474099E+15)
            {
                throw new System.Exception("Forbidden value (" + experienceNextLevelFloor + ") on element of FightResultExperienceData.experienceNextLevelFloor.");
            }

            experienceFightDelta = (long)reader.ReadVarUhLong();
            if (experienceFightDelta < 0 || experienceFightDelta > 9.00719925474099E+15)
            {
                throw new System.Exception("Forbidden value (" + experienceFightDelta + ") on element of FightResultExperienceData.experienceFightDelta.");
            }

            experienceForGuild = (long)reader.ReadVarUhLong();
            if (experienceForGuild < 0 || experienceForGuild > 9.00719925474099E+15)
            {
                throw new System.Exception("Forbidden value (" + experienceForGuild + ") on element of FightResultExperienceData.experienceForGuild.");
            }

            experienceForMount = (long)reader.ReadVarUhLong();
            if (experienceForMount < 0 || experienceForMount > 9.00719925474099E+15)
            {
                throw new System.Exception("Forbidden value (" + experienceForMount + ") on element of FightResultExperienceData.experienceForMount.");
            }

            rerollExperienceMul = (byte)reader.ReadByte();
            if (rerollExperienceMul < 0)
            {
                throw new System.Exception("Forbidden value (" + rerollExperienceMul + ") on element of FightResultExperienceData.rerollExperienceMul.");
            }

        }


    }
}








