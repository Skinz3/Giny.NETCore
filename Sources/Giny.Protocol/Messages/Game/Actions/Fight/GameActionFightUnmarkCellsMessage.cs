using System.Collections.Generic;
using Giny.Core.Network.Messages;
using Giny.Protocol.Types;
using Giny.Core.IO.Interfaces;
using Giny.Protocol;
using Giny.Protocol.Enums;

namespace Giny.Protocol.Messages
{ 
    public class GameActionFightUnmarkCellsMessage : AbstractGameActionMessage  
    { 
        public new const ushort Id = 8111;
        public override ushort MessageId => Id;

        public short markId;

        public GameActionFightUnmarkCellsMessage()
        {
        }
        public GameActionFightUnmarkCellsMessage(short markId,short actionId,double sourceId)
        {
            this.markId = markId;
            this.actionId = actionId;
            this.sourceId = sourceId;
        }
        public override void Serialize(IDataWriter writer)
        {
            base.Serialize(writer);
            writer.WriteShort((short)markId);
        }
        public override void Deserialize(IDataReader reader)
        {
            base.Deserialize(reader);
            markId = (short)reader.ReadShort();
        }


    }
}








