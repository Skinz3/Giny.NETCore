using System.Collections.Generic;
using Giny.Core.Network.Messages;
using Giny.Protocol.Types;
using Giny.Core.IO.Interfaces;
using Giny.Protocol;
using Giny.Protocol.Enums;

namespace Giny.Protocol.Messages
{ 
    public class ObjectGroundListAddedMessage : NetworkMessage  
    { 
        public  const ushort Id = 8468;
        public override ushort MessageId => Id;

        public short[] cells;
        public short[] referenceIds;

        public ObjectGroundListAddedMessage()
        {
        }
        public ObjectGroundListAddedMessage(short[] cells,short[] referenceIds)
        {
            this.cells = cells;
            this.referenceIds = referenceIds;
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

            writer.WriteShort((short)referenceIds.Length);
            for (uint _i2 = 0;_i2 < referenceIds.Length;_i2++)
            {
                if (referenceIds[_i2] < 0)
                {
                    throw new System.Exception("Forbidden value (" + referenceIds[_i2] + ") on element 2 (starting at 1) of referenceIds.");
                }

                writer.WriteVarShort((short)referenceIds[_i2]);
            }

        }
        public override void Deserialize(IDataReader reader)
        {
            uint _val1 = 0;
            uint _val2 = 0;
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

            uint _referenceIdsLen = (uint)reader.ReadUShort();
            referenceIds = new short[_referenceIdsLen];
            for (uint _i2 = 0;_i2 < _referenceIdsLen;_i2++)
            {
                _val2 = (uint)reader.ReadVarUhShort();
                if (_val2 < 0)
                {
                    throw new System.Exception("Forbidden value (" + _val2 + ") on elements of referenceIds.");
                }

                referenceIds[_i2] = (short)_val2;
            }

        }


    }
}








