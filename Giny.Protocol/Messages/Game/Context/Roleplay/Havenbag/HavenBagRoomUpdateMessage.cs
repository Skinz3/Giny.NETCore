using System.Collections.Generic;
using Giny.Core.Network.Messages;
using Giny.Protocol.Types;
using Giny.Core.IO.Interfaces;
using Giny.Protocol;
using Giny.Protocol.Enums;

namespace Giny.Protocol.Messages
{ 
    public class HavenBagRoomUpdateMessage : NetworkMessage  
    { 
        public  const ushort Id = 5991;
        public override ushort MessageId => Id;

        public byte action;
        public HavenBagRoomPreviewInformation[] roomsPreview;

        public HavenBagRoomUpdateMessage()
        {
        }
        public HavenBagRoomUpdateMessage(byte action,HavenBagRoomPreviewInformation[] roomsPreview)
        {
            this.action = action;
            this.roomsPreview = roomsPreview;
        }
        public override void Serialize(IDataWriter writer)
        {
            writer.WriteByte((byte)action);
            writer.WriteShort((short)roomsPreview.Length);
            for (uint _i2 = 0;_i2 < roomsPreview.Length;_i2++)
            {
                (roomsPreview[_i2] as HavenBagRoomPreviewInformation).Serialize(writer);
            }

        }
        public override void Deserialize(IDataReader reader)
        {
            HavenBagRoomPreviewInformation _item2 = null;
            action = (byte)reader.ReadByte();
            if (action < 0)
            {
                throw new System.Exception("Forbidden value (" + action + ") on element of HavenBagRoomUpdateMessage.action.");
            }

            uint _roomsPreviewLen = (uint)reader.ReadUShort();
            for (uint _i2 = 0;_i2 < _roomsPreviewLen;_i2++)
            {
                _item2 = new HavenBagRoomPreviewInformation();
                _item2.Deserialize(reader);
                roomsPreview[_i2] = _item2;
            }

        }


    }
}








