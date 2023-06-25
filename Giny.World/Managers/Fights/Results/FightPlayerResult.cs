
using Giny.Core.DesignPattern;
using Giny.ORM;
using Giny.Protocol.Enums;
using Giny.Protocol.Types;
using Giny.World.Api;
using Giny.World.Managers.Entities.Characters;
using Giny.World.Managers.Experiences;
using Giny.World.Managers.Fights.Fighters;
using Giny.World.Managers.Formulas;
using Giny.World.Managers.Guilds;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Giny.World.Managers.Fights.Results
{
    public class FightPlayerResult : FightResult<CharacterFighter>
    {
        public Character Character
        {
            get
            {
                return base.Fighter.Character;
            }
        }
        public short CharacterLevel
        {
            get
            {
                return this.Character.Level;
            }
        }
        public FightExperienceData ExperienceData
        {
            get;
            private set;
        }
        public FightPvpData PvpData
        {
            get;
            private set;
        }
        public FightPlayerResult(CharacterFighter fighter, FightOutcomeEnum outcome, Loot loot)
            : base(fighter, outcome, loot)
        {
        }
        public override bool CanLoot(FightTeam team)
        {
            return base.Fighter.Team == team && !base.Fighter.Left;
        }
        public override FightResultListEntry GetFightResultListEntry()
        {
            List<FightResultAdditionalData> list = new System.Collections.Generic.List<FightResultAdditionalData>();

            if (this.ExperienceData != null)
            {
                list.Add(this.ExperienceData.GetFightResultAdditionalData());
            }

            if (this.PvpData != null)
            {
                list.Add(this.PvpData.GetFightResultAdditionalData());
            }

            return new FightResultPlayerListEntry()
            {
                outcome = (short)base.Outcome,
                alive = Alive,
                id = Id,
                level = CharacterLevel,
                additional = list.ToArray(),
                rewards = Loot.GetFightLoot(),
                wave = 0
            };
        }
        [WIP]
        public override void Apply()
        {
            this.Character.AddKamas((int)base.Loot.Kamas);

            foreach (DroppedItem current in base.Loot.Items.Values)
            {
                this.Character.Inventory.AddItem(current.ItemGId, current.Amount);
            }
            if (this.ExperienceData != null)
            {
                this.ExperienceData.Apply();
            }
            if (this.PvpData != null)
            {
                this.PvpData.Apply();
            }

            FightEventApi.PlayerResultApplied(this);

            this.Character.RefreshStats();
        }
        [WIP]
        public void AddEarnedExperience(double bonusRatio, int mapRewardRate, int xpIdolBonusPercentSolo, int xpIdolBonusPercentGroup)
        {
            FightXp fightXp = FightFormulas.Instance.GetExperiencePvM(Fighter, mapRewardRate, xpIdolBonusPercentSolo, xpIdolBonusPercentGroup);
            fightXp.ApplyMultiplicator(ConfigFile.Instance.XpRate);
            fightXp.ApplyBonus(bonusRatio);

            var experience = fightXp.Xp;

            if (!base.Fighter.Left)
            {
                if (this.ExperienceData == null)
                {
                    this.ExperienceData = new FightExperienceData(this.Character);
                }
                if (this.Character.HasGuild && this.Character.GuildMember.ExperienceGivenPercent > 0)
                {
                    long num = (int)(experience * (Character.GuildMember.ExperienceGivenPercent * 0.01d));
                    long num2 = (int)this.Character.Guild.AdjustGivenExperience(this.Character, (long)num);
                    num2 = ((num2 > ExperienceManager.Instance.HighestExperienceGuild()) ? ExperienceManager.Instance.HighestExperienceGuild() : num2);
                    experience -= num2;
                    if (num2 > 0)
                    {
                        this.ExperienceData.ShowExperienceForGuild = true;
                        this.ExperienceData.ExperienceForGuild += num2;
                        this.Character.Guild.Record.UpdateElement();
                    }
                }


                this.ExperienceData.ShowExperienceFightDelta = true;
                this.ExperienceData.ShowExperience = true;
                this.ExperienceData.ShowExperienceLevelFloor = true;
                this.ExperienceData.ShowExperienceNextLevelFloor = true;


                if (this.Character.HasGuild && this.Character.GuildMember.ExperienceGivenPercent > 0)
                {
                    long num = (long)(experience * (1 - this.Character.GuildMember.ExperienceGivenPercent * 0.01d));
                    this.ExperienceData.ExperienceFightDelta += num;
                }
                else
                {
                    this.ExperienceData.ExperienceFightDelta += experience;
                }
            }
        }
        [WIP]
        public void SetEarnedHonor(short honor, short dishonor)
        {
            /* if (this.PvpData == null)
             {
                 this.PvpData = new FightPvpData(this.Character);
             }
             this.PvpData.HonorDelta = honor;
             this.PvpData.Honor = this.Character.Record.Alignment.Honor;
             this.PvpData.Grade = (byte)this.Character.Record.Alignment.Grade;
             this.PvpData.MinHonorForGrade = this.Character.Record.Alignment.HonorGradeFloor;
             this.PvpData.MaxHonorForGrade = this.Character.Record.Alignment.HonorGradeNextFloor; */
        }
    }
}
