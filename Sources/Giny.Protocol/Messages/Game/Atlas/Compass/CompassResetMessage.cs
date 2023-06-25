using System.Collections.Generic;
using Giny.Core.Network.Messages;
using Giny.Protocol.Types;
using Giny.Core.IO.Interfaces;
using Giny.Protocol;
using Giny.Protocol.Enums;

namespace Giny.Protocol.Messages
{ 
    public class CompassResetMessage : NetworkMessage  
    { 
        public  const ushort Id = 8958;
        public override ushort MessageId => Id;

        public byte type;

        public CompassResetMessage()
        {
        }
        public CompassResetMessage(byte type)
        {
            this.type = type;
        }
        public override void Serialize(IDataWriter writer)
        {
            writer.WriteByte((byte)type);
        }
        public override void Deserialize(IDataReader reader)
        {
            type = (byte)reader.ReadByte();
            if (type < 0)
            {
                throw new System.Exception("Forbidden value (" + type + ") on element of CompassResetMessage.type.");
            }

        }


    }
}








