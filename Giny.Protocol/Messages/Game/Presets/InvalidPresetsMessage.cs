using System.Collections.Generic;
using Giny.Core.Network.Messages;
using Giny.Protocol.Types;
using Giny.Core.IO.Interfaces;
using Giny.Protocol;
using Giny.Protocol.Enums;

namespace Giny.Protocol.Messages
{ 
    public class InvalidPresetsMessage : NetworkMessage  
    { 
        public  const ushort Id = 9244;
        public override ushort MessageId => Id;

        public short[] presetIds;

        public InvalidPresetsMessage()
        {
        }
        public InvalidPresetsMessage(short[] presetIds)
        {
            this.presetIds = presetIds;
        }
        public override void Serialize(IDataWriter writer)
        {
            writer.WriteShort((short)presetIds.Length);
            for (uint _i1 = 0;_i1 < presetIds.Length;_i1++)
            {
                if (presetIds[_i1] < 0)
                {
                    throw new System.Exception("Forbidden value (" + presetIds[_i1] + ") on element 1 (starting at 1) of presetIds.");
                }

                writer.WriteShort((short)presetIds[_i1]);
            }

        }
        public override void Deserialize(IDataReader reader)
        {
            uint _val1 = 0;
            uint _presetIdsLen = (uint)reader.ReadUShort();
            presetIds = new short[_presetIdsLen];
            for (uint _i1 = 0;_i1 < _presetIdsLen;_i1++)
            {
                _val1 = (uint)reader.ReadShort();
                if (_val1 < 0)
                {
                    throw new System.Exception("Forbidden value (" + _val1 + ") on elements of presetIds.");
                }

                presetIds[_i1] = (short)_val1;
            }

        }


    }
}








