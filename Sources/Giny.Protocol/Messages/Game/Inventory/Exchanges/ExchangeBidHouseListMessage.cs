using System.Collections.Generic;
using Giny.Core.Network.Messages;
using Giny.Protocol.Types;
using Giny.Core.IO.Interfaces;
using Giny.Protocol;
using Giny.Protocol.Enums;

namespace Giny.Protocol.Messages
{ 
    public class ExchangeBidHouseListMessage : NetworkMessage  
    { 
        public  const ushort Id = 1929;
        public override ushort MessageId => Id;

        public short id;
        public bool follow;

        public ExchangeBidHouseListMessage()
        {
        }
        public ExchangeBidHouseListMessage(short id,bool follow)
        {
            this.id = id;
            this.follow = follow;
        }
        public override void Serialize(IDataWriter writer)
        {
            if (id < 0)
            {
                throw new System.Exception("Forbidden value (" + id + ") on element id.");
            }

            writer.WriteVarShort((short)id);
            writer.WriteBoolean((bool)follow);
        }
        public override void Deserialize(IDataReader reader)
        {
            id = (short)reader.ReadVarUhShort();
            if (id < 0)
            {
                throw new System.Exception("Forbidden value (" + id + ") on element of ExchangeBidHouseListMessage.id.");
            }

            follow = (bool)reader.ReadBoolean();
        }


    }
}








