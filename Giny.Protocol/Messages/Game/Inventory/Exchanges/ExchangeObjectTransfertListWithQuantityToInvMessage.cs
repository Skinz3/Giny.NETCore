using System.Collections.Generic;
using Giny.Core.Network.Messages;
using Giny.Protocol.Types;
using Giny.Core.IO.Interfaces;
using Giny.Protocol;
using Giny.Protocol.Enums;

namespace Giny.Protocol.Messages
{ 
    public class ExchangeObjectTransfertListWithQuantityToInvMessage : NetworkMessage  
    { 
        public  const ushort Id = 9233;
        public override ushort MessageId => Id;

        public int[] ids;
        public int[] qtys;

        public ExchangeObjectTransfertListWithQuantityToInvMessage()
        {
        }
        public ExchangeObjectTransfertListWithQuantityToInvMessage(int[] ids,int[] qtys)
        {
            this.ids = ids;
            this.qtys = qtys;
        }
        public override void Serialize(IDataWriter writer)
        {
            writer.WriteShort((short)ids.Length);
            for (uint _i1 = 0;_i1 < ids.Length;_i1++)
            {
                if (ids[_i1] < 0)
                {
                    throw new System.Exception("Forbidden value (" + ids[_i1] + ") on element 1 (starting at 1) of ids.");
                }

                writer.WriteVarInt((int)ids[_i1]);
            }

            writer.WriteShort((short)qtys.Length);
            for (uint _i2 = 0;_i2 < qtys.Length;_i2++)
            {
                if (qtys[_i2] < 0)
                {
                    throw new System.Exception("Forbidden value (" + qtys[_i2] + ") on element 2 (starting at 1) of qtys.");
                }

                writer.WriteVarInt((int)qtys[_i2]);
            }

        }
        public override void Deserialize(IDataReader reader)
        {
            uint _val1 = 0;
            uint _val2 = 0;
            uint _idsLen = (uint)reader.ReadUShort();
            ids = new int[_idsLen];
            for (uint _i1 = 0;_i1 < _idsLen;_i1++)
            {
                _val1 = (uint)reader.ReadVarUhInt();
                if (_val1 < 0)
                {
                    throw new System.Exception("Forbidden value (" + _val1 + ") on elements of ids.");
                }

                ids[_i1] = (int)_val1;
            }

            uint _qtysLen = (uint)reader.ReadUShort();
            qtys = new int[_qtysLen];
            for (uint _i2 = 0;_i2 < _qtysLen;_i2++)
            {
                _val2 = (uint)reader.ReadVarUhInt();
                if (_val2 < 0)
                {
                    throw new System.Exception("Forbidden value (" + _val2 + ") on elements of qtys.");
                }

                qtys[_i2] = (int)_val2;
            }

        }


    }
}








