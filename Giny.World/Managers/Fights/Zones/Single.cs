
using Giny.Protocol.Custom.Enums;
using Giny.World.Managers.Fights.Cast;
using Giny.World.Records.Maps;
using System;

namespace Giny.World.Managers.Fights.Zones
{
    public class Single : Zone
    {
        public Single()
        {
        }

        public override CellRecord[] GetCells(CellRecord centerCell, CellRecord casterCell, MapRecord map)
        {
            return new CellRecord[] { centerCell };
        }


    }
}
