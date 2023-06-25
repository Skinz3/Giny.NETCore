using System.Collections.Generic;
using Giny.Core.Network.Messages;
using Giny.Protocol.Types;
using Giny.Core.IO.Interfaces;
using Giny.Protocol;
using Giny.Protocol.Enums;

namespace Giny.Protocol.Messages
{ 
    public class GameContextMoveMultipleElementsMessage : NetworkMessage  
    { 
        public  const ushort Id = 5560;
        public override ushort MessageId => Id;

        public EntityMovementInformations[] movements;

        public GameContextMoveMultipleElementsMessage()
        {
        }
        public GameContextMoveMultipleElementsMessage(EntityMovementInformations[] movements)
        {
            this.movements = movements;
        }
        public override void Serialize(IDataWriter writer)
        {
            writer.WriteShort((short)movements.Length);
            for (uint _i1 = 0;_i1 < movements.Length;_i1++)
            {
                (movements[_i1] as EntityMovementInformations).Serialize(writer);
            }

        }
        public override void Deserialize(IDataReader reader)
        {
            EntityMovementInformations _item1 = null;
            uint _movementsLen = (uint)reader.ReadUShort();
            for (uint _i1 = 0;_i1 < _movementsLen;_i1++)
            {
                _item1 = new EntityMovementInformations();
                _item1.Deserialize(reader);
                movements[_i1] = _item1;
            }

        }


    }
}








