using Giny.World.Managers.Criterias;
using Giny.World.Managers.Fights.Fighters;
using Giny.World.Managers.Fights;
using Giny.World.Network;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Giny.Protocol.Enums;

namespace Giny.World.Managers.Criterions.Handlers
{
    [Criterion("Ef")]
    public class KillMonsterWithChallengeCriterion : Criterion
    {
        private short MonsterId
        {
            get;
            set;
        }

        private int Count
        {
            get;
            set;
        }
        public KillMonsterWithChallengeCriterion(string criteriaFull) : base(criteriaFull)
        {
            var split = CriteriaValue.Split(',');


            MonsterId = short.Parse(split[0]);
            Count = int.Parse(split[1]);

        }

        public override bool Eval(WorldClient client)
        {
            if (!client.Character.Fighting)
            {
                return false;
            }

            if (client.Character.Fighter.Fight is FightPvM)
            {
                var fightPvM = (FightPvM)client.Character.Fighter.Fight;


                if (fightPvM.Winners != client.Character.Fighter.Team || fightPvM.Challenges.GetNumberOfChallengesSucceeed() == 0)
                {
                    return false;
                }

                var team = fightPvM.GetTeam(TeamTypeEnum.TEAM_TYPE_MONSTER).GetFighters<MonsterFighter>(false);

                var count = team.Count(x => x.Monster.Record.Id == MonsterId);

                if (count == 0)
                {
                    return false;
                }

                return true;

            }

            return false;
        }
    }
}
