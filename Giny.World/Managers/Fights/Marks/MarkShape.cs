using Giny.Protocol.Enums;
using Giny.Protocol.Types;
using Giny.World.Managers.Maps;
using Giny.World.Records.Maps;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Giny.World.Managers.Fights.Marks
{
    public class MarkShape
    {
        public GameActionMarkCellsTypeEnum Type => GameActionMarkCellsTypeEnum.CELLS_SQUARE;

        public Fight Fight
        {
            get;
            private set;
        }
        public CellRecord Cell
        {
            get;
            private set;
        }

        public byte Size
        {
            get;
            private set;
        }
        public Color Color
        {
            get;
            set;
        }
        public MarkShape(Fight fight, CellRecord cell, Color color)
        {
            this.Fight = fight;
            this.Cell = cell;
            this.Color = color;
        }

        public GameActionMarkedCell GetGameActionMarkedCell()
        {
            return new GameActionMarkedCell()
            {
                cellId = Cell.Id,
                cellColor = Color.ToArgb(),
                cellsType = (byte)Type,
                zoneSize = Size,
            };
        }

    }
}
