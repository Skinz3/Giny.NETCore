using Giny.IO.D2I;
using Giny.IO.D2O;
using Giny.IO.D2OClasses;
using Giny.ORM.Attributes;
using Giny.ORM.Interfaces;
using Giny.Protocol.Custom.Enums;
using Giny.World.Records.Items;
using Giny.World.Records.Maps;
using Giny.World.Records.Monsters;
using Giny.World.Records.Npcs;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Giny.World.Records.Quests
{
    [D2OClass("QuestObjective", "",
        typeof(QuestObjectiveBringItemToNpc),
        typeof(QuestObjectiveBringSoulToNpc),
        typeof(QuestObjectiveCraftItem),
        typeof(QuestObjectiveDiscoverMap),
        typeof(QuestObjectiveDiscoverSubArea),
        typeof(QuestObjectiveDuelSpecificPlayer),
        typeof(QuestObjectiveFightMonster),
        typeof(QuestObjectiveFightMonstersOnMap),
        typeof(QuestObjectiveFreeForm),
        typeof(QuestObjectiveGoToNpc),
        typeof(QuestObjectiveMultiFightMonster))]

    [Table("quest_objectives")]
    public class QuestObjectiveRecord : IRecord
    {
        [Container]
        private static Dictionary<long, QuestObjectiveRecord> QuestObjectives = new Dictionary<long, QuestObjectiveRecord>();

        [Primary]
        [D2OField("id")]
        public long Id
        {
            get;
            set;
        }

        [D2OField("stepId")]
        public long StepId
        {
            get;
            set;
        }

        [D2OField("typeId")]
        public byte TypeId
        {
            get;
            set;
        }

        [Ignore]
        public QuestObjectiveTypeEnum Type => (QuestObjectiveTypeEnum)TypeId;


        [D2OField("dialogId")]
        public int DialogId
        {
            get;
            set;
        }

        [Blob]
        [D2OField("parameters")]
        public QuestObjectiveParametersRecord Parameters
        {
            get;
            set;
        }

        [Blob]
        [D2OField("coords")]
        public PointRecord Coords
        {
            get;
            set;
        }

        [D2OField("mapId")]
        public long MapId
        {
            get;
            set;
        }

        public static QuestObjectiveRecord GetQuestObjective(long id)
        {
            return QuestObjectives[id];
        }

        public static List<QuestObjectiveRecord> GetQuestObjectives()
        {
            return QuestObjectives.Values.ToList();
        }
        public CharacterQuestObjectiveRecord GetCharacterQuestObjectiveRecord()
        {
            return new CharacterQuestObjectiveRecord()
            {
                Done = false,
                ObjectiveId = Id,
            };
        }
        public override string ToString()
        {
            switch (Type)
            {
                case QuestObjectiveTypeEnum.None:
                    return $"{D2IManager.GetText(Parameters.Param0)}";
                case QuestObjectiveTypeEnum.GoToNpc:
                    return $"Aller voir {NpcRecord.GetNpcRecord((short)Parameters.Param0).Name}";
                case QuestObjectiveTypeEnum.BringItemToNpc:
                    return $"Rapporter {ItemRecord.GetItem(Parameters.Param1).Name}";
                case QuestObjectiveTypeEnum.GiveItemToNpc:
                    return $"Donner {Parameters.Param0}x [{ItemRecord.GetItem(Parameters.Param1).Name}]";
                case QuestObjectiveTypeEnum.DiscoverMap:
                    return $"Découvrir la carte {Parameters.Param0}";
                case QuestObjectiveTypeEnum.DiscoverSubarea:
                    return $"Discover subarea {Parameters.Param0}";
                case QuestObjectiveTypeEnum.DefeatMonsterOneFight:
                    return $"Vaincre x{Parameters.Param1} {MonsterRecord.GetMonsterRecord((short)Parameters.Param0).Name} en un seul combat ";
                case QuestObjectiveTypeEnum.DefeatMonsters:
                    return $"Defeat monster {Parameters.Param0}";
                case QuestObjectiveTypeEnum.UseItem:
                    return $"Use item {Parameters.Param0}";
                case QuestObjectiveTypeEnum.NpcTalkBack:
                    return $"Talk back to {Parameters.Param0}";
                case QuestObjectiveTypeEnum.Escort:
                    break;
                case QuestObjectiveTypeEnum.DuelSpecificPlayer:
                    break;
                case QuestObjectiveTypeEnum.BringSoulsToNpc:
                    break;
                case QuestObjectiveTypeEnum.DefeatOne:
                    break;
                case QuestObjectiveTypeEnum.DefeatMulti:
                    break;
                case QuestObjectiveTypeEnum.WinKromaster:
                    break;
                case QuestObjectiveTypeEnum.CraftItem:
                    break;
            }
            return "No description";

        }
    }
}
