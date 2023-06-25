using Giny.Protocol.Enums;
using Giny.Protocol.Messages;
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
    public class FightDual : Fight
    {
        public override FightTypeEnum FightType => FightTypeEnum.FIGHT_TYPE_CHALLENGE;

        public override bool ShowBlades => true;

        public override bool SpawnJoin => false;

        public FightDual(Character origin, int id, MapRecord map, FightTeam blueTeam, FightTeam redTeam, CellRecord cell) : base(origin, id, map, blueTeam, redTeam, cell)
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

        public override void OnFightEnded()
        {

        }


        protected override IEnumerable<IFightResult> GenerateResults()
        {
            return from entry in base.GetFighters<Fighter>(false)
                   select entry into fighter
                   select fighter.GetFightResult();
        }

        public override void OnFighterJoined(Fighter fighter)
        {

        }

        public override void OnFightStarted()
        {

        }
    }
}
