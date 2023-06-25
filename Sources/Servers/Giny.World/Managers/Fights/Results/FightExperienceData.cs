using Giny.Protocol.Types;
using Giny.World.Managers.Entities.Characters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Giny.World.Managers.Fights.Results
{
    public class FightExperienceData : ResultAdditionalData
    {
        public bool ShowExperience
        {
            get;
            set;
        }
        public bool ShowExperienceLevelFloor
        {
            get;
            set;
        }
        public bool ShowExperienceNextLevelFloor
        {
            get;
            set;
        }
        public bool ShowExperienceFightDelta
        {
            get;
            set;
        }
        public bool ShowExperienceForGuild
        {
            get;
            set;
        }
        public bool ShowExperienceForMount
        {
            get;
            set;
        }
        public byte RerollExperienceMul
        {
            get;
            set;
        }
        public bool IsIncarnationExperience
        {
            get;
            set;
        }
        public long ExperienceFightDelta
        {
            get;
            set;
        }
        public long ExperienceForGuild
        {
            get;
            set;
        }
        public long ExperienceForMount
        {
            get;
            set;
        }
        public FightExperienceData(Character character)
            : base(character)
        {
        }
        public override FightResultAdditionalData GetFightResultAdditionalData()
        {
            return new FightResultExperienceData()
            {
                experience = Character.Experience,
                experienceFightDelta = ExperienceFightDelta,
                experienceForGuild = ExperienceForGuild,
                experienceForMount = ExperienceForMount,
                experienceLevelFloor = Character.LowerBoundExperience,
                experienceNextLevelFloor = Character.UpperBoundExperience,
                isIncarnationExperience = IsIncarnationExperience,
                rerollExperienceMul = RerollExperienceMul,
                showExperience = ShowExperience,
                showExperienceFightDelta = ShowExperienceFightDelta,
                showExperienceForGuild = ShowExperienceForGuild,
                showExperienceForMount = ShowExperienceForMount,
                showExperienceLevelFloor = ShowExperienceLevelFloor,
                showExperienceNextLevelFloor = ShowExperienceNextLevelFloor,

            };
        }
        public override void Apply()
        {
            base.Character.AddExperience(this.ExperienceFightDelta, false);

            if (base.Character.HasGuild && this.ExperienceForGuild > 0)
            {
                base.Character.Guild.AddExp(Character.GuildMember, ExperienceForGuild);
            }
        }
    }
}
