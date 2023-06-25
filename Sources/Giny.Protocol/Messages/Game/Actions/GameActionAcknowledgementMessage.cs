using System.Collections.Generic;
using Giny.Core.Network.Messages;
using Giny.Protocol.Types;
using Giny.Core.IO.Interfaces;
using Giny.Protocol;
using Giny.Protocol.Enums;

namespace Giny.Protocol.Messages
{ 
    public class GameActionAcknowledgementMessage : NetworkMessage  
    { 
        public  const ushort Id = 2893;
        public override ushort MessageId => Id;

        public bool valid;
        public byte actionId;

        public GameActionAcknowledgementMessage()
        {
        }
        public GameActionAcknowledgementMessage(bool valid,byte actionId)
        {
            this.valid = valid;
            this.actionId = actionId;
        }
        public override void Serialize(IDataWriter writer)
        {
            writer.WriteBoolean((bool)valid);
            writer.WriteByte((byte)actionId);
        }
        public override void Deserialize(IDataReader reader)
        {
            valid = (bool)reader.ReadBoolean();
            actionId = (byte)reader.ReadByte();
        }


    }
}








