using System.Collections.Generic;
using Giny.Core.Network.Messages;
using Giny.Protocol.Types;
using Giny.Core.IO.Interfaces;
using Giny.Protocol;
using Giny.Protocol.Enums;

namespace Giny.Protocol.Messages
{ 
    public class PresetUseResultWithMissingIdsMessage : PresetUseResultMessage  
    { 
        public new const ushort Id = 4515;
        public override ushort MessageId => Id;

        public short[] missingIds;

        public PresetUseResultWithMissingIdsMessage()
        {
        }
        public PresetUseResultWithMissingIdsMessage(short[] missingIds,short presetId,byte code)
        {
            this.missingIds = missingIds;
            this.presetId = presetId;
            this.code = code;
        }
        public override void Serialize(IDataWriter writer)
        {
            base.Serialize(writer);
            writer.WriteShort((short)missingIds.Length);
            for (uint _i1 = 0;_i1 < missingIds.Length;_i1++)
            {
                if (missingIds[_i1] < 0)
                {
                    throw new System.Exception("Forbidden value (" + missingIds[_i1] + ") on element 1 (starting at 1) of missingIds.");
                }

                writer.WriteVarShort((short)missingIds[_i1]);
            }

        }
        public override void Deserialize(IDataReader reader)
        {
            uint _val1 = 0;
            base.Deserialize(reader);
            uint _missingIdsLen = (uint)reader.ReadUShort();
            missingIds = new short[_missingIdsLen];
            for (uint _i1 = 0;_i1 < _missingIdsLen;_i1++)
            {
                _val1 = (uint)reader.ReadVarUhShort();
                if (_val1 < 0)
                {
                    throw new System.Exception("Forbidden value (" + _val1 + ") on elements of missingIds.");
                }

                missingIds[_i1] = (short)_val1;
            }

        }


    }
}








