using System.Collections.Generic;
using Giny.Core.Network.Messages;
using Giny.Protocol.Types;
using Giny.Core.IO.Interfaces;
using Giny.Protocol;
using Giny.Protocol.Enums;

namespace Giny.Protocol.Messages
{
    public class AllianceSummaryRequestMessage : PaginationRequestAbstractMessage
    {
        public new const ushort Id = 8794;
        public override ushort MessageId => Id;

        public int filterType;
        public string textFilter;
        public bool hideFullFilter;
        public bool followingAllianceCriteria;
        public int[] criterionFilter;
        public byte sortType;
        public bool sortDescending;
        public int[] languagesFilter;
        public byte[] recruitmentTypeFilter;
        public short minPlayerLevelFilter;
        public short maxPlayerLevelFilter;

        public AllianceSummaryRequestMessage()
        {
        }
        public AllianceSummaryRequestMessage(int filterType, string textFilter, bool hideFullFilter, bool followingAllianceCriteria, int[] criterionFilter, byte sortType, bool sortDescending, int[] languagesFilter, byte[] recruitmentTypeFilter, short minPlayerLevelFilter, short maxPlayerLevelFilter, double offset, uint count)
        {
            this.filterType = filterType;
            this.textFilter = textFilter;
            this.hideFullFilter = hideFullFilter;
            this.followingAllianceCriteria = followingAllianceCriteria;
            this.criterionFilter = criterionFilter;
            this.sortType = sortType;
            this.sortDescending = sortDescending;
            this.languagesFilter = languagesFilter;
            this.recruitmentTypeFilter = recruitmentTypeFilter;
            this.minPlayerLevelFilter = minPlayerLevelFilter;
            this.maxPlayerLevelFilter = maxPlayerLevelFilter;
            this.offset = offset;
            this.count = count;
        }
        public override void Serialize(IDataWriter writer)
        {
            base.Serialize(writer);
            byte _box0 = 0;
            _box0 = BooleanByteWrapper.SetFlag(_box0, 0, hideFullFilter);
            _box0 = BooleanByteWrapper.SetFlag(_box0, 1, followingAllianceCriteria);
            _box0 = BooleanByteWrapper.SetFlag(_box0, 2, sortDescending);
            writer.WriteByte((byte)_box0);
            writer.WriteInt((int)filterType);
            writer.WriteUTF((string)textFilter);
            writer.WriteShort((short)criterionFilter.Length);
            for (uint _i5 = 0; _i5 < criterionFilter.Length; _i5++)
            {
                if (criterionFilter[_i5] < 0)
                {
                    throw new System.Exception("Forbidden value (" + criterionFilter[_i5] + ") on element 5 (starting at 1) of criterionFilter.");
                }

                writer.WriteVarInt((int)criterionFilter[_i5]);
            }

            writer.WriteByte((byte)sortType);
            writer.WriteShort((short)languagesFilter.Length);
            for (uint _i8 = 0; _i8 < languagesFilter.Length; _i8++)
            {
                if (languagesFilter[_i8] < 0)
                {
                    throw new System.Exception("Forbidden value (" + languagesFilter[_i8] + ") on element 8 (starting at 1) of languagesFilter.");
                }

                writer.WriteVarInt((int)languagesFilter[_i8]);
            }

            writer.WriteShort((short)recruitmentTypeFilter.Length);
            for (uint _i9 = 0; _i9 < recruitmentTypeFilter.Length; _i9++)
            {
                writer.WriteByte((byte)recruitmentTypeFilter[_i9]);
            }

            if (minPlayerLevelFilter < 0)
            {
                throw new System.Exception("Forbidden value (" + minPlayerLevelFilter + ") on element minPlayerLevelFilter.");
            }

            writer.WriteShort((short)minPlayerLevelFilter);
            if (maxPlayerLevelFilter < 0)
            {
                throw new System.Exception("Forbidden value (" + maxPlayerLevelFilter + ") on element maxPlayerLevelFilter.");
            }

            writer.WriteShort((short)maxPlayerLevelFilter);
        }
        public override void Deserialize(IDataReader reader)
        {
            uint _val5 = 0;
            uint _val8 = 0;
            uint _val9 = 0;
            base.Deserialize(reader);
            byte _box0 = reader.ReadByte();
            hideFullFilter = BooleanByteWrapper.GetFlag(_box0, 0);
            followingAllianceCriteria = BooleanByteWrapper.GetFlag(_box0, 1);
            sortDescending = BooleanByteWrapper.GetFlag(_box0, 2);
            filterType = (int)reader.ReadInt();
            textFilter = (string)reader.ReadUTF();
            uint _criterionFilterLen = (uint)reader.ReadUShort();
            criterionFilter = new int[_criterionFilterLen];
            for (uint _i5 = 0; _i5 < _criterionFilterLen; _i5++)
            {
                _val5 = (uint)reader.ReadVarUhInt();
                if (_val5 < 0)
                {
                    throw new System.Exception("Forbidden value (" + _val5 + ") on elements of criterionFilter.");
                }

                criterionFilter[_i5] = (int)_val5;
            }

            sortType = (byte)reader.ReadByte();
            if (sortType < 0)
            {
                throw new System.Exception("Forbidden value (" + sortType + ") on element of AllianceSummaryRequestMessage.sortType.");
            }

            uint _languagesFilterLen = (uint)reader.ReadUShort();
            languagesFilter = new int[_languagesFilterLen];
            for (uint _i8 = 0; _i8 < _languagesFilterLen; _i8++)
            {
                _val8 = (uint)reader.ReadVarUhInt();
                if (_val8 < 0)
                {
                    throw new System.Exception("Forbidden value (" + _val8 + ") on elements of languagesFilter.");
                }

                languagesFilter[_i8] = (int)_val8;
            }

            uint _recruitmentTypeFilterLen = (uint)reader.ReadUShort();
            recruitmentTypeFilter = new byte[_recruitmentTypeFilterLen];
            for (uint _i9 = 0; _i9 < _recruitmentTypeFilterLen; _i9++)
            {
                _val9 = (uint)reader.ReadByte();
                if (_val9 < 0)
                {
                    throw new System.Exception("Forbidden value (" + _val9 + ") on elements of recruitmentTypeFilter.");
                }

                recruitmentTypeFilter[_i9] = (byte)_val9;
            }

            minPlayerLevelFilter = (short)reader.ReadShort();
            if (minPlayerLevelFilter < 0)
            {
                throw new System.Exception("Forbidden value (" + minPlayerLevelFilter + ") on element of AllianceSummaryRequestMessage.minPlayerLevelFilter.");
            }

            maxPlayerLevelFilter = (short)reader.ReadShort();
            if (maxPlayerLevelFilter < 0)
            {
                throw new System.Exception("Forbidden value (" + maxPlayerLevelFilter + ") on element of AllianceSummaryRequestMessage.maxPlayerLevelFilter.");
            }

        }

    }
}


