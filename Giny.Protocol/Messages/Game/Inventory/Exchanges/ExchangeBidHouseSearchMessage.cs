using System.Collections.Generic;
using Giny.Core.Network.Messages;
using Giny.Protocol.Types;
using Giny.Core.IO.Interfaces;
using Giny.Protocol;
using Giny.Protocol.Enums;

namespace Giny.Protocol.Messages
{ 
    public class ExchangeBidHouseSearchMessage : NetworkMessage  
    { 
        public  const ushort Id = 6174;
        public override ushort MessageId => Id;

        public short genId;
        public bool follow;

        public ExchangeBidHouseSearchMessage()
        {
        }
        public ExchangeBidHouseSearchMessage(short genId,bool follow)
        {
            this.genId = genId;
            this.follow = follow;
        }
        public override void Serialize(IDataWriter writer)
        {
            if (genId < 0)
            {
                throw new System.Exception("Forbidden value (" + genId + ") on element genId.");
            }

            writer.WriteVarShort((short)genId);
            writer.WriteBoolean((bool)follow);
        }
        public override void Deserialize(IDataReader reader)
        {
            genId = (short)reader.ReadVarUhShort();
            if (genId < 0)
            {
                throw new System.Exception("Forbidden value (" + genId + ") on element of ExchangeBidHouseSearchMessage.genId.");
            }

            follow = (bool)reader.ReadBoolean();
        }


    }
}








