using System.Collections.Generic;
using Giny.Core.IO.Interfaces;
using Giny.Protocol;
using Giny.Protocol.Enums;

namespace Giny.Protocol.Types
{ 
    public class AbstractFightTeamInformations  
    { 
        public const ushort Id = 7711;
        public virtual ushort TypeId => Id;

        public byte teamId;
        public double leaderId;
        public byte teamSide;
        public byte teamTypeId;
        public byte nbWaves;

        public AbstractFightTeamInformations()
        {
        }
        public AbstractFightTeamInformations(byte teamId,double leaderId,byte teamSide,byte teamTypeId,byte nbWaves)
        {
            this.teamId = teamId;
            this.leaderId = leaderId;
            this.teamSide = teamSide;
            this.teamTypeId = teamTypeId;
            this.nbWaves = nbWaves;
        }
        public virtual void Serialize(IDataWriter writer)
        {
            writer.WriteByte((byte)teamId);
            if (leaderId < -9.00719925474099E+15 || leaderId > 9.00719925474099E+15)
            {
                throw new System.Exception("Forbidden value (" + leaderId + ") on element leaderId.");
            }

            writer.WriteDouble((double)leaderId);
            writer.WriteByte((byte)teamSide);
            writer.WriteByte((byte)teamTypeId);
            if (nbWaves < 0)
            {
                throw new System.Exception("Forbidden value (" + nbWaves + ") on element nbWaves.");
            }

            writer.WriteByte((byte)nbWaves);
        }
        public virtual void Deserialize(IDataReader reader)
        {
            teamId = (byte)reader.ReadByte();
            if (teamId < 0)
            {
                throw new System.Exception("Forbidden value (" + teamId + ") on element of AbstractFightTeamInformations.teamId.");
            }

            leaderId = (double)reader.ReadDouble();
            if (leaderId < -9.00719925474099E+15 || leaderId > 9.00719925474099E+15)
            {
                throw new System.Exception("Forbidden value (" + leaderId + ") on element of AbstractFightTeamInformations.leaderId.");
            }

            teamSide = (byte)reader.ReadByte();
            teamTypeId = (byte)reader.ReadByte();
            if (teamTypeId < 0)
            {
                throw new System.Exception("Forbidden value (" + teamTypeId + ") on element of AbstractFightTeamInformations.teamTypeId.");
            }

            nbWaves = (byte)reader.ReadByte();
            if (nbWaves < 0)
            {
                throw new System.Exception("Forbidden value (" + nbWaves + ") on element of AbstractFightTeamInformations.nbWaves.");
            }

        }


    }
}








