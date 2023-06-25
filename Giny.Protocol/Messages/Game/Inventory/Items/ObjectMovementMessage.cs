using System.Collections.Generic;
using Giny.Core.Network.Messages;
using Giny.Protocol.Types;
using Giny.Core.IO.Interfaces;
using Giny.Protocol;
using Giny.Protocol.Enums;

namespace Giny.Protocol.Messages
{ 
    public class ObjectMovementMessage : NetworkMessage  
    { 
        public  const ushort Id = 5879;
        public override ushort MessageId => Id;

        public int objectUID;
        public short position;

        public ObjectMovementMessage()
        {
        }
        public ObjectMovementMessage(int objectUID,short position)
        {
            this.objectUID = objectUID;
            this.position = position;
        }
        public override void Serialize(IDataWriter writer)
        {
            if (objectUID < 0)
            {
                throw new System.Exception("Forbidden value (" + objectUID + ") on element objectUID.");
            }

            writer.WriteVarInt((int)objectUID);
            writer.WriteShort((short)position);
        }
        public override void Deserialize(IDataReader reader)
        {
            objectUID = (int)reader.ReadVarUhInt();
            if (objectUID < 0)
            {
                throw new System.Exception("Forbidden value (" + objectUID + ") on element of ObjectMovementMessage.objectUID.");
            }

            position = (short)reader.ReadShort();
            if (position < 0)
            {
                throw new System.Exception("Forbidden value (" + position + ") on element of ObjectMovementMessage.position.");
            }

        }


    }
}








