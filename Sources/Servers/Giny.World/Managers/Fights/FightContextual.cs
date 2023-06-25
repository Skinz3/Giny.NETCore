using Giny.Protocol.Enums;
using Giny.Protocol.Types;
using Giny.World.Managers.Entities.Characters;
using Giny.World.Managers.Fights.Fighters;
using Giny.World.Managers.Fights.Results;
using Giny.World.Records.Maps;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Giny.World.Managers.Fights
{
    public class FightContextual : Fight
    {
        public override FightTypeEnum FightType => FightTypeEnum.FIGHT_TYPE_PvMA;

        public override bool ShowBlades => true;

        public override bool SpawnJoin => true;

        public FightContextual(Character origin, int id,  MapRecord map, FightTeam blueTeam, FightTeam redTeam, CellRecord cell)
            : base(origin, id, map, blueTeam, redTeam, cell)
        {
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
        public override void OnFightEnded()
        {
            if (Winners == GetTeam(TeamTypeEnum.TEAM_TYPE_PLAYER))
            {
              // do stuff
            }
        }

        protected override IEnumerable<IFightResult> GenerateResults()
        {
            IEnumerable<IFightResult> results = GetFighters<Fighter>(false).Where(x => !x.IsSummoned()).Select(x => x.GetFightResult()).ToArray();
            return results;
        }
        public override void OnFighterJoined(Fighter fighter)
        {

        }

        public override void OnFightStarted()
        {

        }
    }
}
