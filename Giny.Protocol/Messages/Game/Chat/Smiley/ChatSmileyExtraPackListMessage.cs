using System.Collections.Generic;
using Giny.Core.Network.Messages;
using Giny.Protocol.Types;
using Giny.Core.IO.Interfaces;
using Giny.Protocol;
using Giny.Protocol.Enums;

namespace Giny.Protocol.Messages
{ 
    public class ChatSmileyExtraPackListMessage : NetworkMessage  
    { 
        public  const ushort Id = 1279;
        public override ushort MessageId => Id;

        public byte[] packIds;

        public ChatSmileyExtraPackListMessage()
        {
        }
        public ChatSmileyExtraPackListMessage(byte[] packIds)
        {
            this.packIds = packIds;
        }
        public override void Serialize(IDataWriter writer)
        {
            writer.WriteShort((short)packIds.Length);
            for (uint _i1 = 0;_i1 < packIds.Length;_i1++)
            {
                if (packIds[_i1] < 0)
                {
                    throw new System.Exception("Forbidden value (" + packIds[_i1] + ") on element 1 (starting at 1) of packIds.");
                }

                writer.WriteByte((byte)packIds[_i1]);
            }

        }
        public override void Deserialize(IDataReader reader)
        {
            uint _val1 = 0;
            uint _packIdsLen = (uint)reader.ReadUShort();
            packIds = new byte[_packIdsLen];
            for (uint _i1 = 0;_i1 < _packIdsLen;_i1++)
            {
                _val1 = (uint)reader.ReadByte();
                if (_val1 < 0)
                {
                    throw new System.Exception("Forbidden value (" + _val1 + ") on elements of packIds.");
                }

                packIds[_i1] = (byte)_val1;
            }

        }


    }
}








