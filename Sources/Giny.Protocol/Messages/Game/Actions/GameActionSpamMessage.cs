using System.Collections.Generic;
using Giny.Core.Network.Messages;
using Giny.Protocol.Types;
using Giny.Core.IO.Interfaces;
using Giny.Protocol;
using Giny.Protocol.Enums;

namespace Giny.Protocol.Messages
{ 
    public class GameActionSpamMessage : NetworkMessage  
    { 
        public  const ushort Id = 9175;
        public override ushort MessageId => Id;

        public short[] cells;

        public GameActionSpamMessage()
        {
        }
        public GameActionSpamMessage(short[] cells)
        {
            this.cells = cells;
        }
        public override void Serialize(IDataWriter writer)
        {
            writer.WriteShort((short)cells.Length);
            for (uint _i1 = 0;_i1 < cells.Length;_i1++)
            {
                writer.WriteShort((short)cells[_i1]);
            }

        }
        public override void Deserialize(IDataReader reader)
        {
            int _val1 = 0;
            uint _cellsLen = (uint)reader.ReadUShort();
            cells = new short[_cellsLen];
            for (uint _i1 = 0;_i1 < _cellsLen;_i1++)
            {
                _val1 = (int)reader.ReadShort();
                cells[_i1] = (short)_val1;
            }

        }


    }
}








