using System.Collections.Generic;
using Giny.Core.Network.Messages;
using Giny.Protocol.Types;
using Giny.Core.IO.Interfaces;
using Giny.Protocol;
using Giny.Protocol.Enums;

namespace Giny.Protocol.Messages
{ 
    public class AbstractGameActionWithAckMessage : AbstractGameActionMessage  
    { 
        public new const ushort Id = 8218;
        public override ushort MessageId => Id;

        public short waitAckId;

        public AbstractGameActionWithAckMessage()
        {
        }
        public AbstractGameActionWithAckMessage(short waitAckId,short actionId,double sourceId)
        {
            this.waitAckId = waitAckId;
            this.actionId = actionId;
            this.sourceId = sourceId;
        }
        public override void Serialize(IDataWriter writer)
        {
            base.Serialize(writer);
            writer.WriteShort((short)waitAckId);
        }
        public override void Deserialize(IDataReader reader)
        {
            base.Deserialize(reader);
            waitAckId = (short)reader.ReadShort();
        }


    }
}








