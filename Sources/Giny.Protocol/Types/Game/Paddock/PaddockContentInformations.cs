using System.Collections.Generic;
using Giny.Core.IO.Interfaces;
using Giny.Protocol;
using Giny.Protocol.Enums;

namespace Giny.Protocol.Types
{ 
    public class PaddockContentInformations : PaddockInformations  
    { 
        public new const ushort Id = 6719;
        public override ushort TypeId => Id;

        public double paddockId;
        public short worldX;
        public short worldY;
        public double mapId;
        public short subAreaId;
        public bool abandonned;
        public MountInformationsForPaddock[] mountsInformations;

        public PaddockContentInformations()
        {
        }
        public PaddockContentInformations(double paddockId,short worldX,short worldY,double mapId,short subAreaId,bool abandonned,MountInformationsForPaddock[] mountsInformations,short maxOutdoorMount,short maxItems)
        {
            this.paddockId = paddockId;
            this.worldX = worldX;
            this.worldY = worldY;
            this.mapId = mapId;
            this.subAreaId = subAreaId;
            this.abandonned = abandonned;
            this.mountsInformations = mountsInformations;
            this.maxOutdoorMount = maxOutdoorMount;
            this.maxItems = maxItems;
        }
        public override void Serialize(IDataWriter writer)
        {
            base.Serialize(writer);
            if (paddockId < 0 || paddockId > 9.00719925474099E+15)
            {
                throw new System.Exception("Forbidden value (" + paddockId + ") on element paddockId.");
            }

            writer.WriteDouble((double)paddockId);
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
            writer.WriteBoolean((bool)abandonned);
            writer.WriteShort((short)mountsInformations.Length);
            for (uint _i7 = 0;_i7 < mountsInformations.Length;_i7++)
            {
                (mountsInformations[_i7] as MountInformationsForPaddock).Serialize(writer);
            }

        }
        public override void Deserialize(IDataReader reader)
        {
            MountInformationsForPaddock _item7 = null;
            base.Deserialize(reader);
            paddockId = (double)reader.ReadDouble();
            if (paddockId < 0 || paddockId > 9.00719925474099E+15)
            {
                throw new System.Exception("Forbidden value (" + paddockId + ") on element of PaddockContentInformations.paddockId.");
            }

            worldX = (short)reader.ReadShort();
            if (worldX < -255 || worldX > 255)
            {
                throw new System.Exception("Forbidden value (" + worldX + ") on element of PaddockContentInformations.worldX.");
            }

            worldY = (short)reader.ReadShort();
            if (worldY < -255 || worldY > 255)
            {
                throw new System.Exception("Forbidden value (" + worldY + ") on element of PaddockContentInformations.worldY.");
            }

            mapId = (double)reader.ReadDouble();
            if (mapId < 0 || mapId > 9.00719925474099E+15)
            {
                throw new System.Exception("Forbidden value (" + mapId + ") on element of PaddockContentInformations.mapId.");
            }

            subAreaId = (short)reader.ReadVarUhShort();
            if (subAreaId < 0)
            {
                throw new System.Exception("Forbidden value (" + subAreaId + ") on element of PaddockContentInformations.subAreaId.");
            }

            abandonned = (bool)reader.ReadBoolean();
            uint _mountsInformationsLen = (uint)reader.ReadUShort();
            for (uint _i7 = 0;_i7 < _mountsInformationsLen;_i7++)
            {
                _item7 = new MountInformationsForPaddock();
                _item7.Deserialize(reader);
                mountsInformations[_i7] = _item7;
            }

        }


    }
}








