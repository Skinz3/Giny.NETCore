using Giny.Core.Network.Messages;
using Giny.Protocol.Messages;
using Giny.Protocol.Types;
using Giny.World.Managers.Entities;
using Giny.World.Managers.Entities.Characters;
using Giny.World.Network;
using Giny.World.Records;
using Giny.World.Records.Maps;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Giny.World.Managers.Maps.Instances
{
    public class ClassicMapInstance : MapInstance
    {
        public ClassicMapInstance(MapRecord record) : base(record)
        {

        }

        public override MapComplementaryInformationsDataMessage GetMapComplementaryInformationsDataMessage(Character character)
        {
            return new MapComplementaryInformationsDataMessage(character.Map.SubareaId, Record.Id, GetHousesInformations(), GetGameRolePlayActorsInformations(),
              GetInteractiveElements(character), GetStatedElements(), GetMapObstacles(), GetFightsCommonInformations(), HasAgressiveMonsters(),
              GetFightStartingPositions());
        }


    }
}
