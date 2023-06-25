using System.Collections.Generic;
using Giny.Core.Network.Messages;
using Giny.Protocol.Types;
using Giny.Core.IO.Interfaces;
using Giny.Protocol;
using Giny.Protocol.Enums;

namespace Giny.Protocol.Messages
{ 
    public class AccessoryPreviewRequestMessage : NetworkMessage  
    { 
        public  const ushort Id = 1584;
        public override ushort MessageId => Id;

        public short[] genericId;

        public AccessoryPreviewRequestMessage()
        {
        }
        public AccessoryPreviewRequestMessage(short[] genericId)
        {
            this.genericId = genericId;
        }
        public override void Serialize(IDataWriter writer)
        {
            writer.WriteShort((short)genericId.Length);
            for (uint _i1 = 0;_i1 < genericId.Length;_i1++)
            {
                if (genericId[_i1] < 0)
                {
                    throw new System.Exception("Forbidden value (" + genericId[_i1] + ") on element 1 (starting at 1) of genericId.");
                }

                writer.WriteVarShort((short)genericId[_i1]);
            }

        }
        public override void Deserialize(IDataReader reader)
        {
            uint _val1 = 0;
            uint _genericIdLen = (uint)reader.ReadUShort();
            genericId = new short[_genericIdLen];
            for (uint _i1 = 0;_i1 < _genericIdLen;_i1++)
            {
                _val1 = (uint)reader.ReadVarUhShort();
                if (_val1 < 0)
                {
                    throw new System.Exception("Forbidden value (" + _val1 + ") on elements of genericId.");
                }

                genericId[_i1] = (short)_val1;
            }

        }


    }
}








