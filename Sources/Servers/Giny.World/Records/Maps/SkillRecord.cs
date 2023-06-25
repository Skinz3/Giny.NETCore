using Giny.IO.D2O;
using Giny.ORM.Attributes;
using Giny.ORM.Interfaces;
using Giny.Protocol.Custom.Enums;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Giny.World.Records.Maps
{
    [Table("skills")]
    [D2OClass("Skill")]
    public class SkillRecord : ITable
    {
        [Container]
        private static ConcurrentDictionary<long, SkillRecord> Skills = new ConcurrentDictionary<long, SkillRecord>();

        [Primary]
        [D2OField("id")]
        public long Id
        {
            get;
            set;
        }
        [I18NField]
        [D2OField("nameId")]
        public string Name
        {
            get;
            set;
        }

        [D2OField("parentJobId")]
        public byte ParentJobId
        {
            get;
            set;
        }
        [Update]
        public List<int> ParentBonesIds
        {
            get;
            set;
        } = new List<int>();

        [D2OField("interactiveId")]
        public int InteractiveTypeId
        {
            get;
            set;
        }
        [D2OField("levelMin")]
        public short MinLevel
        {
            get;
            set;
        }
        [D2OField("gatheredRessourceItem")]
        public short GatheredRessourceItem
        {
            get;
            set;
        }
        public static IEnumerable<SkillRecord> GetSkills()
        {
            return Skills.Values;
        }
        public static SkillRecord GetSkillByJobId(byte jobId)
        {
            return Skills.Values.FirstOrDefault(x => x.ParentJobId == jobId);
        }

        public static SkillRecord GetSkill(SkillTypeEnum skillId)
        {
            return Skills[(long)skillId];
        }
    }
}
