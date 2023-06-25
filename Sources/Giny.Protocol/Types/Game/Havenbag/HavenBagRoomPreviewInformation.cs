using System.Collections.Generic;
using Giny.Core.IO.Interfaces;
using Giny.Protocol;
using Giny.Protocol.Enums;

namespace Giny.Protocol.Types
{ 
    public class HavenBagRoomPreviewInformation  
    { 
        public const ushort Id = 4433;
        public virtual ushort TypeId => Id;

        public byte roomId;
        public byte themeId;

        public HavenBagRoomPreviewInformation()
        {
        }
        public HavenBagRoomPreviewInformation(byte roomId,byte themeId)
        {
            this.roomId = roomId;
            this.themeId = themeId;
        }
        public virtual void Serialize(IDataWriter writer)
        {
            if (roomId < 0 || roomId > 255)
            {
                throw new System.Exception("Forbidden value (" + roomId + ") on element roomId.");
            }

            writer.WriteByte((byte)roomId);
            writer.WriteByte((byte)themeId);
        }
        public virtual void Deserialize(IDataReader reader)
        {
            roomId = (byte)reader.ReadSByte();
            if (roomId < 0 || roomId > 255)
            {
                throw new System.Exception("Forbidden value (" + roomId + ") on element of HavenBagRoomPreviewInformation.roomId.");
            }

            themeId = (byte)reader.ReadByte();
        }


    }
}








