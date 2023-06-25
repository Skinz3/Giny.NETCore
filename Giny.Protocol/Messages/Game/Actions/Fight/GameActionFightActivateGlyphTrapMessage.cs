using System.Collections.Generic;
using Giny.Core.Network.Messages;
using Giny.Protocol.Types;
using Giny.Core.IO.Interfaces;
using Giny.Protocol;
using Giny.Protocol.Enums;

namespace Giny.Protocol.Messages
{ 
    public class GameActionFightActivateGlyphTrapMessage : AbstractGameActionMessage  
    { 
        public new const ushort Id = 6444;
        public override ushort MessageId => Id;

        public short markId;
        public bool active;

        public GameActionFightActivateGlyphTrapMessage()
        {
        }
        public GameActionFightActivateGlyphTrapMessage(short markId,bool active,short actionId,double sourceId)
        {
            this.markId = markId;
            this.active = active;
            this.actionId = actionId;
            this.sourceId = sourceId;
        }
        public override void Serialize(IDataWriter writer)
        {
            base.Serialize(writer);
            writer.WriteShort((short)markId);
            writer.WriteBoolean((bool)active);
        }
        public override void Deserialize(IDataReader reader)
        {
            base.Deserialize(reader);
            markId = (short)reader.ReadShort();
            active = (bool)reader.ReadBoolean();
        }


    }
}








