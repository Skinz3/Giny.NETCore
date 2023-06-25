using System.Collections.Generic;
using Giny.Core.IO.Interfaces;
using Giny.Protocol;
using Giny.Protocol.Enums;

namespace Giny.Protocol.Types
{ 
    public class HouseInformationsInside : HouseInformations  
    { 
        public new const ushort Id = 8706;
        public override ushort TypeId => Id;

        public HouseInstanceInformations houseInfos;
        public short worldX;
        public short worldY;

        public HouseInformationsInside()
        {
        }
        public HouseInformationsInside(HouseInstanceInformations houseInfos,short worldX,short worldY,int houseId,short modelId)
        {
            this.houseInfos = houseInfos;
            this.worldX = worldX;
            this.worldY = worldY;
            this.houseId = houseId;
            this.modelId = modelId;
        }
        public override void Serialize(IDataWriter writer)
        {
            base.Serialize(writer);
            writer.WriteShort((short)houseInfos.TypeId);
            houseInfos.Serialize(writer);
            if (worldX < -255 || worldX > 255)
            {
                throw new System.Exception("Forbidden value (" + worldX + ") on element worldX.");
            }

            writer.WriteShort((short)worldX);
            if (worldY < -255 || worldY > 255)
            {
                throw new System.Exception("Forbidden value (" + worldY + ") on element worldY.");
            }

            writer.WriteShort((short)worldY);
        }
        public override void Deserialize(IDataReader reader)
        {
            base.Deserialize(reader);
            uint _id1 = (uint)reader.ReadUShort();
            houseInfos = ProtocolTypeManager.GetInstance<HouseInstanceInformations>((short)_id1);
            houseInfos.Deserialize(reader);
            worldX = (short)reader.ReadShort();
            if (worldX < -255 || worldX > 255)
            {
                throw new System.Exception("Forbidden value (" + worldX + ") on element of HouseInformationsInside.worldX.");
            }

            worldY = (short)reader.ReadShort();
            if (worldY < -255 || worldY > 255)
            {
                throw new System.Exception("Forbidden value (" + worldY + ") on element of HouseInformationsInside.worldY.");
            }

        }


    }
}








