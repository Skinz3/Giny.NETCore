using System.Collections.Generic;
using Giny.Core.Network.Messages;
using Giny.Protocol.Types;
using Giny.Core.IO.Interfaces;
using Giny.Protocol;
using Giny.Protocol.Enums;

namespace Giny.Protocol.Messages
{ 
    public class GameContextRemoveElementWithEventMessage : GameContextRemoveElementMessage  
    { 
        public new const ushort Id = 2969;
        public override ushort MessageId => Id;

        public byte elementEventId;

        public GameContextRemoveElementWithEventMessage()
        {
        }
        public GameContextRemoveElementWithEventMessage(byte elementEventId,double id)
        {
            this.elementEventId = elementEventId;
            this.id = id;
        }
        public override void Serialize(IDataWriter writer)
        {
            base.Serialize(writer);
            if (elementEventId < 0)
            {
                throw new System.Exception("Forbidden value (" + elementEventId + ") on element elementEventId.");
            }

            writer.WriteByte((byte)elementEventId);
        }
        public override void Deserialize(IDataReader reader)
        {
            base.Deserialize(reader);
            elementEventId = (byte)reader.ReadByte();
            if (elementEventId < 0)
            {
                throw new System.Exception("Forbidden value (" + elementEventId + ") on element of GameContextRemoveElementWithEventMessage.elementEventId.");
            }

        }


    }
}








