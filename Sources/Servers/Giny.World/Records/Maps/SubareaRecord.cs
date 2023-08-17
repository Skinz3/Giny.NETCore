using Giny.Core.DesignPattern;
using Giny.Core.Extensions;
using Giny.IO.D2O;
using Giny.IO.D2OClasses;
using Giny.ORM;
using Giny.ORM.Attributes;
using Giny.ORM.Interfaces;
using Giny.World.Records.Achievements;
using Giny.World.Records.Monsters;
using Giny.World.Records.Npcs;
using ProtoBuf;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Giny.World.Records.Maps
{
    [D2OClass("SubArea")]
    [Table("subareas")]
    public class SubareaRecord : IRecord
    {
        [Container]
        private static Dictionary<long, SubareaRecord> Subareas = new Dictionary<long, SubareaRecord>();

        [D2OField("id")]
        [Primary]
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
        [D2OField("areaId")]
        public int AreaId
        {
            get;
            set;
        }
        [Ignore]
        public AreaRecord Area
        {
            get;
            private set;
        }
        [D2OField("level")]
        public uint Level
        {
            get;
            set;
        }
        [ProtoSerialize]
        [D2OField("monsters")]
        public short[] MonsterIds
        {
            get;
            set;
        }
        [ProtoSerialize]
        [D2OField("quests")]
        public ObjectMapPosition[] QuestIds
        {
            get;
            set;
        }
        [ProtoSerialize]
        [D2OField("npcs")]
        public ObjectMapPosition[] NpcIds
        {
            get;
            set;
        }
        [D2OField("associatedZaapMapId")]
        public int AssociatedZaapMapId
        {
            get;
            set;
        }
        [Ignore]
        public MonsterSpawnRecord[] Monsters
        {
            get;
            set;
        }
        [Update]
        public int AchievementId
        {
            get;
            set;
        } = -1;

        [Ignore]
        public AchievementRecord? AchievementRecord
        {
            get;
            private set;
        }
        [StartupInvoke("Subarea members", StartupInvokePriority.ThirdPass)]
        public static void Initialize()
        {
            foreach (var subarea in Subareas.Values)
            {
                subarea.ReloadMembers();

            }
        }

        public void ReloadMembers()
        {
            Monsters = MonsterSpawnRecord.GetMonsterSpawnRecords(Id).ToArray();
            Area = AreaRecord.GetArea(AreaId);

            if (AchievementId > 0)
            {
                AchievementRecord = AchievementRecord.GetAchievement(AchievementId);
            }
        }

        public static SubareaRecord GetSubarea(short id)
        {
            SubareaRecord result = null;
            Subareas.TryGetValue(id, out result);
            return result;
        }
        public static IEnumerable<SubareaRecord> GetSubareas()
        {
            return Subareas.Values;
        }
    }
    [ProtoContract]
    public struct ObjectMapPosition
    {
        [ProtoMember(1)]
        public int Id;

        [ProtoMember(2)]
        public int MapId;

        public ObjectMapPosition(int id, int mapId)
        {
            this.Id = id;
            this.MapId = mapId;
        }
    }
}
