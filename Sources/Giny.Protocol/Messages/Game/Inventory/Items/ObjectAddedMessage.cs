using System.Collections.Generic;
using Giny.Core.Network.Messages;
using Giny.Protocol.Types;
using Giny.Core.IO.Interfaces;
using Giny.Protocol;
using Giny.Protocol.Enums;

namespace Giny.Protocol.Messages
{ 
    public class ObjectAddedMessage : NetworkMessage  
    { 
        public  const ushort Id = 7116;
        public override ushort MessageId => Id;

        public ObjectItem @object;
        public byte origin;

        public ObjectAddedMessage()
        {
        }
        public ObjectAddedMessage(ObjectItem @object,byte origin)
        {
            this.@object = @object;
            this.origin = origin;
        }
        public override void Serialize(IDataWriter writer)
        {
            @object.Serialize(writer);
            writer.WriteByte((byte)origin);
        }
        public override void Deserialize(IDataReader reader)
        {
            @object = new ObjectItem();
            @object.Deserialize(reader);
            origin = (byte)reader.ReadByte();
            if (origin < 0)
            {
                throw new System.Exception("Forbidden value (" + origin + ") on element of ObjectAddedMessage.origin.");
            }

        }


    }
}








