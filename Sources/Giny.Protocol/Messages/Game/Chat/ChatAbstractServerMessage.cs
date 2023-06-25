using System.Collections.Generic;
using Giny.Core.Network.Messages;
using Giny.Protocol.Types;
using Giny.Core.IO.Interfaces;
using Giny.Protocol;
using Giny.Protocol.Enums;

namespace Giny.Protocol.Messages
{ 
    public class ChatAbstractServerMessage : NetworkMessage  
    { 
        public  const ushort Id = 8034;
        public override ushort MessageId => Id;

        public byte channel;
        public string content;
        public int timestamp;
        public string fingerprint;

        public ChatAbstractServerMessage()
        {
        }
        public ChatAbstractServerMessage(byte channel,string content,int timestamp,string fingerprint)
        {
            this.channel = channel;
            this.content = content;
            this.timestamp = timestamp;
            this.fingerprint = fingerprint;
        }
        public override void Serialize(IDataWriter writer)
        {
            writer.WriteByte((byte)channel);
            writer.WriteUTF((string)content);
            if (timestamp < 0)
            {
                throw new System.Exception("Forbidden value (" + timestamp + ") on element timestamp.");
            }

            writer.WriteInt((int)timestamp);
            writer.WriteUTF((string)fingerprint);
        }
        public override void Deserialize(IDataReader reader)
        {
            channel = (byte)reader.ReadByte();
            if (channel < 0)
            {
                throw new System.Exception("Forbidden value (" + channel + ") on element of ChatAbstractServerMessage.channel.");
            }

            content = (string)reader.ReadUTF();
            timestamp = (int)reader.ReadInt();
            if (timestamp < 0)
            {
                throw new System.Exception("Forbidden value (" + timestamp + ") on element of ChatAbstractServerMessage.timestamp.");
            }

            fingerprint = (string)reader.ReadUTF();
        }


    }
}








