using System.Collections.Generic;
using Giny.Core.Network.Messages;
using Giny.Protocol.Types;
using Giny.Core.IO.Interfaces;
using Giny.Protocol;
using Giny.Protocol.Enums;

namespace Giny.Protocol.Messages
{ 
    public class GameActionFightMultipleSummonMessage : AbstractGameActionMessage  
    { 
        public new const ushort Id = 6695;
        public override ushort MessageId => Id;

        public GameContextSummonsInformation[] summons;

        public GameActionFightMultipleSummonMessage()
        {
        }
        public GameActionFightMultipleSummonMessage(GameContextSummonsInformation[] summons,short actionId,double sourceId)
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
                writer.WriteShort((short)(summons[_i1] as GameContextSummonsInformation).TypeId);
                (summons[_i1] as GameContextSummonsInformation).Serialize(writer);
            }

        }
        public override void Deserialize(IDataReader reader)
        {
            uint _id1 = 0;
            GameContextSummonsInformation _item1 = null;
            base.Deserialize(reader);
            uint _summonsLen = (uint)reader.ReadUShort();
            for (uint _i1 = 0;_i1 < _summonsLen;_i1++)
            {
                _id1 = (uint)reader.ReadUShort();
                _item1 = ProtocolTypeManager.GetInstance<GameContextSummonsInformation>((short)_id1);
                _item1.Deserialize(reader);
                summons[_i1] = _item1;
            }

        }


    }
}








