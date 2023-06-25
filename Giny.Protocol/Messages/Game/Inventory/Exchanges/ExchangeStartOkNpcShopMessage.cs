using System.Collections.Generic;
using Giny.Core.Network.Messages;
using Giny.Protocol.Types;
using Giny.Core.IO.Interfaces;
using Giny.Protocol;
using Giny.Protocol.Enums;

namespace Giny.Protocol.Messages
{ 
    public class ExchangeStartOkNpcShopMessage : NetworkMessage  
    { 
        public  const ushort Id = 3711;
        public override ushort MessageId => Id;

        public double npcSellerId;
        public short tokenId;
        public ObjectItemToSellInNpcShop[] objectsInfos;

        public ExchangeStartOkNpcShopMessage()
        {
        }
        public ExchangeStartOkNpcShopMessage(double npcSellerId,short tokenId,ObjectItemToSellInNpcShop[] objectsInfos)
        {
            this.npcSellerId = npcSellerId;
            this.tokenId = tokenId;
            this.objectsInfos = objectsInfos;
        }
        public override void Serialize(IDataWriter writer)
        {
            if (npcSellerId < -9.00719925474099E+15 || npcSellerId > 9.00719925474099E+15)
            {
                throw new System.Exception("Forbidden value (" + npcSellerId + ") on element npcSellerId.");
            }

            writer.WriteDouble((double)npcSellerId);
            if (tokenId < 0)
            {
                throw new System.Exception("Forbidden value (" + tokenId + ") on element tokenId.");
            }

            writer.WriteVarShort((short)tokenId);
            writer.WriteShort((short)objectsInfos.Length);
            for (uint _i3 = 0;_i3 < objectsInfos.Length;_i3++)
            {
                (objectsInfos[_i3] as ObjectItemToSellInNpcShop).Serialize(writer);
            }

        }
        public override void Deserialize(IDataReader reader)
        {
            ObjectItemToSellInNpcShop _item3 = null;
            npcSellerId = (double)reader.ReadDouble();
            if (npcSellerId < -9.00719925474099E+15 || npcSellerId > 9.00719925474099E+15)
            {
                throw new System.Exception("Forbidden value (" + npcSellerId + ") on element of ExchangeStartOkNpcShopMessage.npcSellerId.");
            }

            tokenId = (short)reader.ReadVarUhShort();
            if (tokenId < 0)
            {
                throw new System.Exception("Forbidden value (" + tokenId + ") on element of ExchangeStartOkNpcShopMessage.tokenId.");
            }

            uint _objectsInfosLen = (uint)reader.ReadUShort();
            for (uint _i3 = 0;_i3 < _objectsInfosLen;_i3++)
            {
                _item3 = new ObjectItemToSellInNpcShop();
                _item3.Deserialize(reader);
                objectsInfos[_i3] = _item3;
            }

        }


    }
}








