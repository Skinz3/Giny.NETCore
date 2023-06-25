using Giny.Core.DesignPattern;
using Giny.Protocol.Enums;
using Giny.World.Managers.Entities.Monsters;
using Giny.World.Managers.Fights;
using Giny.World.Managers.Fights.Fighters;
using Giny.World.Managers.Fights.Results;
using Giny.World.Managers.Monsters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Giny.World.Managers.Formulas
{
    public struct FightXp
    {
        public long XpSolo;
        public long XpGroup;
        public bool IsSolo;
        public long Xp;

        public FightXp(long xpSolo, long xpGroup, bool isSolo)
        {
            this.XpSolo = xpSolo;
            this.XpGroup = xpGroup;
            this.IsSolo = isSolo;
            this.Xp = IsSolo ? XpSolo : XpGroup;
        }
        public void ApplyMultiplicator(double ratio)
        {
            Xp = (long)(Xp * ratio);
        }
        public void ApplyBonus(double ratio)
        {
            Xp = (long)(Xp + (Xp * ratio));
        }
    }
    public class FightFormulas : Singleton<FightFormulas>
    {
        private static double[] XP_GROUP = new double[] { 1, 1.1, 1.5, 2.3, 3.1, 3.6, 4.2, 4.7 };

        private const int MAX_LEVEL_MALUS = 4;

        private const int KAMAS_RATE = 1;

        public FightXp GetExperiencePvM(CharacterFighter fighter, int mapRewardRate = 0, int xpIdolsBonusPercentSolo = 0, int xpIdolsBonusPercentGroup = 0)
        {
            IEnumerable<MonsterFighter> monsters = fighter.Fight.GetTeam(TeamTypeEnum.TEAM_TYPE_MONSTER).GetFighters<MonsterFighter>(false);

            IEnumerable<CharacterFighter> groupMembers = fighter.Team.GetFighters<CharacterFighter>(false);

            double lvlPlayers = 0;
            double lvlMaxGroup = 0;
            int totalPlayerForBonusGroup = 0;
            double coeffDiffLvlGroup = double.NaN;
            double ratioXpMontureSolo = double.NaN;
            double ratioXpMontureGroup = double.NaN;
            double ratioXpGuildSolo = double.NaN;
            double ratioXpGuildGroup = double.NaN;
            double xpAlliancePrismBonus = double.NaN;
            double xpBase = 0;
            double maxLvlMonster = 0;
            double lvlMonsters = 0;
            double hiddenLvlMonsters = 0;

            foreach (var mob in monsters)
            {
                xpBase = xpBase + mob.Monster.Grade.GradeXp;
                lvlMonsters = lvlMonsters + mob.Level;
                hiddenLvlMonsters = hiddenLvlMonsters + (mob.Monster.Grade.HiddenLevel > 0 ? mob.Monster.Grade.HiddenLevel : mob.Level);
                if (mob.Level > maxLvlMonster)
                {
                    maxLvlMonster = mob.Level;
                }
            }
            lvlPlayers = 0;
            lvlMaxGroup = 0;
            totalPlayerForBonusGroup = 0;

            foreach (var member in groupMembers)
            {
                lvlPlayers = lvlPlayers + member.Level;
                if (member.Level > lvlMaxGroup)
                {
                    lvlMaxGroup = member.Level;
                }
            }
            foreach (var member in groupMembers)
            {
                if (!member.IsCompanion() && member.Level >= lvlMaxGroup / 3)
                {
                    totalPlayerForBonusGroup++;
                }
            }
            coeffDiffLvlGroup = 1;
            if (lvlPlayers - 5 > lvlMonsters)
            {
                coeffDiffLvlGroup = lvlMonsters / lvlPlayers;
            }
            else if (lvlPlayers + 10 < lvlMonsters)
            {
                coeffDiffLvlGroup = (lvlPlayers + 10) / lvlMonsters;
            }
            double coeffDiffLvlSolo = 1;

            if (fighter.Level - 5 > lvlMonsters)
            {
                coeffDiffLvlSolo = lvlMonsters / fighter.Level;
            }
            else if (fighter.Level + 10 < lvlMonsters)
            {
                coeffDiffLvlSolo = (fighter.Level + 10) / lvlMonsters;
            }

            long v = Math.Min(fighter.Level, this.truncate(2.5 * maxLvlMonster));

            double xpLimitMaxLvlSolo = v / (double)fighter.Level * 100;

            double xpLimitMaxLvlGroup = v / lvlPlayers * 100;

            long xpGroupAlone = this.truncate(xpBase * XP_GROUP[0] * coeffDiffLvlSolo);

            if (totalPlayerForBonusGroup == 0)
            {
                totalPlayerForBonusGroup = 1;
            }

            long xpGroup = this.truncate(xpBase * XP_GROUP[totalPlayerForBonusGroup - 1] * coeffDiffLvlGroup);
            long xpNoSagesseAlone = this.truncate(xpLimitMaxLvlSolo / 100 * xpGroupAlone);
            long xpNoSagesseGroup = this.truncate(xpLimitMaxLvlGroup / 100 * xpGroup);
            double realMapRewardRate = (100 + mapRewardRate) / 100;
            double lvlMalusIdolsSolo = Math.Min(MAX_LEVEL_MALUS, hiddenLvlMonsters / monsters.Count() / fighter.Level);

            lvlMalusIdolsSolo = lvlMalusIdolsSolo * lvlMalusIdolsSolo;

            double lvlMalusIdolsGroup = Math.Min(MAX_LEVEL_MALUS, hiddenLvlMonsters / monsters.Count() / lvlMaxGroup);
            lvlMalusIdolsGroup = lvlMalusIdolsGroup * lvlMalusIdolsGroup;

            long idolsWisdomBonusSolo = this.truncate((100 + fighter.Level * 2.5) * this.truncate(xpIdolsBonusPercentSolo * lvlMalusIdolsSolo) / 100);
            long idolsWisdomBonusGroup = this.truncate((100 + fighter.Level * 2.5) * this.truncate(xpIdolsBonusPercentGroup * lvlMalusIdolsGroup) / 100);
            var totalWisdomSolo = Math.Max(fighter.Character.Stats.Wisdom.Total() + idolsWisdomBonusSolo, 0);
            var totalWisdomGroup = Math.Max(fighter.Character.Stats.Wisdom.Total() + idolsWisdomBonusGroup, 0);
            var xpTotalOnePlayer = this.truncate(this.truncate(xpNoSagesseAlone * (100 + totalWisdomSolo) / 100d) * realMapRewardRate);
            var xpTotalGroup = this.truncate(this.truncate(xpNoSagesseGroup * (100 + totalWisdomGroup) / 100d) * realMapRewardRate);
            double xpBonus = 1 + fighter.Character.XpBonusPercent / 100;
            double tmpSolo = xpTotalOnePlayer;
            double tmpGroup = xpTotalGroup;

            if (fighter.Character.XpRatioMount > 0)
            {
                ratioXpMontureSolo = tmpSolo * fighter.Character.XpRatioMount / 100;
                ratioXpMontureGroup = tmpGroup * fighter.Character.XpRatioMount / 100;
                tmpSolo = this.truncate(tmpSolo - ratioXpMontureSolo);
                tmpGroup = this.truncate(tmpGroup - ratioXpMontureGroup);
            }
            tmpSolo = tmpSolo * xpBonus;
            tmpGroup = tmpGroup * xpBonus;
            if (fighter.Character.XpGuildGivenPercent > 0)
            {
                ratioXpGuildSolo = tmpSolo * fighter.Character.XpGuildGivenPercent / 100;
                ratioXpGuildGroup = tmpGroup * fighter.Character.XpGuildGivenPercent / 100;
                tmpSolo = tmpSolo - ratioXpGuildSolo;
                tmpGroup = tmpGroup - ratioXpGuildGroup;
            }
            if (fighter.Character.XpAlliancePrismBonusPercent > 0)
            {
                xpAlliancePrismBonus = 1 + fighter.Character.XpAlliancePrismBonusPercent / 100;
                tmpSolo = tmpSolo * xpAlliancePrismBonus;
                tmpGroup = tmpGroup * xpAlliancePrismBonus;
            }
            xpTotalOnePlayer = this.truncate(tmpSolo);
            xpTotalGroup = this.truncate(tmpGroup);
            var _xpSolo = xpBase > 0 ? Math.Max(xpTotalOnePlayer, 1) : 0d;
            var _xpGroup = xpBase > 0 ? Math.Max(xpTotalGroup, 1) : 0d;

            return new FightXp((long)_xpSolo, (long)_xpGroup, groupMembers.Count() == 1);
        }

        public int AdjustDroppedKamas(IFightResult looter, int teamPP, long baseKamas, double bonusRatio)
        {
            var additionalPP = (looter.Prospecting * bonusRatio);
            var looterPP = looter.Prospecting + additionalPP;
            var kamas = (int)(baseKamas * (looterPP / teamPP) * KAMAS_RATE);
            return kamas;
        }
        public double AdjustDropChance(IFightResult looter, MonsterDrop item, Monster dropper, double bonusRatio)
        {
            var additionalPP = (looter.Prospecting * bonusRatio);
            var looterPP = looter.Prospecting + additionalPP;

            var rate = (item.GetDropRate((int)dropper.Grade.GradeId) * (looterPP / 100d) + 1) * ConfigFile.Instance.DropRate;

            return rate;
        }

        private long truncate(double val)
        {
            double multiplier = Math.Pow(10, 0);
            double truncatedVal = val * multiplier;
            return (long)(truncatedVal / multiplier);
        }
    }




}
