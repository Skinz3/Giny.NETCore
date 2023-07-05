using System.Collections.Generic;
using Giny.Core.IO.Interfaces;
using Giny.Protocol;
using Giny.Protocol.Enums;

namespace Giny.Protocol.Types
{
    public class PrismGeolocalizedInformation
    {
        public const ushort Id = 4198;
        public virtual ushort TypeId => Id;

        public short subAreaId;
        public int allianceId;
        public short worldX;
        public short worldY;
        public double mapId;
        public PrismInformation prism;

        public PrismGeolocalizedInformation()
        {
        }
        public PrismGeolocalizedInformation(short subAreaId, int allianceId, short worldX, short worldY, double mapId, PrismInformation prism)
        {
            this.subAreaId = subAreaId;
            this.allianceId = allianceId;
            this.worldX = worldX;
            this.worldY = worldY;
            this.mapId = mapId;
            this.prism = prism;
        }
        public virtual void Serialize(IDataWriter writer)
        {
            if (subAreaId < 0)
            {
                throw new System.Exception("Forbidden value (" + subAreaId + ") on element subAreaId.");
            }

            writer.WriteVarShort((short)subAreaId);
            if (allianceId < 0)
            {
                throw new System.Exception("Forbidden value (" + allianceId + ") on element allianceId.");
            }

            writer.WriteVarInt((int)allianceId);
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
            if (mapId < 0 || mapId > 9007199254740992)
            {
                throw new System.Exception("Forbidden value (" + mapId + ") on element mapId.");
            }

            writer.WriteDouble((double)mapId);
            writer.WriteShort((short)prism.TypeId);
            prism.Serialize(writer);
        }
        public virtual void Deserialize(IDataReader reader)
        {
            subAreaId = (short)reader.ReadVarUhShort();
            if (subAreaId < 0)
            {
                throw new System.Exception("Forbidden value (" + subAreaId + ") on element of PrismGeolocalizedInformation.subAreaId.");
            }

            allianceId = (int)reader.ReadVarUhInt();
            if (allianceId < 0)
            {
                throw new System.Exception("Forbidden value (" + allianceId + ") on element of PrismGeolocalizedInformation.allianceId.");
            }

            worldX = (short)reader.ReadShort();
            if (worldX < -255 || worldX > 255)
            {
                throw new System.Exception("Forbidden value (" + worldX + ") on element of PrismGeolocalizedInformation.worldX.");
            }

            worldY = (short)reader.ReadShort();
            if (worldY < -255 || worldY > 255)
            {
                throw new System.Exception("Forbidden value (" + worldY + ") on element of PrismGeolocalizedInformation.worldY.");
            }

            mapId = (double)reader.ReadDouble();
            if (mapId < 0 || mapId > 9007199254740992)
            {
                throw new System.Exception("Forbidden value (" + mapId + ") on element of PrismGeolocalizedInformation.mapId.");
            }

            uint _id6 = (uint)reader.ReadUShort();
            prism = ProtocolTypeManager.GetInstance<PrismInformation>((short)_id6);
            prism.Deserialize(reader);
        }


    }
}


