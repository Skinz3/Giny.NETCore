using Giny.Protocol.Types;
using Giny.World.Managers.Entities.Characters;
using Giny.World.Records.Achievements;
using ProtoBuf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Giny.World.Managers.Achievements
{
    [ProtoContract]
    public class CharacterAchievement
    {
        [ProtoMember(1)]
        public short AchievementId
        {
            get;
            set;
        }

        [ProtoMember(2)]
        public bool Rewarded
        {
            get;
            set;
        }

        [ProtoMember(3)]
        public short FinishedLevel
        {
            get;
            set;
        }
        [ProtoMember(4)]
        public List<int> FinishedObjectives
        {
            get;
            set;
        } = new List<int>();

        public bool Finished => FinishedObjectives.Count == Record.Objectives.Count;

        public AchievementRecord Record
        {
            get;
            private set;
        }
        public CharacterAchievement()
        {

        }
        public CharacterAchievement(short achievementId, short finishedLevel, bool rewarded = false)
        {
            AchievementId = achievementId;
            FinishedLevel = finishedLevel;
            Rewarded = rewarded;
        }

        public void Achieve()
        {
            foreach (var objectiveRecord in Record.Objectives)
            {
                ReachObjective(objectiveRecord.Id);
            }
        }

        public void Initialize()
        {
            this.Record = AchievementRecord.GetAchievement(AchievementId);
        }

        public AchievementAchieved GetAchievementAchieved(long characterId)
        {
            return Rewarded ? new AchievementAchieved(AchievementId, characterId) : new AchievementAchievedRewardable(FinishedLevel, AchievementId, characterId);
        }

        public void ReachObjective(long id)
        {
            if (Finished)
            {
                return;
            }

            if (!FinishedObjectives.Contains((int)id))
            {
                FinishedObjectives.Add((int)id);
            }
        }

        public Achievement GetAchievement(Character character)
        {
            List<AchievementObjective> finishedObjectives = new List<AchievementObjective>();

            foreach (var objectiveId in FinishedObjectives)
            {
                var objectiveRecord = Record.Objectives.FirstOrDefault(x => x.Id == objectiveId);

                finishedObjectives.Add(new AchievementObjective(objectiveId, objectiveRecord.GetMaxValue()));
            }

            List<AchievementStartedObjective> startedObjectives = new List<AchievementStartedObjective>();

            foreach (var objectiveRecord in Record.Objectives.Where(x => !FinishedObjectives.Contains((int)x.Id)))
            {
                startedObjectives.Add(new AchievementStartedObjective(objectiveRecord.GetValue(character.Client), (int)objectiveRecord.Id, objectiveRecord.GetMaxValue()));
            }

            return new Achievement(AchievementId, finishedObjectives.ToArray(), startedObjectives.ToArray());
        }
    }
}
