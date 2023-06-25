using System.Collections.Generic;
using Giny.Core.Network.Messages;
using Giny.Protocol.Types;
using Giny.Core.IO.Interfaces;
using Giny.Protocol;
using Giny.Protocol.Enums;

namespace Giny.Protocol.Messages
{ 
    public class ObjectFeedMessage : NetworkMessage  
    { 
        public  const ushort Id = 9950;
        public override ushort MessageId => Id;

        public int objectUID;
        public ObjectItemQuantity[] meal;

        public ObjectFeedMessage()
        {
        }
        public ObjectFeedMessage(int objectUID,ObjectItemQuantity[] meal)
        {
            this.objectUID = objectUID;
            this.meal = meal;
        }
        public override void Serialize(IDataWriter writer)
        {
            if (objectUID < 0)
            {
                throw new System.Exception("Forbidden value (" + objectUID + ") on element objectUID.");
            }

            writer.WriteVarInt((int)objectUID);
            writer.WriteShort((short)meal.Length);
            for (uint _i2 = 0;_i2 < meal.Length;_i2++)
            {
                (meal[_i2] as ObjectItemQuantity).Serialize(writer);
            }

        }
        public override void Deserialize(IDataReader reader)
        {
            ObjectItemQuantity _item2 = null;
            objectUID = (int)reader.ReadVarUhInt();
            if (objectUID < 0)
            {
                throw new System.Exception("Forbidden value (" + objectUID + ") on element of ObjectFeedMessage.objectUID.");
            }

            uint _mealLen = (uint)reader.ReadUShort();
            meal = new ObjectItemQuantity[_mealLen];
            for (uint _i2 = 0;_i2 < _mealLen;_i2++)
            {
                _item2 = new ObjectItemQuantity();
                _item2.Deserialize(reader);
                meal[_i2] = _item2;
            }

        }


    }
}








