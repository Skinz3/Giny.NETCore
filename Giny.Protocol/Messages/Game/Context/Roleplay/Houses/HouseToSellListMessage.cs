using System.Collections.Generic;
using Giny.Core.Network.Messages;
using Giny.Protocol.Types;
using Giny.Core.IO.Interfaces;
using Giny.Protocol;
using Giny.Protocol.Enums;

namespace Giny.Protocol.Messages
{ 
    public class HouseToSellListMessage : NetworkMessage  
    { 
        public  const ushort Id = 824;
        public override ushort MessageId => Id;

        public short pageIndex;
        public short totalPage;
        public HouseInformationsForSell[] houseList;

        public HouseToSellListMessage()
        {
        }
        public HouseToSellListMessage(short pageIndex,short totalPage,HouseInformationsForSell[] houseList)
        {
            this.pageIndex = pageIndex;
            this.totalPage = totalPage;
            this.houseList = houseList;
        }
        public override void Serialize(IDataWriter writer)
        {
            if (pageIndex < 0)
            {
                throw new System.Exception("Forbidden value (" + pageIndex + ") on element pageIndex.");
            }

            writer.WriteVarShort((short)pageIndex);
            if (totalPage < 0)
            {
                throw new System.Exception("Forbidden value (" + totalPage + ") on element totalPage.");
            }

            writer.WriteVarShort((short)totalPage);
            writer.WriteShort((short)houseList.Length);
            for (uint _i3 = 0;_i3 < houseList.Length;_i3++)
            {
                (houseList[_i3] as HouseInformationsForSell).Serialize(writer);
            }

        }
        public override void Deserialize(IDataReader reader)
        {
            HouseInformationsForSell _item3 = null;
            pageIndex = (short)reader.ReadVarUhShort();
            if (pageIndex < 0)
            {
                throw new System.Exception("Forbidden value (" + pageIndex + ") on element of HouseToSellListMessage.pageIndex.");
            }

            totalPage = (short)reader.ReadVarUhShort();
            if (totalPage < 0)
            {
                throw new System.Exception("Forbidden value (" + totalPage + ") on element of HouseToSellListMessage.totalPage.");
            }

            uint _houseListLen = (uint)reader.ReadUShort();
            for (uint _i3 = 0;_i3 < _houseListLen;_i3++)
            {
                _item3 = new HouseInformationsForSell();
                _item3.Deserialize(reader);
                houseList[_i3] = _item3;
            }

        }


    }
}








