using System.Collections.Generic;
using Giny.Core.Network.Messages;
using Giny.Protocol.Types;
using Giny.Core.IO.Interfaces;
using Giny.Protocol;
using Giny.Protocol.Enums;

namespace Giny.Protocol.Messages
{ 
    public class PaddockToSellListMessage : NetworkMessage  
    { 
        public  const ushort Id = 7367;
        public override ushort MessageId => Id;

        public short pageIndex;
        public short totalPage;
        public PaddockInformationsForSell[] paddockList;

        public PaddockToSellListMessage()
        {
        }
        public PaddockToSellListMessage(short pageIndex,short totalPage,PaddockInformationsForSell[] paddockList)
        {
            this.pageIndex = pageIndex;
            this.totalPage = totalPage;
            this.paddockList = paddockList;
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
            writer.WriteShort((short)paddockList.Length);
            for (uint _i3 = 0;_i3 < paddockList.Length;_i3++)
            {
                (paddockList[_i3] as PaddockInformationsForSell).Serialize(writer);
            }

        }
        public override void Deserialize(IDataReader reader)
        {
            PaddockInformationsForSell _item3 = null;
            pageIndex = (short)reader.ReadVarUhShort();
            if (pageIndex < 0)
            {
                throw new System.Exception("Forbidden value (" + pageIndex + ") on element of PaddockToSellListMessage.pageIndex.");
            }

            totalPage = (short)reader.ReadVarUhShort();
            if (totalPage < 0)
            {
                throw new System.Exception("Forbidden value (" + totalPage + ") on element of PaddockToSellListMessage.totalPage.");
            }

            uint _paddockListLen = (uint)reader.ReadUShort();
            for (uint _i3 = 0;_i3 < _paddockListLen;_i3++)
            {
                _item3 = new PaddockInformationsForSell();
                _item3.Deserialize(reader);
                paddockList[_i3] = _item3;
            }

        }


    }
}








