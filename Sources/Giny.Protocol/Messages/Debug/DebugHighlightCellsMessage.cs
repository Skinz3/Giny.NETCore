using System.Collections.Generic;
using Giny.Core.Network.Messages;
using Giny.Protocol.Types;
using Giny.Core.IO.Interfaces;
using Giny.Protocol;
using Giny.Protocol.Enums;

namespace Giny.Protocol.Messages
{ 
    public class DebugHighlightCellsMessage : NetworkMessage  
    { 
        public  const ushort Id = 7356;
        public override ushort MessageId => Id;

        public double color;
        public short[] cells;

        public DebugHighlightCellsMessage()
        {
        }
        public DebugHighlightCellsMessage(double color,short[] cells)
        {
            this.color = color;
            this.cells = cells;
        }
        public override void Serialize(IDataWriter writer)
        {
            if (color < -9.00719925474099E+15 || color > 9.00719925474099E+15)
            {
                throw new System.Exception("Forbidden value (" + color + ") on element color.");
            }

            writer.WriteDouble((double)color);
            writer.WriteShort((short)cells.Length);
            for (uint _i2 = 0;_i2 < cells.Length;_i2++)
            {
                if (cells[_i2] < 0 || cells[_i2] > 559)
                {
                    throw new System.Exception("Forbidden value (" + cells[_i2] + ") on element 2 (starting at 1) of cells.");
                }

                writer.WriteVarShort((short)cells[_i2]);
            }

        }
        public override void Deserialize(IDataReader reader)
        {
            uint _val2 = 0;
            color = (double)reader.ReadDouble();
            if (color < -9.00719925474099E+15 || color > 9.00719925474099E+15)
            {
                throw new System.Exception("Forbidden value (" + color + ") on element of DebugHighlightCellsMessage.color.");
            }

            uint _cellsLen = (uint)reader.ReadUShort();
            cells = new short[_cellsLen];
            for (uint _i2 = 0;_i2 < _cellsLen;_i2++)
            {
                _val2 = (uint)reader.ReadVarUhShort();
                if (_val2 < 0 || _val2 > 559)
                {
                    throw new System.Exception("Forbidden value (" + _val2 + ") on elements of cells.");
                }

                cells[_i2] = (short)_val2;
            }

        }


    }
}








