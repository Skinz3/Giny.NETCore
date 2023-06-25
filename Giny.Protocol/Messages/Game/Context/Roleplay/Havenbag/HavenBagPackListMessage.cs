using System.Collections.Generic;
using Giny.Core.Network.Messages;
using Giny.Protocol.Types;
using Giny.Core.IO.Interfaces;
using Giny.Protocol;
using Giny.Protocol.Enums;

namespace Giny.Protocol.Messages
{ 
    public class HavenBagPackListMessage : NetworkMessage  
    { 
        public  const ushort Id = 8638;
        public override ushort MessageId => Id;

        public byte[] packIds;

        public HavenBagPackListMessage()
        {
        }
        public HavenBagPackListMessage(byte[] packIds)
        {
            this.packIds = packIds;
        }
        public override void Serialize(IDataWriter writer)
        {
            writer.WriteShort((short)packIds.Length);
            for (uint _i1 = 0;_i1 < packIds.Length;_i1++)
            {
                writer.WriteByte((byte)packIds[_i1]);
            }

        }
        public override void Deserialize(IDataReader reader)
        {
            int _val1 = 0;
            uint _packIdsLen = (uint)reader.ReadUShort();
            packIds = new byte[_packIdsLen];
            for (uint _i1 = 0;_i1 < _packIdsLen;_i1++)
            {
                _val1 = (int)reader.ReadByte();
                packIds[_i1] = (byte)_val1;
            }

        }


    }
}








