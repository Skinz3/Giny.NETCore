using System.Collections.Generic;
using Giny.Core.IO.Interfaces;
using Giny.Protocol;
using Giny.Protocol.Enums;

namespace Giny.Protocol.Types
{ 
    public class TaxCollectorInformations  
    { 
        public const ushort Id = 7513;
        public virtual ushort TypeId => Id;

        public double uniqueId;
        public short firtNameId;
        public short lastNameId;
        public AdditionalTaxCollectorInformations additionalInfos;
        public short worldX;
        public short worldY;
        public short subAreaId;
        public byte state;
        public EntityLook look;
        public TaxCollectorComplementaryInformations[] complements;

        public TaxCollectorInformations()
        {
        }
        public TaxCollectorInformations(double uniqueId,short firtNameId,short lastNameId,AdditionalTaxCollectorInformations additionalInfos,short worldX,short worldY,short subAreaId,byte state,EntityLook look,TaxCollectorComplementaryInformations[] complements)
        {
            this.uniqueId = uniqueId;
            this.firtNameId = firtNameId;
            this.lastNameId = lastNameId;
            this.additionalInfos = additionalInfos;
            this.worldX = worldX;
            this.worldY = worldY;
            this.subAreaId = subAreaId;
            this.state = state;
            this.look = look;
            this.complements = complements;
        }
        public virtual void Serialize(IDataWriter writer)
        {
            if (uniqueId < 0 || uniqueId > 9.00719925474099E+15)
            {
                throw new System.Exception("Forbidden value (" + uniqueId + ") on element uniqueId.");
            }

            writer.WriteDouble((double)uniqueId);
            if (firtNameId < 0)
            {
                throw new System.Exception("Forbidden value (" + firtNameId + ") on element firtNameId.");
            }

            writer.WriteVarShort((short)firtNameId);
            if (lastNameId < 0)
            {
                throw new System.Exception("Forbidden value (" + lastNameId + ") on element lastNameId.");
            }

            writer.WriteVarShort((short)lastNameId);
            additionalInfos.Serialize(writer);
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
            if (subAreaId < 0)
            {
                throw new System.Exception("Forbidden value (" + subAreaId + ") on element subAreaId.");
            }

            writer.WriteVarShort((short)subAreaId);
            writer.WriteByte((byte)state);
            look.Serialize(writer);
            writer.WriteShort((short)complements.Length);
            for (uint _i10 = 0;_i10 < complements.Length;_i10++)
            {
                writer.WriteShort((short)(complements[_i10] as TaxCollectorComplementaryInformations).TypeId);
                (complements[_i10] as TaxCollectorComplementaryInformations).Serialize(writer);
            }

        }
        public virtual void Deserialize(IDataReader reader)
        {
            uint _id10 = 0;
            TaxCollectorComplementaryInformations _item10 = null;
            uniqueId = (double)reader.ReadDouble();
            if (uniqueId < 0 || uniqueId > 9.00719925474099E+15)
            {
                throw new System.Exception("Forbidden value (" + uniqueId + ") on element of TaxCollectorInformations.uniqueId.");
            }

            firtNameId = (short)reader.ReadVarUhShort();
            if (firtNameId < 0)
            {
                throw new System.Exception("Forbidden value (" + firtNameId + ") on element of TaxCollectorInformations.firtNameId.");
            }

            lastNameId = (short)reader.ReadVarUhShort();
            if (lastNameId < 0)
            {
                throw new System.Exception("Forbidden value (" + lastNameId + ") on element of TaxCollectorInformations.lastNameId.");
            }

            additionalInfos = new AdditionalTaxCollectorInformations();
            additionalInfos.Deserialize(reader);
            worldX = (short)reader.ReadShort();
            if (worldX < -255 || worldX > 255)
            {
                throw new System.Exception("Forbidden value (" + worldX + ") on element of TaxCollectorInformations.worldX.");
            }

            worldY = (short)reader.ReadShort();
            if (worldY < -255 || worldY > 255)
            {
                throw new System.Exception("Forbidden value (" + worldY + ") on element of TaxCollectorInformations.worldY.");
            }

            subAreaId = (short)reader.ReadVarUhShort();
            if (subAreaId < 0)
            {
                throw new System.Exception("Forbidden value (" + subAreaId + ") on element of TaxCollectorInformations.subAreaId.");
            }

            state = (byte)reader.ReadByte();
            if (state < 0)
            {
                throw new System.Exception("Forbidden value (" + state + ") on element of TaxCollectorInformations.state.");
            }

            look = new EntityLook();
            look.Deserialize(reader);
            uint _complementsLen = (uint)reader.ReadUShort();
            for (uint _i10 = 0;_i10 < _complementsLen;_i10++)
            {
                _id10 = (uint)reader.ReadUShort();
                _item10 = ProtocolTypeManager.GetInstance<TaxCollectorComplementaryInformations>((short)_id10);
                _item10.Deserialize(reader);
                complements[_i10] = _item10;
            }

        }


    }
}








