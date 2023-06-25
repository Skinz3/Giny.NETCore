using System.Collections.Generic;
using Giny.Core.Network.Messages;
using Giny.Protocol.Types;
using Giny.Core.IO.Interfaces;
using Giny.Protocol;
using Giny.Protocol.Enums;

namespace Giny.Protocol.Messages
{ 
    public class ExchangeObjectTransfertListToInvMessage : NetworkMessage  
    { 
        public  const ushort Id = 1051;
        public override ushort MessageId => Id;

        public int[] ids;

        public ExchangeObjectTransfertListToInvMessage()
        {
        }
        public ExchangeObjectTransfertListToInvMessage(int[] ids)
        {
            this.ids = ids;
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

        }
        public override void Deserialize(IDataReader reader)
        {
            uint _val1 = 0;
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

        }


    }
}








