using Giny.IO.D2O;
using Giny.ORM.Attributes;
using Giny.ORM.Interfaces;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Giny.World.Records.Quests
{
    [D2OClass("QuestObjectiveFightMonstersOnMap")]
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

        [D2OField("dialogId")]
        public int DialogId
        {
            get;
            set;
        }

        [D2OField("parameters")]
        public List<QuestObjectiveParameterRecord> Parameters
        {
            get;
            set;
        }

        [D2OField("point")]
        public Point Point
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
    }
}
