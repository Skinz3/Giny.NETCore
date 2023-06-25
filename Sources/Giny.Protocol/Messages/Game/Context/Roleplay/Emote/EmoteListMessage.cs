using System.Collections.Generic;
using Giny.Core.Network.Messages;
using Giny.Protocol.Types;
using Giny.Core.IO.Interfaces;
using Giny.Protocol;
using Giny.Protocol.Enums;

namespace Giny.Protocol.Messages
{ 
    public class EmoteListMessage : NetworkMessage  
    { 
        public  const ushort Id = 1739;
        public override ushort MessageId => Id;

        public short[] emoteIds;

        public EmoteListMessage()
        {
        }
        public EmoteListMessage(short[] emoteIds)
        {
            this.emoteIds = emoteIds;
        }
        public override void Serialize(IDataWriter writer)
        {
            writer.WriteShort((short)emoteIds.Length);
            for (uint _i1 = 0;_i1 < emoteIds.Length;_i1++)
            {
                if (emoteIds[_i1] < 0 || emoteIds[_i1] > 65535)
                {
                    throw new System.Exception("Forbidden value (" + emoteIds[_i1] + ") on element 1 (starting at 1) of emoteIds.");
                }

                writer.WriteShort((short)emoteIds[_i1]);
            }

        }
        public override void Deserialize(IDataReader reader)
        {
            uint _val1 = 0;
            uint _emoteIdsLen = (uint)reader.ReadUShort();
            emoteIds = new short[_emoteIdsLen];
            for (uint _i1 = 0;_i1 < _emoteIdsLen;_i1++)
            {
                _val1 = (uint)reader.ReadUShort();
                if (_val1 < 0 || _val1 > 65535)
                {
                    throw new System.Exception("Forbidden value (" + _val1 + ") on elements of emoteIds.");
                }

                emoteIds[_i1] = (short)_val1;
            }

        }


    }
}








