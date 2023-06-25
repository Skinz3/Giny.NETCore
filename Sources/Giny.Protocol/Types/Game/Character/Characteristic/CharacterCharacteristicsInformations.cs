using System.Collections.Generic;
using Giny.Core.IO.Interfaces;
using Giny.Protocol;
using Giny.Protocol.Enums;

namespace Giny.Protocol.Types
{ 
    public class CharacterCharacteristicsInformations  
    { 
        public const ushort Id = 4497;
        public virtual ushort TypeId => Id;

        public long experience;
        public long experienceLevelFloor;
        public long experienceNextLevelFloor;
        public long experienceBonusLimit;
        public long kamas;
        public ActorExtendedAlignmentInformations alignmentInfos;
        public short criticalHitWeapon;
        public CharacterCharacteristic[] characteristics;
        public CharacterSpellModification[] spellModifications;
        public int probationTime;

        public CharacterCharacteristicsInformations()
        {
        }
        public CharacterCharacteristicsInformations(long experience,long experienceLevelFloor,long experienceNextLevelFloor,long experienceBonusLimit,long kamas,ActorExtendedAlignmentInformations alignmentInfos,short criticalHitWeapon,CharacterCharacteristic[] characteristics,CharacterSpellModification[] spellModifications,int probationTime)
        {
            this.experience = experience;
            this.experienceLevelFloor = experienceLevelFloor;
            this.experienceNextLevelFloor = experienceNextLevelFloor;
            this.experienceBonusLimit = experienceBonusLimit;
            this.kamas = kamas;
            this.alignmentInfos = alignmentInfos;
            this.criticalHitWeapon = criticalHitWeapon;
            this.characteristics = characteristics;
            this.spellModifications = spellModifications;
            this.probationTime = probationTime;
        }
        public virtual void Serialize(IDataWriter writer)
        {
            if (experience < 0 || experience > 9.00719925474099E+15)
            {
                throw new System.Exception("Forbidden value (" + experience + ") on element experience.");
            }

            writer.WriteVarLong((long)experience);
            if (experienceLevelFloor < 0 || experienceLevelFloor > 9.00719925474099E+15)
            {
                throw new System.Exception("Forbidden value (" + experienceLevelFloor + ") on element experienceLevelFloor.");
            }

            writer.WriteVarLong((long)experienceLevelFloor);
            if (experienceNextLevelFloor < 0 || experienceNextLevelFloor > 9.00719925474099E+15)
            {
                throw new System.Exception("Forbidden value (" + experienceNextLevelFloor + ") on element experienceNextLevelFloor.");
            }

            writer.WriteVarLong((long)experienceNextLevelFloor);
            if (experienceBonusLimit < 0 || experienceBonusLimit > 9.00719925474099E+15)
            {
                throw new System.Exception("Forbidden value (" + experienceBonusLimit + ") on element experienceBonusLimit.");
            }

            writer.WriteVarLong((long)experienceBonusLimit);
            if (kamas < 0 || kamas > 9.00719925474099E+15)
            {
                throw new System.Exception("Forbidden value (" + kamas + ") on element kamas.");
            }

            writer.WriteVarLong((long)kamas);
            alignmentInfos.Serialize(writer);
            if (criticalHitWeapon < 0)
            {
                throw new System.Exception("Forbidden value (" + criticalHitWeapon + ") on element criticalHitWeapon.");
            }

            writer.WriteVarShort((short)criticalHitWeapon);
            writer.WriteShort((short)characteristics.Length);
            for (uint _i8 = 0;_i8 < characteristics.Length;_i8++)
            {
                writer.WriteShort((short)(characteristics[_i8] as CharacterCharacteristic).TypeId);
                (characteristics[_i8] as CharacterCharacteristic).Serialize(writer);
            }

            writer.WriteShort((short)spellModifications.Length);
            for (uint _i9 = 0;_i9 < spellModifications.Length;_i9++)
            {
                (spellModifications[_i9] as CharacterSpellModification).Serialize(writer);
            }

            if (probationTime < 0)
            {
                throw new System.Exception("Forbidden value (" + probationTime + ") on element probationTime.");
            }

            writer.WriteInt((int)probationTime);
        }
        public virtual void Deserialize(IDataReader reader)
        {
            uint _id8 = 0;
            CharacterCharacteristic _item8 = null;
            CharacterSpellModification _item9 = null;
            experience = (long)reader.ReadVarUhLong();
            if (experience < 0 || experience > 9.00719925474099E+15)
            {
                throw new System.Exception("Forbidden value (" + experience + ") on element of CharacterCharacteristicsInformations.experience.");
            }

            experienceLevelFloor = (long)reader.ReadVarUhLong();
            if (experienceLevelFloor < 0 || experienceLevelFloor > 9.00719925474099E+15)
            {
                throw new System.Exception("Forbidden value (" + experienceLevelFloor + ") on element of CharacterCharacteristicsInformations.experienceLevelFloor.");
            }

            experienceNextLevelFloor = (long)reader.ReadVarUhLong();
            if (experienceNextLevelFloor < 0 || experienceNextLevelFloor > 9.00719925474099E+15)
            {
                throw new System.Exception("Forbidden value (" + experienceNextLevelFloor + ") on element of CharacterCharacteristicsInformations.experienceNextLevelFloor.");
            }

            experienceBonusLimit = (long)reader.ReadVarUhLong();
            if (experienceBonusLimit < 0 || experienceBonusLimit > 9.00719925474099E+15)
            {
                throw new System.Exception("Forbidden value (" + experienceBonusLimit + ") on element of CharacterCharacteristicsInformations.experienceBonusLimit.");
            }

            kamas = (long)reader.ReadVarUhLong();
            if (kamas < 0 || kamas > 9.00719925474099E+15)
            {
                throw new System.Exception("Forbidden value (" + kamas + ") on element of CharacterCharacteristicsInformations.kamas.");
            }

            alignmentInfos = new ActorExtendedAlignmentInformations();
            alignmentInfos.Deserialize(reader);
            criticalHitWeapon = (short)reader.ReadVarUhShort();
            if (criticalHitWeapon < 0)
            {
                throw new System.Exception("Forbidden value (" + criticalHitWeapon + ") on element of CharacterCharacteristicsInformations.criticalHitWeapon.");
            }

            uint _characteristicsLen = (uint)reader.ReadUShort();
            for (uint _i8 = 0;_i8 < _characteristicsLen;_i8++)
            {
                _id8 = (uint)reader.ReadUShort();
                _item8 = ProtocolTypeManager.GetInstance<CharacterCharacteristic>((short)_id8);
                _item8.Deserialize(reader);
                characteristics[_i8] = _item8;
            }

            uint _spellModificationsLen = (uint)reader.ReadUShort();
            for (uint _i9 = 0;_i9 < _spellModificationsLen;_i9++)
            {
                _item9 = new CharacterSpellModification();
                _item9.Deserialize(reader);
                spellModifications[_i9] = _item9;
            }

            probationTime = (int)reader.ReadInt();
            if (probationTime < 0)
            {
                throw new System.Exception("Forbidden value (" + probationTime + ") on element of CharacterCharacteristicsInformations.probationTime.");
            }

        }


    }
}








