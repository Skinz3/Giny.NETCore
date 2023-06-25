using System.Collections.Generic;
using Giny.Core.Network.Messages;
using Giny.Protocol.Types;
using Giny.Core.IO.Interfaces;
using Giny.Protocol;
using Giny.Protocol.Enums;

namespace Giny.Protocol.Messages
{ 
    public class FinishMoveListMessage : NetworkMessage  
    { 
        public  const ushort Id = 820;
        public override ushort MessageId => Id;

        public FinishMoveInformations[] finishMoves;

        public FinishMoveListMessage()
        {
        }
        public FinishMoveListMessage(FinishMoveInformations[] finishMoves)
        {
            this.finishMoves = finishMoves;
        }
        public override void Serialize(IDataWriter writer)
        {
            writer.WriteShort((short)finishMoves.Length);
            for (uint _i1 = 0;_i1 < finishMoves.Length;_i1++)
            {
                (finishMoves[_i1] as FinishMoveInformations).Serialize(writer);
            }

        }
        public override void Deserialize(IDataReader reader)
        {
            FinishMoveInformations _item1 = null;
            uint _finishMovesLen = (uint)reader.ReadUShort();
            for (uint _i1 = 0;_i1 < _finishMovesLen;_i1++)
            {
                _item1 = new FinishMoveInformations();
                _item1.Deserialize(reader);
                finishMoves[_i1] = _item1;
            }

        }


    }
}








