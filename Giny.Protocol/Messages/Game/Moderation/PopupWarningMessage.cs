using System.Collections.Generic;
using Giny.Core.Network.Messages;
using Giny.Protocol.Types;
using Giny.Core.IO.Interfaces;
using Giny.Protocol;
using Giny.Protocol.Enums;

namespace Giny.Protocol.Messages
{ 
    public class PopupWarningMessage : NetworkMessage  
    { 
        public  const ushort Id = 8260;
        public override ushort MessageId => Id;

        public byte lockDuration;
        public string author;
        public string content;

        public PopupWarningMessage()
        {
        }
        public PopupWarningMessage(byte lockDuration,string author,string content)
        {
            this.lockDuration = lockDuration;
            this.author = author;
            this.content = content;
        }
        public override void Serialize(IDataWriter writer)
        {
            if (lockDuration < 0 || lockDuration > 255)
            {
                throw new System.Exception("Forbidden value (" + lockDuration + ") on element lockDuration.");
            }

            writer.WriteByte((byte)lockDuration);
            writer.WriteUTF((string)author);
            writer.WriteUTF((string)content);
        }
        public override void Deserialize(IDataReader reader)
        {
            lockDuration = (byte)reader.ReadSByte();
            if (lockDuration < 0 || lockDuration > 255)
            {
                throw new System.Exception("Forbidden value (" + lockDuration + ") on element of PopupWarningMessage.lockDuration.");
            }

            author = (string)reader.ReadUTF();
            content = (string)reader.ReadUTF();
        }


    }
}








