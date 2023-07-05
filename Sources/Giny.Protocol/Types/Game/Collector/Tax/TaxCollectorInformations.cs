using System.Collections.Generic;
using Giny.Core.IO.Interfaces;
using Giny.Protocol;
using Giny.Protocol.Enums;

namespace Giny.Protocol.Types
{
    public class TaxCollectorInformations
    {
        public const ushort Id = 3218;
        public virtual ushort TypeId => Id;

        public double uniqueId;
        public short firstNameId;
        public short lastNameId;
        public AllianceInformation allianceIdentity;
        public AdditionalTaxCollectorInformation additionalInfos;
        public short worldX;
        public short worldY;
        public short subAreaId;
        public byte state;
        public EntityLook look;
        public TaxCollectorComplementaryInformations[] complements;
        public CharacterCharacteristics characteristics;
        public ObjectItem[] equipments;
        public TaxCollectorOrderedSpell[] spells;

        public TaxCollectorInformations()
        {
        }
        public TaxCollectorInformations(double uniqueId, short firstNameId, short lastNameId, AllianceInformation allianceIdentity, AdditionalTaxCollectorInformation additionalInfos, short worldX, short worldY, short subAreaId, byte state, EntityLook look, TaxCollectorComplementaryInformations[] complements, CharacterCharacteristics characteristics, ObjectItem[] equipments, TaxCollectorOrderedSpell[] spells)
        {
            this.uniqueId = uniqueId;
            this.firstNameId = firstNameId;
            this.lastNameId = lastNameId;
            this.allianceIdentity = allianceIdentity;
            this.additionalInfos = additionalInfos;
            this.worldX = worldX;
            this.worldY = worldY;
            this.subAreaId = subAreaId;
            this.state = state;
            this.look = look;
            this.complements = complements;
            this.characteristics = characteristics;
            this.equipments = equipments;
            this.spells = spells;
        }
        public virtual void Serialize(IDataWriter writer)
        {
            if (uniqueId < 0 || uniqueId > 9007199254740992)
            {
                throw new System.Exception("Forbidden value (" + uniqueId + ") on element uniqueId.");
            }

            writer.WriteDouble((double)uniqueId);
            if (firstNameId < 0)
            {
                throw new System.Exception("Forbidden value (" + firstNameId + ") on element firstNameId.");
            }

            writer.WriteVarShort((short)firstNameId);
            if (lastNameId < 0)
            {
                throw new System.Exception("Forbidden value (" + lastNameId + ") on element lastNameId.");
            }

            writer.WriteVarShort((short)lastNameId);
            allianceIdentity.Serialize(writer);
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
            for (uint _i11 = 0; _i11 < complements.Length; _i11++)
            {
                writer.WriteShort((short)(complements[_i11] as TaxCollectorComplementaryInformations).TypeId);
                (complements[_i11] as TaxCollectorComplementaryInformations).Serialize(writer);
            }

            characteristics.Serialize(writer);
            writer.WriteShort((short)equipments.Length);
            for (uint _i13 = 0; _i13 < equipments.Length; _i13++)
            {
                (equipments[_i13] as ObjectItem).Serialize(writer);
            }

            writer.WriteShort((short)spells.Length);
            for (uint _i14 = 0; _i14 < spells.Length; _i14++)
            {
                (spells[_i14] as TaxCollectorOrderedSpell).Serialize(writer);
            }

        }
        public virtual void Deserialize(IDataReader reader)
        {
            uint _id11 = 0;
            TaxCollectorComplementaryInformations _item11 = null;
            ObjectItem _item13 = null;
            TaxCollectorOrderedSpell _item14 = null;
            uniqueId = (double)reader.ReadDouble();
            if (uniqueId < 0 || uniqueId > 9007199254740992)
            {
                throw new System.Exception("Forbidden value (" + uniqueId + ") on element of TaxCollectorInformations.uniqueId.");
            }

            firstNameId = (short)reader.ReadVarUhShort();
            if (firstNameId < 0)
            {
                throw new System.Exception("Forbidden value (" + firstNameId + ") on element of TaxCollectorInformations.firstNameId.");
            }

            lastNameId = (short)reader.ReadVarUhShort();
            if (lastNameId < 0)
            {
                throw new System.Exception("Forbidden value (" + lastNameId + ") on element of TaxCollectorInformations.lastNameId.");
            }

            allianceIdentity = new AllianceInformation();
            allianceIdentity.Deserialize(reader);
            additionalInfos = new AdditionalTaxCollectorInformation();
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
            for (uint _i11 = 0; _i11 < _complementsLen; _i11++)
            {
                _id11 = (uint)reader.ReadUShort();
                _item11 = ProtocolTypeManager.GetInstance<TaxCollectorComplementaryInformations>((short)_id11);
                _item11.Deserialize(reader);
                complements[_i11] = _item11;
            }

            characteristics = new CharacterCharacteristics();
            characteristics.Deserialize(reader);
            uint _equipmentsLen = (uint)reader.ReadUShort();
            for (uint _i13 = 0; _i13 < _equipmentsLen; _i13++)
            {
                _item13 = new ObjectItem();
                _item13.Deserialize(reader);
                equipments[_i13] = _item13;
            }

            uint _spellsLen = (uint)reader.ReadUShort();
            for (uint _i14 = 0; _i14 < _spellsLen; _i14++)
            {
                _item14 = new TaxCollectorOrderedSpell();
                _item14.Deserialize(reader);
                spells[_i14] = _item14;
            }

        }


    }
}


