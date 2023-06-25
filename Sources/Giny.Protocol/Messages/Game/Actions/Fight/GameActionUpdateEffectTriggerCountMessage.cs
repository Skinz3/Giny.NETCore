using System.Collections.Generic;
using Giny.Core.Network.Messages;
using Giny.Protocol.Types;
using Giny.Core.IO.Interfaces;
using Giny.Protocol;
using Giny.Protocol.Enums;

namespace Giny.Protocol.Messages
{ 
    public class GameActionUpdateEffectTriggerCountMessage : NetworkMessage  
    { 
        public  const ushort Id = 5582;
        public override ushort MessageId => Id;

        public GameFightEffectTriggerCount[] targetIds;

        public GameActionUpdateEffectTriggerCountMessage()
        {
        }
        public GameActionUpdateEffectTriggerCountMessage(GameFightEffectTriggerCount[] targetIds)
        {
            this.targetIds = targetIds;
        }
        public override void Serialize(IDataWriter writer)
        {
            writer.WriteShort((short)targetIds.Length);
            for (uint _i1 = 0;_i1 < targetIds.Length;_i1++)
            {
                (targetIds[_i1] as GameFightEffectTriggerCount).Serialize(writer);
            }

        }
        public override void Deserialize(IDataReader reader)
        {
            GameFightEffectTriggerCount _item1 = null;
            uint _targetIdsLen = (uint)reader.ReadUShort();
            for (uint _i1 = 0;_i1 < _targetIdsLen;_i1++)
            {
                _item1 = new GameFightEffectTriggerCount();
                _item1.Deserialize(reader);
                targetIds[_i1] = _item1;
            }

        }


    }
}








