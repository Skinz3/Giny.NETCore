using Giny.IO.D2O;
using Giny.ORM.Attributes;
using Giny.ORM.Interfaces;
using ProtoBuf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Giny.World.Records.Quests
{
    [D2OClass("QuestStepRewards")]
    [Table("quest_step_rewards")]
    public class QuestStepRewardRecord : IRecord
    {
        [Container]
        private static Dictionary<long, QuestStepRewardRecord> QuestStepRewards = new Dictionary<long, QuestStepRewardRecord>();

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

        [D2OField("levelMin")]
        public short LevelMin
        {
            get;
            set;
        }

        [D2OField("levelMax")]
        public short LevelMax
        {
            get;
            set;
        }

        [D2OField("kamasRatio")]

        public double KamasRatio
        {
            get;
            set;
        }

        [D2OField("experienceRatio")]
        public double ExperienceRatio
        {
            get;
            set;
        }

        [D2OField("kamasScaleWithPlayerLevel")]
        public bool KamasScaleWithPlayerLevel
        {
            get;
            set;
        }
        [Blob]
        [D2OField("itemsReward")]
        public List<ItemWithQuantity> ItemRewards
        {
            get;
            set;
        }


        [D2OField("emotesReward")]
        public List<int> EmoteReward
        {
            get;
            set;
        }


        [D2OField("jobsReward")]
        public List<int> JobsReward
        {
            get;
            set;
        }

        [D2OField("spellsReward")]
        public List<int> SpellsReward
        {
            get;
            set;
        }

        [D2OField("titlesReward")]
        public List<int> TitlesReward
        {
            get;
            set;
        }

        public static QuestStepRewardRecord GetQuestStepReward(long rewardId)
        {
            //return QuestStepRewards[rewardId];
            // temporary fix by bimbo to debug worlds errors. I think it's missing some data in the SQL file.
            return null;
        }

        public override string ToString()
        {
            return $"({Id})";
        }
    }
    [ProtoContract]
    public class ItemWithQuantity
    {
        [ProtoMember(1)]
        public short ItemId
        {
            get;
            set;
        }
        [ProtoMember(2)]
        public int Quantity
        {
            get;
            set;
        }

        public ItemWithQuantity()
        {

        }
        public ItemWithQuantity(short itemId, int quantity)
        {
            ItemId = itemId;
            Quantity = quantity;
        }
    }

}
