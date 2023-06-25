using System.Collections.Generic;
using Giny.Core.Network.Messages;
using Giny.Protocol.Types;
using Giny.Core.IO.Interfaces;
using Giny.Protocol;
using Giny.Protocol.Enums;

namespace Giny.Protocol.Messages
{ 
    public class NotificationListMessage : NetworkMessage  
    { 
        public  const ushort Id = 1912;
        public override ushort MessageId => Id;

        public int[] flags;

        public NotificationListMessage()
        {
        }
        public NotificationListMessage(int[] flags)
        {
            this.flags = flags;
        }
        public override void Serialize(IDataWriter writer)
        {
            writer.WriteShort((short)flags.Length);
            for (uint _i1 = 0;_i1 < flags.Length;_i1++)
            {
                writer.WriteVarInt((int)flags[_i1]);
            }

        }
        public override void Deserialize(IDataReader reader)
        {
            int _val1 = 0;
            uint _flagsLen = (uint)reader.ReadUShort();
            flags = new int[_flagsLen];
            for (uint _i1 = 0;_i1 < _flagsLen;_i1++)
            {
                _val1 = (int)reader.ReadVarInt();
                flags[_i1] = (int)_val1;
            }

        }


    }
}








