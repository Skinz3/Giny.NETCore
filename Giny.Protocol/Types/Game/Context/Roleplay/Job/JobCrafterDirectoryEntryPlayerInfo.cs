using System.Collections.Generic;
using Giny.Core.IO.Interfaces;
using Giny.Protocol;
using Giny.Protocol.Enums;

namespace Giny.Protocol.Types
{ 
    public class JobCrafterDirectoryEntryPlayerInfo  
    { 
        public const ushort Id = 6128;
        public virtual ushort TypeId => Id;

        public long playerId;
        public string playerName;
        public byte alignmentSide;
        public byte breed;
        public bool sex;
        public bool isInWorkshop;
        public short worldX;
        public short worldY;
        public double mapId;
        public short subAreaId;
        public bool canCraftLegendary;
        public PlayerStatus status;

        public JobCrafterDirectoryEntryPlayerInfo()
        {
        }
        public JobCrafterDirectoryEntryPlayerInfo(long playerId,string playerName,byte alignmentSide,byte breed,bool sex,bool isInWorkshop,short worldX,short worldY,double mapId,short subAreaId,bool canCraftLegendary,PlayerStatus status)
        {
            this.playerId = playerId;
            this.playerName = playerName;
            this.alignmentSide = alignmentSide;
            this.breed = breed;
            this.sex = sex;
            this.isInWorkshop = isInWorkshop;
            this.worldX = worldX;
            this.worldY = worldY;
            this.mapId = mapId;
            this.subAreaId = subAreaId;
            this.canCraftLegendary = canCraftLegendary;
            this.status = status;
        }
        public virtual void Serialize(IDataWriter writer)
        {
            if (playerId < 0 || playerId > 9.00719925474099E+15)
            {
                throw new System.Exception("Forbidden value (" + playerId + ") on element playerId.");
            }

            writer.WriteVarLong((long)playerId);
            writer.WriteUTF((string)playerName);
            writer.WriteByte((byte)alignmentSide);
            writer.WriteByte((byte)breed);
            writer.WriteBoolean((bool)sex);
            writer.WriteBoolean((bool)isInWorkshop);
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
            writer.WriteBoolean((bool)canCraftLegendary);
            writer.WriteShort((short)status.TypeId);
            status.Serialize(writer);
        }
        public virtual void Deserialize(IDataReader reader)
        {
            playerId = (long)reader.ReadVarUhLong();
            if (playerId < 0 || playerId > 9.00719925474099E+15)
            {
                throw new System.Exception("Forbidden value (" + playerId + ") on element of JobCrafterDirectoryEntryPlayerInfo.playerId.");
            }

            playerName = (string)reader.ReadUTF();
            alignmentSide = (byte)reader.ReadByte();
            breed = (byte)reader.ReadByte();
            if (breed < (byte)PlayableBreedEnum.Feca || breed > (byte)PlayableBreedEnum.Ouginak)
            {
                throw new System.Exception("Forbidden value (" + breed + ") on element of JobCrafterDirectoryEntryPlayerInfo.breed.");
            }

            sex = (bool)reader.ReadBoolean();
            isInWorkshop = (bool)reader.ReadBoolean();
            worldX = (short)reader.ReadShort();
            if (worldX < -255 || worldX > 255)
            {
                throw new System.Exception("Forbidden value (" + worldX + ") on element of JobCrafterDirectoryEntryPlayerInfo.worldX.");
            }

            worldY = (short)reader.ReadShort();
            if (worldY < -255 || worldY > 255)
            {
                throw new System.Exception("Forbidden value (" + worldY + ") on element of JobCrafterDirectoryEntryPlayerInfo.worldY.");
            }

            mapId = (double)reader.ReadDouble();
            if (mapId < 0 || mapId > 9.00719925474099E+15)
            {
                throw new System.Exception("Forbidden value (" + mapId + ") on element of JobCrafterDirectoryEntryPlayerInfo.mapId.");
            }

            subAreaId = (short)reader.ReadVarUhShort();
            if (subAreaId < 0)
            {
                throw new System.Exception("Forbidden value (" + subAreaId + ") on element of JobCrafterDirectoryEntryPlayerInfo.subAreaId.");
            }

            canCraftLegendary = (bool)reader.ReadBoolean();
            uint _id12 = (uint)reader.ReadUShort();
            status = ProtocolTypeManager.GetInstance<PlayerStatus>((short)_id12);
            status.Deserialize(reader);
        }


    }
}








