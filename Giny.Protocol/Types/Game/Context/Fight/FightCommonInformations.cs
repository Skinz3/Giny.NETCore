using System.Collections.Generic;
using Giny.Core.IO.Interfaces;
using Giny.Protocol;
using Giny.Protocol.Enums;

namespace Giny.Protocol.Types
{ 
    public class FightCommonInformations  
    { 
        public const ushort Id = 5751;
        public virtual ushort TypeId => Id;

        public short fightId;
        public byte fightType;
        public FightTeamInformations[] fightTeams;
        public short[] fightTeamsPositions;
        public FightOptionsInformations[] fightTeamsOptions;

        public FightCommonInformations()
        {
        }
        public FightCommonInformations(short fightId,byte fightType,FightTeamInformations[] fightTeams,short[] fightTeamsPositions,FightOptionsInformations[] fightTeamsOptions)
        {
            this.fightId = fightId;
            this.fightType = fightType;
            this.fightTeams = fightTeams;
            this.fightTeamsPositions = fightTeamsPositions;
            this.fightTeamsOptions = fightTeamsOptions;
        }
        public virtual void Serialize(IDataWriter writer)
        {
            if (fightId < 0)
            {
                throw new System.Exception("Forbidden value (" + fightId + ") on element fightId.");
            }

            writer.WriteVarShort((short)fightId);
            writer.WriteByte((byte)fightType);
            writer.WriteShort((short)fightTeams.Length);
            for (uint _i3 = 0;_i3 < fightTeams.Length;_i3++)
            {
                writer.WriteShort((short)(fightTeams[_i3] as FightTeamInformations).TypeId);
                (fightTeams[_i3] as FightTeamInformations).Serialize(writer);
            }

            writer.WriteShort((short)fightTeamsPositions.Length);
            for (uint _i4 = 0;_i4 < fightTeamsPositions.Length;_i4++)
            {
                if (fightTeamsPositions[_i4] < 0 || fightTeamsPositions[_i4] > 559)
                {
                    throw new System.Exception("Forbidden value (" + fightTeamsPositions[_i4] + ") on element 4 (starting at 1) of fightTeamsPositions.");
                }

                writer.WriteVarShort((short)fightTeamsPositions[_i4]);
            }

            writer.WriteShort((short)fightTeamsOptions.Length);
            for (uint _i5 = 0;_i5 < fightTeamsOptions.Length;_i5++)
            {
                (fightTeamsOptions[_i5] as FightOptionsInformations).Serialize(writer);
            }

        }
        public virtual void Deserialize(IDataReader reader)
        {
            uint _id3 = 0;
            FightTeamInformations _item3 = null;
            uint _val4 = 0;
            FightOptionsInformations _item5 = null;
            fightId = (short)reader.ReadVarUhShort();
            if (fightId < 0)
            {
                throw new System.Exception("Forbidden value (" + fightId + ") on element of FightCommonInformations.fightId.");
            }

            fightType = (byte)reader.ReadByte();
            if (fightType < 0)
            {
                throw new System.Exception("Forbidden value (" + fightType + ") on element of FightCommonInformations.fightType.");
            }

            uint _fightTeamsLen = (uint)reader.ReadUShort();
            for (uint _i3 = 0;_i3 < _fightTeamsLen;_i3++)
            {
                _id3 = (uint)reader.ReadUShort();
                _item3 = ProtocolTypeManager.GetInstance<FightTeamInformations>((short)_id3);
                _item3.Deserialize(reader);
                fightTeams[_i3] = _item3;
            }

            uint _fightTeamsPositionsLen = (uint)reader.ReadUShort();
            fightTeamsPositions = new short[_fightTeamsPositionsLen];
            for (uint _i4 = 0;_i4 < _fightTeamsPositionsLen;_i4++)
            {
                _val4 = (uint)reader.ReadVarUhShort();
                if (_val4 < 0 || _val4 > 559)
                {
                    throw new System.Exception("Forbidden value (" + _val4 + ") on elements of fightTeamsPositions.");
                }

                fightTeamsPositions[_i4] = (short)_val4;
            }

            uint _fightTeamsOptionsLen = (uint)reader.ReadUShort();
            for (uint _i5 = 0;_i5 < _fightTeamsOptionsLen;_i5++)
            {
                _item5 = new FightOptionsInformations();
                _item5.Deserialize(reader);
                fightTeamsOptions[_i5] = _item5;
            }

        }


    }
}








