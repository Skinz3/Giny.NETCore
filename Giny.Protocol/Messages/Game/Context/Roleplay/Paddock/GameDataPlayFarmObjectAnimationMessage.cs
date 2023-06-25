using System.Collections.Generic;
using Giny.Core.Network.Messages;
using Giny.Protocol.Types;
using Giny.Core.IO.Interfaces;
using Giny.Protocol;
using Giny.Protocol.Enums;

namespace Giny.Protocol.Messages
{ 
    public class GameDataPlayFarmObjectAnimationMessage : NetworkMessage  
    { 
        public  const ushort Id = 1970;
        public override ushort MessageId => Id;

        public short[] cellId;

        public GameDataPlayFarmObjectAnimationMessage()
        {
        }
        public GameDataPlayFarmObjectAnimationMessage(short[] cellId)
        {
            this.cellId = cellId;
        }
        public override void Serialize(IDataWriter writer)
        {
            writer.WriteShort((short)cellId.Length);
            for (uint _i1 = 0;_i1 < cellId.Length;_i1++)
            {
                if (cellId[_i1] < 0 || cellId[_i1] > 559)
                {
                    throw new System.Exception("Forbidden value (" + cellId[_i1] + ") on element 1 (starting at 1) of cellId.");
                }

                writer.WriteVarShort((short)cellId[_i1]);
            }

        }
        public override void Deserialize(IDataReader reader)
        {
            uint _val1 = 0;
            uint _cellIdLen = (uint)reader.ReadUShort();
            cellId = new short[_cellIdLen];
            for (uint _i1 = 0;_i1 < _cellIdLen;_i1++)
            {
                _val1 = (uint)reader.ReadVarUhShort();
                if (_val1 < 0 || _val1 > 559)
                {
                    throw new System.Exception("Forbidden value (" + _val1 + ") on elements of cellId.");
                }

                cellId[_i1] = (short)_val1;
            }

        }


    }
}








