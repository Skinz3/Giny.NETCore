using Giny.Protocol.Custom.Enums;
using Giny.Protocol.Types;
using Giny.World.Managers.Experiences;
using Giny.World.Records.Maps;
using ProtoBuf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Giny.World.Records.Characters
{
    [ProtoContract]
    public class CharacterJob
    {
        [ProtoMember(1)]
        public byte JobId
        {
            get;
            set;
        }
        [ProtoMember(2)]
        public long Experience
        {
            get;
            set;
        }
        public short Level
        {
            get
            {
                return ExperienceManager.Instance.GetJobLevel(Experience);
            }
        }

        private SkillRecord m_skill;

        public SkillRecord Skill
        {
            get
            {
                if (m_skill == null)
                {
                    m_skill = SkillRecord.GetSkillByJobId(JobId);
                    return m_skill;
                }
                else
                {
                    return m_skill;
                }
            }
        }

        public JobCrafterDirectorySettings GetDirectorySettings()
        {
            return new JobCrafterDirectorySettings(JobId, (byte)Level, true);
        }
        public JobDescription GetJobDescription()
        {
            return new JobDescription(JobId, new SkillActionDescription[0]);
        }
        public JobExperience GetJobExperience()
        {
            long experienceForLevel = ExperienceManager.Instance.GetJobXPForLevel(Level);
            long experienceForNextLevel = ExperienceManager.Instance.GetJobXPForNextLevel(Level);
            return new JobExperience(JobId, (byte)Level, Experience, experienceForLevel, experienceForNextLevel);
        }
        public static IEnumerable<CharacterJob> New()
        {
            List<CharacterJob> jobs = new List<CharacterJob>();

            foreach (JobTypeEnum job in Enum.GetValues(typeof(JobTypeEnum)))
            {
                CharacterJob characterJob = new CharacterJob()
                {
                    JobId = (byte)job,
                    Experience = 0,

                };
                yield return characterJob;
            }

        }

    }
}
