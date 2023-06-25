using System.Collections.Generic;
using Giny.Core.Network.Messages;
using Giny.Protocol.Types;
using Giny.Core.IO.Interfaces;
using Giny.Protocol;
using Giny.Protocol.Enums;

namespace Giny.Protocol.Messages
{ 
    public class ObjectGroundRemovedMultipleMessage : NetworkMessage  
    { 
        public  const ushort Id = 4743;
        public override ushort MessageId => Id;

        public short[] cells;

        public ObjectGroundRemovedMultipleMessage()
        {
        }
        public ObjectGroundRemovedMultipleMessage(short[] cells)
        {
            this.cells = cells;
        }
        public override void Serialize(IDataWriter writer)
        {
            writer.WriteShort((short)cells.Length);
            for (uint _i1 = 0;_i1 < cells.Length;_i1++)
            {
                if (cells[_i1] < 0 || cells[_i1] > 559)
                {
                    throw new System.Exception("Forbidden value (" + cells[_i1] + ") on element 1 (starting at 1) of cells.");
                }

                writer.WriteVarShort((short)cells[_i1]);
            }

        }
        public override void Deserialize(IDataReader reader)
        {
            uint _val1 = 0;
            uint _cellsLen = (uint)reader.ReadUShort();
            cells = new short[_cellsLen];
            for (uint _i1 = 0;_i1 < _cellsLen;_i1++)
            {
                _val1 = (uint)reader.ReadVarUhShort();
                if (_val1 < 0 || _val1 > 559)
                {
                    throw new System.Exception("Forbidden value (" + _val1 + ") on elements of cells.");
                }

                cells[_i1] = (short)_val1;
            }

        }


    }
}








