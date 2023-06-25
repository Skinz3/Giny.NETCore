using System.Collections.Generic;
using Giny.Core.IO.Interfaces;
using Giny.Protocol;
using Giny.Protocol.Enums;

namespace Giny.Protocol.Types
{ 
    public class AccountHouseInformations : HouseInformations  
    { 
        public new const ushort Id = 588;
        public override ushort TypeId => Id;

        public HouseInstanceInformations houseInfos;
        public short worldX;
        public short worldY;
        public double mapId;
        public short subAreaId;

        public AccountHouseInformations()
        {
        }
        public AccountHouseInformations(HouseInstanceInformations houseInfos,short worldX,short worldY,double mapId,short subAreaId,int houseId,short modelId)
        {
            this.houseInfos = houseInfos;
            this.worldX = worldX;
            this.worldY = worldY;
            this.mapId = mapId;
            this.subAreaId = subAreaId;
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
            if (mapId < 0 || mapId > 9.00719925474099E+15)
            {
                throw new System.Exception("Forbidden value (" + mapId + ") on element mapId.");
            }

            writer.WriteDouble((double)mapId);
            if (subAreaId < 0)
            {
                throw new System.Exception("Forbidden value (" + subAreaId + ") on element subAreaId.");
            }

            writer.WriteVarShort((short)subAreaId);
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
                throw new System.Exception("Forbidden value (" + worldX + ") on element of AccountHouseInformations.worldX.");
            }

            worldY = (short)reader.ReadShort();
            if (worldY < -255 || worldY > 255)
            {
                throw new System.Exception("Forbidden value (" + worldY + ") on element of AccountHouseInformations.worldY.");
            }

            mapId = (double)reader.ReadDouble();
            if (mapId < 0 || mapId > 9.00719925474099E+15)
            {
                throw new System.Exception("Forbidden value (" + mapId + ") on element of AccountHouseInformations.mapId.");
            }

            subAreaId = (short)reader.ReadVarUhShort();
            if (subAreaId < 0)
            {
                throw new System.Exception("Forbidden value (" + subAreaId + ") on element of AccountHouseInformations.subAreaId.");
            }

        }


    }
}








