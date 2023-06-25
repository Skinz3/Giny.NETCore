using Giny.Core.DesignPattern;
using Giny.World.Records;
using Giny.World.Records.Characters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Giny.World.Managers.Experiences
{
    public class ExperienceManager : Singleton<ExperienceManager>
    {
        public const short MaxLevel = 200;

        public const short MaxLevelOmega = 1000;

        private ExperienceRecord HighestExperienceOmega;

        private ExperienceRecord HighestExperience;

        [StartupInvoke(StartupInvokePriority.SixthPath)]
        public void Initialize()
        {
            HighestExperienceOmega = ExperienceRecord.GetExperienceForLevel(MaxLevelOmega);
            HighestExperience = ExperienceRecord.GetExperienceForLevel(MaxLevel);
        }

        public short GetCharacterLevel(long experience)
        {
            if (experience >= HighestExperienceOmega.ExperienceCharacter)
                return MaxLevelOmega;

            return (short)(ExperienceRecord.GetExperiences().First(entry => entry.ExperienceCharacter > experience).Level - 1);
        }

        public short GetCharacterLevelRegular(long experience)
        {
            if (experience >= HighestExperience.ExperienceCharacter)
                return MaxLevel;

            return (short)(ExperienceRecord.GetExperiences().First(entry => entry.ExperienceCharacter > experience).Level - 1);
        }


        public short GetJobLevel(long experience)
        {
            if (experience >= HighestExperience.ExperienceJob)
                return MaxLevel;

            return (short)(ExperienceRecord.GetExperiences().First(entry => entry.ExperienceJob > experience).Level - 1);
        }

        public byte GetGuildLevel(long experience)
        {
            if (experience >= HighestExperience.ExperienceGuild)
                return (byte)MaxLevel;

            return (byte)(ExperienceRecord.GetExperiences().First(entry => entry.ExperienceGuild > experience).Level - 1);
        }

        public long HighestExperienceGuild()
        {
            return HighestExperience.ExperienceGuild;
        }
        public long GetJobXPForLevel(short level)
        {
            return ExperienceRecord.GetExperienceForLevel(level).ExperienceJob;
        }
        public long GetJobXPForNextLevel(short level)
        {
            if (level >= MaxLevel)
                return HighestExperience.ExperienceJob;

            return GetJobXPForLevel((short)(level + 1));
        }
        public long GetGuildXPForLevel(byte level)
        {
            return ExperienceRecord.GetExperienceForLevel(level).ExperienceGuild;
        }
        public long GetGuildXPForNextLevel(byte level)
        {
            if (level >= MaxLevel)
                return HighestExperience.ExperienceGuild;

            return GetGuildXPForLevel((byte)(level + 1));
        }

        public long GetCharacterXPForLevel(short level)
        {
            return ExperienceRecord.GetExperienceForLevel(level).ExperienceCharacter;
        }
        public long GetCharacterXPForNextLevel(short level)
        {
            if (level >= MaxLevelOmega)
                return HighestExperienceOmega.ExperienceCharacter;

            return GetCharacterXPForLevel((short)(level + 1));
        }
    }
}
