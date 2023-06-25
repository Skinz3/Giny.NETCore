using System.Collections.Generic;
using Giny.Core.IO.Interfaces;
using Giny.Protocol;
using Giny.Protocol.Enums;

namespace Giny.Protocol.Types
{ 
    public class PartyMemberGeoPosition  
    { 
        public const ushort Id = 6539;
        public virtual ushort TypeId => Id;

        public int memberId;
        public short worldX;
        public short worldY;
        public double mapId;
        public short subAreaId;

        public PartyMemberGeoPosition()
        {
        }
        public PartyMemberGeoPosition(int memberId,short worldX,short worldY,double mapId,short subAreaId)
        {
            this.memberId = memberId;
            this.worldX = worldX;
            this.worldY = worldY;
            this.mapId = mapId;
            this.subAreaId = subAreaId;
        }
        public virtual void Serialize(IDataWriter writer)
        {
            if (memberId < 0)
            {
                throw new System.Exception("Forbidden value (" + memberId + ") on element memberId.");
            }

            writer.WriteInt((int)memberId);
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
        public virtual void Deserialize(IDataReader reader)
        {
            memberId = (int)reader.ReadInt();
            if (memberId < 0)
            {
                throw new System.Exception("Forbidden value (" + memberId + ") on element of PartyMemberGeoPosition.memberId.");
            }

            worldX = (short)reader.ReadShort();
            if (worldX < -255 || worldX > 255)
            {
                throw new System.Exception("Forbidden value (" + worldX + ") on element of PartyMemberGeoPosition.worldX.");
            }

            worldY = (short)reader.ReadShort();
            if (worldY < -255 || worldY > 255)
            {
                throw new System.Exception("Forbidden value (" + worldY + ") on element of PartyMemberGeoPosition.worldY.");
            }

            mapId = (double)reader.ReadDouble();
            if (mapId < 0 || mapId > 9.00719925474099E+15)
            {
                throw new System.Exception("Forbidden value (" + mapId + ") on element of PartyMemberGeoPosition.mapId.");
            }

            subAreaId = (short)reader.ReadVarUhShort();
            if (subAreaId < 0)
            {
                throw new System.Exception("Forbidden value (" + subAreaId + ") on element of PartyMemberGeoPosition.subAreaId.");
            }

        }


    }
}








