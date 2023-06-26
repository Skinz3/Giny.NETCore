using System.Collections.Generic;
using Giny.Core.Network.Messages;
using Giny.Protocol.Types;
using Giny.Core.IO.Interfaces;
using Giny.Protocol;
using Giny.Protocol.Enums;

namespace Giny.Protocol.Messages
{ 
    public class DebugInClientMessage : NetworkMessage  
    { 
        public  const ushort Id = 2158;
        public override ushort MessageId => Id;

        public byte level;
        public string message;

        public DebugInClientMessage()
        {
        }
        public DebugInClientMessage(byte level,string message)
        {
            this.level = level;
            this.message = message;
        }
        public override void Serialize(IDataWriter writer)
        {
            writer.WriteByte((byte)level);
            writer.WriteUTF((string)message);
        }
        public override void Deserialize(IDataReader reader)
        {
            level = (byte)reader.ReadByte();
            if (level < 0)
            {
                throw new System.Exception("Forbidden value (" + level + ") on element of DebugInClientMessage.level.");
            }

            message = (string)reader.ReadUTF();
        }


    }
}








