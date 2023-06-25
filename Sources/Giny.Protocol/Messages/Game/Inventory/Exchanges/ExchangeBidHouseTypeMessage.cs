using System.Collections.Generic;
using Giny.Core.Network.Messages;
using Giny.Protocol.Types;
using Giny.Core.IO.Interfaces;
using Giny.Protocol;
using Giny.Protocol.Enums;

namespace Giny.Protocol.Messages
{ 
    public class ExchangeBidHouseTypeMessage : NetworkMessage  
    { 
        public  const ushort Id = 690;
        public override ushort MessageId => Id;

        public int type;
        public bool follow;

        public ExchangeBidHouseTypeMessage()
        {
        }
        public ExchangeBidHouseTypeMessage(int type,bool follow)
        {
            this.type = type;
            this.follow = follow;
        }
        public override void Serialize(IDataWriter writer)
        {
            if (type < 0)
            {
                throw new System.Exception("Forbidden value (" + type + ") on element type.");
            }

            writer.WriteVarInt((int)type);
            writer.WriteBoolean((bool)follow);
        }
        public override void Deserialize(IDataReader reader)
        {
            type = (int)reader.ReadVarUhInt();
            if (type < 0)
            {
                throw new System.Exception("Forbidden value (" + type + ") on element of ExchangeBidHouseTypeMessage.type.");
            }

            follow = (bool)reader.ReadBoolean();
        }


    }
}








