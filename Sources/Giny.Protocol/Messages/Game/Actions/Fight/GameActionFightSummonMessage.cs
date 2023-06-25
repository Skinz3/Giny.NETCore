using System.Collections.Generic;
using Giny.Core.Network.Messages;
using Giny.Protocol.Types;
using Giny.Core.IO.Interfaces;
using Giny.Protocol;
using Giny.Protocol.Enums;

namespace Giny.Protocol.Messages
{ 
    public class GameActionFightSummonMessage : AbstractGameActionMessage  
    { 
        public new const ushort Id = 2897;
        public override ushort MessageId => Id;

        public GameFightFighterInformations[] summons;

        public GameActionFightSummonMessage()
        {
        }
        public GameActionFightSummonMessage(GameFightFighterInformations[] summons,short actionId,double sourceId)
        {
            this.summons = summons;
            this.actionId = actionId;
            this.sourceId = sourceId;
        }
        public override void Serialize(IDataWriter writer)
        {
            base.Serialize(writer);
            writer.WriteShort((short)summons.Length);
            for (uint _i1 = 0;_i1 < summons.Length;_i1++)
            {
                writer.WriteShort((short)(summons[_i1] as GameFightFighterInformations).TypeId);
                (summons[_i1] as GameFightFighterInformations).Serialize(writer);
            }

        }
        public override void Deserialize(IDataReader reader)
        {
            uint _id1 = 0;
            GameFightFighterInformations _item1 = null;
            base.Deserialize(reader);
            uint _summonsLen = (uint)reader.ReadUShort();
            for (uint _i1 = 0;_i1 < _summonsLen;_i1++)
            {
                _id1 = (uint)reader.ReadUShort();
                _item1 = ProtocolTypeManager.GetInstance<GameFightFighterInformations>((short)_id1);
                _item1.Deserialize(reader);
                summons[_i1] = _item1;
            }

        }


    }
}








