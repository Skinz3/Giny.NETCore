using System.Collections.Generic;
using Giny.Core.Network.Messages;
using Giny.Protocol.Types;
using Giny.Core.IO.Interfaces;
using Giny.Protocol;
using Giny.Protocol.Enums;

namespace Giny.Protocol.Messages
{ 
    public class ChatServerCopyWithObjectMessage : ChatServerCopyMessage  
    { 
        public new const ushort Id = 500;
        public override ushort MessageId => Id;

        public ObjectItem[] objects;

        public ChatServerCopyWithObjectMessage()
        {
        }
        public ChatServerCopyWithObjectMessage(ObjectItem[] objects,byte channel,string content,int timestamp,string fingerprint,long receiverId,string receiverName)
        {
            this.objects = objects;
            this.channel = channel;
            this.content = content;
            this.timestamp = timestamp;
            this.fingerprint = fingerprint;
            this.receiverId = receiverId;
            this.receiverName = receiverName;
        }
        public override void Serialize(IDataWriter writer)
        {
            base.Serialize(writer);
            writer.WriteShort((short)objects.Length);
            for (uint _i1 = 0;_i1 < objects.Length;_i1++)
            {
                (objects[_i1] as ObjectItem).Serialize(writer);
            }

        }
        public override void Deserialize(IDataReader reader)
        {
            ObjectItem _item1 = null;
            base.Deserialize(reader);
            uint _objectsLen = (uint)reader.ReadUShort();
            for (uint _i1 = 0;_i1 < _objectsLen;_i1++)
            {
                _item1 = new ObjectItem();
                _item1.Deserialize(reader);
                objects[_i1] = _item1;
            }

        }


    }
}








