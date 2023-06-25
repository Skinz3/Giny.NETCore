using Giny.IO.D2O;
using Giny.ORM.Attributes;
using Giny.ORM.Interfaces;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Giny.World.Records.Characters
{
    [D2OClass("CharacterXPMapping")]
    [Table("experiences")]
    public class ExperienceRecord : ITable
    {
        [Container]
        private static readonly ConcurrentDictionary<long, ExperienceRecord> Experiences = new ConcurrentDictionary<long, ExperienceRecord>();

        [Ignore]
        public long Id => Level;

        [D2OField("level")]
        [Primary]
        public short Level
        {
            get;
            set;
        }
        [D2OField("experiencePoints")]
        public long ExperienceCharacter
        {
            get;
            set;
        }
       
        [Update]
        public long ExperienceJob
        {
            get;
            set;
        }

        [Update]
        public long ExperienceGuild
        {
            get;
            set;
        }
        public static IEnumerable<ExperienceRecord> GetExperiences()
        {
            return Experiences.Values;
        }

        public static ExperienceRecord GetExperienceForLevel(short level)
        {
            return Experiences[level];
        }
    }
}
