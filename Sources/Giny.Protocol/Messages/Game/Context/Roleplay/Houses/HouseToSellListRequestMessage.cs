using System.Collections.Generic;
using Giny.Core.Network.Messages;
using Giny.Protocol.Types;
using Giny.Core.IO.Interfaces;
using Giny.Protocol;
using Giny.Protocol.Enums;

namespace Giny.Protocol.Messages
{ 
    public class HouseToSellListRequestMessage : NetworkMessage  
    { 
        public  const ushort Id = 2476;
        public override ushort MessageId => Id;

        public short pageIndex;

        public HouseToSellListRequestMessage()
        {
        }
        public HouseToSellListRequestMessage(short pageIndex)
        {
            this.pageIndex = pageIndex;
        }
        public override void Serialize(IDataWriter writer)
        {
            if (pageIndex < 0)
            {
                throw new System.Exception("Forbidden value (" + pageIndex + ") on element pageIndex.");
            }

            writer.WriteVarShort((short)pageIndex);
        }
        public override void Deserialize(IDataReader reader)
        {
            pageIndex = (short)reader.ReadVarUhShort();
            if (pageIndex < 0)
            {
                throw new System.Exception("Forbidden value (" + pageIndex + ") on element of HouseToSellListRequestMessage.pageIndex.");
            }

        }


    }
}








