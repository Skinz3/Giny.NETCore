using System.Collections.Generic;
using Giny.Core.IO.Interfaces;
using Giny.Protocol;
using Giny.Protocol.Enums;

namespace Giny.Protocol.Types
{
    public class SocialRecruitmentInformation
    {
        public const ushort Id = 7344;
        public virtual ushort TypeId => Id;

        public int socialId;
        public byte recruitmentType;
        public string recruitmentTitle;
        public string recruitmentText;
        public int[] selectedLanguages;
        public int[] selectedCriterion;
        public short minLevel;
        public bool minLevelFacultative;
        public bool invalidatedByModeration;
        public string lastEditPlayerName;
        public double lastEditDate;
        public bool recruitmentAutoLocked;

        public SocialRecruitmentInformation()
        {
        }
        public SocialRecruitmentInformation(int socialId, byte recruitmentType, string recruitmentTitle, string recruitmentText, int[] selectedLanguages, int[] selectedCriterion, short minLevel, bool minLevelFacultative, bool invalidatedByModeration, string lastEditPlayerName, double lastEditDate, bool recruitmentAutoLocked)
        {
            this.socialId = socialId;
            this.recruitmentType = recruitmentType;
            this.recruitmentTitle = recruitmentTitle;
            this.recruitmentText = recruitmentText;
            this.selectedLanguages = selectedLanguages;
            this.selectedCriterion = selectedCriterion;
            this.minLevel = minLevel;
            this.minLevelFacultative = minLevelFacultative;
            this.invalidatedByModeration = invalidatedByModeration;
            this.lastEditPlayerName = lastEditPlayerName;
            this.lastEditDate = lastEditDate;
            this.recruitmentAutoLocked = recruitmentAutoLocked;
        }
        public virtual void Serialize(IDataWriter writer)
        {
            byte _box0 = 0;
            _box0 = BooleanByteWrapper.SetFlag(_box0, 0, minLevelFacultative);
            _box0 = BooleanByteWrapper.SetFlag(_box0, 1, invalidatedByModeration);
            _box0 = BooleanByteWrapper.SetFlag(_box0, 2, recruitmentAutoLocked);
            writer.WriteByte((byte)_box0);
            if (socialId < 0)
            {
                throw new System.Exception("Forbidden value (" + socialId + ") on element socialId.");
            }

            writer.WriteVarInt((int)socialId);
            writer.WriteByte((byte)recruitmentType);
            writer.WriteUTF((string)recruitmentTitle);
            writer.WriteUTF((string)recruitmentText);
            writer.WriteShort((short)selectedLanguages.Length);
            for (uint _i5 = 0; _i5 < selectedLanguages.Length; _i5++)
            {
                if (selectedLanguages[_i5] < 0)
                {
                    throw new System.Exception("Forbidden value (" + selectedLanguages[_i5] + ") on element 5 (starting at 1) of selectedLanguages.");
                }

                writer.WriteVarInt((int)selectedLanguages[_i5]);
            }

            writer.WriteShort((short)selectedCriterion.Length);
            for (uint _i6 = 0; _i6 < selectedCriterion.Length; _i6++)
            {
                if (selectedCriterion[_i6] < 0)
                {
                    throw new System.Exception("Forbidden value (" + selectedCriterion[_i6] + ") on element 6 (starting at 1) of selectedCriterion.");
                }

                writer.WriteVarInt((int)selectedCriterion[_i6]);
            }

            if (minLevel < 0)
            {
                throw new System.Exception("Forbidden value (" + minLevel + ") on element minLevel.");
            }

            writer.WriteShort((short)minLevel);
            writer.WriteUTF((string)lastEditPlayerName);
            if (lastEditDate < -9007199254740992 || lastEditDate > 9007199254740992)
            {
                throw new System.Exception("Forbidden value (" + lastEditDate + ") on element lastEditDate.");
            }

            writer.WriteDouble((double)lastEditDate);
        }
        public virtual void Deserialize(IDataReader reader)
        {
            uint _val5 = 0;
            uint _val6 = 0;
            byte _box0 = reader.ReadByte();
            minLevelFacultative = BooleanByteWrapper.GetFlag(_box0, 0);
            invalidatedByModeration = BooleanByteWrapper.GetFlag(_box0, 1);
            recruitmentAutoLocked = BooleanByteWrapper.GetFlag(_box0, 2);
            socialId = (int)reader.ReadVarUhInt();
            if (socialId < 0)
            {
                throw new System.Exception("Forbidden value (" + socialId + ") on element of SocialRecruitmentInformation.socialId.");
            }

            recruitmentType = (byte)reader.ReadByte();
            if (recruitmentType < 0)
            {
                throw new System.Exception("Forbidden value (" + recruitmentType + ") on element of SocialRecruitmentInformation.recruitmentType.");
            }

            recruitmentTitle = (string)reader.ReadUTF();
            recruitmentText = (string)reader.ReadUTF();
            uint _selectedLanguagesLen = (uint)reader.ReadUShort();
            selectedLanguages = new int[_selectedLanguagesLen];
            for (uint _i5 = 0; _i5 < _selectedLanguagesLen; _i5++)
            {
                _val5 = (uint)reader.ReadVarUhInt();
                if (_val5 < 0)
                {
                    throw new System.Exception("Forbidden value (" + _val5 + ") on elements of selectedLanguages.");
                }

                selectedLanguages[_i5] = (int)_val5;
            }

            uint _selectedCriterionLen = (uint)reader.ReadUShort();
            selectedCriterion = new int[_selectedCriterionLen];
            for (uint _i6 = 0; _i6 < _selectedCriterionLen; _i6++)
            {
                _val6 = (uint)reader.ReadVarUhInt();
                if (_val6 < 0)
                {
                    throw new System.Exception("Forbidden value (" + _val6 + ") on elements of selectedCriterion.");
                }

                selectedCriterion[_i6] = (int)_val6;
            }

            minLevel = (short)reader.ReadShort();
            if (minLevel < 0)
            {
                throw new System.Exception("Forbidden value (" + minLevel + ") on element of SocialRecruitmentInformation.minLevel.");
            }

            lastEditPlayerName = (string)reader.ReadUTF();
            lastEditDate = (double)reader.ReadDouble();
            if (lastEditDate < -9007199254740992 || lastEditDate > 9007199254740992)
            {
                throw new System.Exception("Forbidden value (" + lastEditDate + ") on element of SocialRecruitmentInformation.lastEditDate.");
            }

        }


    }
}


