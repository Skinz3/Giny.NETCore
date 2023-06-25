using System.Collections.Generic;
using Giny.Core.Network.Messages;
using Giny.Protocol.Types;
using Giny.Core.IO.Interfaces;
using Giny.Protocol;
using Giny.Protocol.Enums;

namespace Giny.Protocol.Messages
{ 
    public class GameActionFightMarkCellsMessage : AbstractGameActionMessage  
    { 
        public new const ushort Id = 6848;
        public override ushort MessageId => Id;

        public GameActionMark mark;

        public GameActionFightMarkCellsMessage()
        {
        }
        public GameActionFightMarkCellsMessage(GameActionMark mark,short actionId,double sourceId)
        {
            this.mark = mark;
            this.actionId = actionId;
            this.sourceId = sourceId;
        }
        public override void Serialize(IDataWriter writer)
        {
            base.Serialize(writer);
            mark.Serialize(writer);
        }
        public override void Deserialize(IDataReader reader)
        {
            base.Deserialize(reader);
            mark = new GameActionMark();
            mark.Deserialize(reader);
        }


    }
}








