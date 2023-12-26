using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Giny.Core.DesignPattern;
using Giny.Core.Time;
using Giny.Protocol.Custom.Enums;
using Giny.Protocol.Enums;
using Giny.Protocol.Messages;
using Giny.Protocol.Types;
using Giny.World.Managers.Entities.Characters;
using Giny.World.Managers.Entities.Monsters;
using Giny.World.Managers.Fights.Cast;
using Giny.World.Managers.Fights.Challenges;
using Giny.World.Managers.Fights.Fighters;
using Giny.World.Managers.Fights.Results;
using Giny.World.Managers.Formulas;
using Giny.World.Managers.Monsters;
using Giny.World.Records.Maps;

namespace Giny.World.Managers.Fights
{
    public class FightPvM : Fight
    {
        public override FightTypeEnum FightType => FightTypeEnum.FIGHT_TYPE_PvM;

        public override bool ShowBlades => true;

        public override bool SpawnJoin => true;

        private MonsterGroup MonsterGroup
        {
            get;
            set;
        }

        public FightChallenges Challenges
        {
            get;
            private set;
        }
        public FightPvM(Character origin, int id, MapRecord map, FightTeam blueTeam, FightTeam redTeam, CellRecord cell, MonsterGroup monsterGroup)
            : base(origin, id, map, blueTeam, redTeam, cell)
        {
            this.MonsterGroup = monsterGroup;

            if (map.IsDungeonMap && MonsterGroup is ModularMonsterGroup)
            {
                var nextMapId = map.GetNextRoomMapId();

                if (nextMapId.HasValue)
                {
                    this.TargetMapId = nextMapId;
                }
            }

            Challenges = new FightChallenges(this);

        }
        public override FightCommonInformations GetFightCommonInformations()
        {
            return new FightCommonInformations((short)Id, (byte)FightType, GetFightTeamInformations(),
                new short[]
                {
                    BlueTeam.BladesCell.Id,RedTeam.BladesCell.Id
                }
                , GetFightOptionsInformations());
        }


        public override int GetPlacementDelay()
        {
            return 30;
        }
        /*
         * Before fight ended.
         */
        protected override void OnWinnersDetermined()
        {
            Challenges.OnWinnersDetermined();

        }
        /*
         * When fight ended.
         */
        public override void OnFightEnded()
        {
            if (ShowBlades && (this.Map.Instance.MonsterGroupCount < MonstersManager.MaxGroupPerMap))
            {
                if (Winners == GetTeam(TeamTypeEnum.TEAM_TYPE_MONSTER) || !Started && MonsterGroup.RespawnOnVictory)
                {
                    Map.Instance.AddEntity(MonsterGroup);
                }
            }
        }
        public bool GroupExistOnMap()
        {
            return Map.Instance.MonsterGroupExists(this.MonsterGroup);
        }

        protected override IEnumerable<IFightResult> GenerateResults()
        {
            IEnumerable<IFightResult> results = GetFighters<Fighter>(false).Where(x => !x.IsSummoned()).Select(x => x.GetFightResult()).ToArray();

            foreach (var team in GetTeams())
            {
                IEnumerable<Fighter> droppers = team.EnemyTeam.GetFighters<Fighter>(false).Where(entry => !entry.Alive && entry.CanDrop);

                var looters = results.Where(x => x.CanLoot(team)).OrderByDescending(entry => entry.Prospecting);

                var teamPP = team.GetFighters<CharacterFighter>(false).Sum(entry => entry.Stats[CharacteristicEnum.MAGIC_FIND].TotalInContext());

                var kamas = Winners == team ? droppers.Sum(entry => entry.GetDroppedKamas()) * team.GetFighters<CharacterFighter>().Count() : 0;

                foreach (var looter in looters)
                {
                    double xpBonusRatio = 0d;
                    double dropBonusRatio = 0d;

                    if (looter is FightPlayerResult && looter.Outcome == FightOutcomeEnum.RESULT_VICTORY)
                    {

                        FightPlayerResult playerResult = (FightPlayerResult)looter;

                        if (team == Challenges.GetTeamChallenged())
                        {
                            if (playerResult.Fighter.ChallengeBonus == ChallengeBonusEnum.CHALLENGE_EXPERIENCE_BONUS)
                            {
                                xpBonusRatio += Challenges.GetChallengesExpRatioBonus();
                            }
                            else if (playerResult.Fighter.ChallengeBonus == ChallengeBonusEnum.CHALLENGE_DROP_BONUS)
                            {
                                dropBonusRatio += Challenges.GetChallengesDropRatioBonus();
                            }
                        }

                        playerResult.AddEarnedExperience(xpBonusRatio, 0);
                    }

                    if (team == Winners)
                    {
                        foreach (var item in droppers.SelectMany(dropper => dropper.RollLoot(looter, dropBonusRatio)))
                        {
                            looter.Loot.AddItem(item);
                        }
                    }

                    looter.Loot.Kamas = teamPP > 0 ? FightFormulas.Instance.AdjustDroppedKamas(looter, teamPP, kamas, dropBonusRatio) : 0;
                }
            }
            return results;
        }

        public override void OnFighterJoined(Fighter fighter)
        {
            if (!Started)
            {
                if (MonsterGroup is ModularMonsterGroup)
                {
                    FightTeam monsterTeam = this.GetTeam(TeamTypeEnum.TEAM_TYPE_MONSTER);

                    FightTeam playerTeam = monsterTeam.EnemyTeam;

                    if (fighter.Team == playerTeam)
                    {
                        var modularGroup = (ModularMonsterGroup)MonsterGroup;

                        foreach (var monsterFighter in modularGroup.CreateFighters(monsterTeam, monsterTeam.GetFightersCount(), playerTeam.GetFightersCount()))
                        {
                            monsterTeam.AddFighter(monsterFighter);
                        }
                    }
                }

                if (fighter is CharacterFighter && this.Phase == FightPhaseEnum.Placement)
                {
                    Challenges.OnFighterJoined((CharacterFighter)fighter);
                }


            }
        }

        protected override void OnPlacementStarted()
        {
            Challenges.OnPlacementStarted();
        }

        public override void OnFightStarted()
        {
            Challenges.OnFightStart();
            MonsterGroup?.OnFightStarted(this);
        }
    }
}
