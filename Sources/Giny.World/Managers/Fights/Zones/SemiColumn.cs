using Giny.Protocol.Custom.Enums;
using Giny.World.Managers.Fights.Cast;
using Giny.World.Records.Maps;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Giny.World.Managers.Fights.Zones
{
    public class SemiColumn : Zone
    {
        private IEnumerable<short> Cells
        {
            get;
            set;
        }

        public SemiColumn(IEnumerable<short> cells)
        {
            this.Cells = cells;
        }

        public override CellRecord[] GetCells(CellRecord centerCell, CellRecord casterCell, MapRecord map)
        {
            return Cells.Select(x => map.GetCell(x)).ToArray();
        }
    }
}
