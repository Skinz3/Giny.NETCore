using System.Collections.Generic;
using Giny.Core.IO.Interfaces;
using Giny.Protocol;
using Giny.Protocol.Enums;

namespace Giny.Protocol.Types
{ 
    public class FightExternalInformations  
    { 
        public const ushort Id = 7549;
        public virtual ushort TypeId => Id;

        public short fightId;
        public byte fightType;
        public int fightStart;
        public bool fightSpectatorLocked;
        public FightTeamLightInformations[] fightTeams;
        public FightOptionsInformations[] fightTeamsOptions;

        public FightExternalInformations()
        {
        }
        public FightExternalInformations(short fightId,byte fightType,int fightStart,bool fightSpectatorLocked,FightTeamLightInformations[] fightTeams,FightOptionsInformations[] fightTeamsOptions)
        {
            this.fightId = fightId;
            this.fightType = fightType;
            this.fightStart = fightStart;
            this.fightSpectatorLocked = fightSpectatorLocked;
            this.fightTeams = fightTeams;
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
            if (fightStart < 0)
            {
                throw new System.Exception("Forbidden value (" + fightStart + ") on element fightStart.");
            }

            writer.WriteInt((int)fightStart);
            writer.WriteBoolean((bool)fightSpectatorLocked);
            for (uint _i5 = 0;_i5 < 2;_i5++)
            {
                fightTeams[_i5].Serialize(writer);
            }

            for (uint _i6 = 0;_i6 < 2;_i6++)
            {
                fightTeamsOptions[_i6].Serialize(writer);
            }

        }
        public virtual void Deserialize(IDataReader reader)
        {
            fightId = (short)reader.ReadVarUhShort();
            if (fightId < 0)
            {
                throw new System.Exception("Forbidden value (" + fightId + ") on element of FightExternalInformations.fightId.");
            }

            fightType = (byte)reader.ReadByte();
            if (fightType < 0)
            {
                throw new System.Exception("Forbidden value (" + fightType + ") on element of FightExternalInformations.fightType.");
            }

            fightStart = (int)reader.ReadInt();
            if (fightStart < 0)
            {
                throw new System.Exception("Forbidden value (" + fightStart + ") on element of FightExternalInformations.fightStart.");
            }

            fightSpectatorLocked = (bool)reader.ReadBoolean();
            for (uint _i5 = 0;_i5 < 2;_i5++)
            {
                fightTeams[_i5] = new FightTeamLightInformations();
                fightTeams[_i5].Deserialize(reader);
            }

            for (uint _i6 = 0;_i6 < 2;_i6++)
            {
                fightTeamsOptions[_i6] = new FightOptionsInformations();
                fightTeamsOptions[_i6].Deserialize(reader);
            }

        }


    }
}








