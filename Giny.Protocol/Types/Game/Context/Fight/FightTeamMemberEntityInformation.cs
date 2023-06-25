using System.Collections.Generic;
using Giny.Core.IO.Interfaces;
using Giny.Protocol;
using Giny.Protocol.Enums;

namespace Giny.Protocol.Types
{ 
    public class FightTeamMemberEntityInformation : FightTeamMemberInformations  
    { 
        public new const ushort Id = 9031;
        public override ushort TypeId => Id;

        public byte entityModelId;
        public short level;
        public double masterId;

        public FightTeamMemberEntityInformation()
        {
        }
        public FightTeamMemberEntityInformation(byte entityModelId,short level,double masterId,double id)
        {
            this.entityModelId = entityModelId;
            this.level = level;
            this.masterId = masterId;
            this.id = id;
        }
        public override void Serialize(IDataWriter writer)
        {
            base.Serialize(writer);
            if (entityModelId < 0)
            {
                throw new System.Exception("Forbidden value (" + entityModelId + ") on element entityModelId.");
            }

            writer.WriteByte((byte)entityModelId);
            if (level < 1 || level > 200)
            {
                throw new System.Exception("Forbidden value (" + level + ") on element level.");
            }

            writer.WriteVarShort((short)level);
            if (masterId < -9.00719925474099E+15 || masterId > 9.00719925474099E+15)
            {
                throw new System.Exception("Forbidden value (" + masterId + ") on element masterId.");
            }

            writer.WriteDouble((double)masterId);
        }
        public override void Deserialize(IDataReader reader)
        {
            base.Deserialize(reader);
            entityModelId = (byte)reader.ReadByte();
            if (entityModelId < 0)
            {
                throw new System.Exception("Forbidden value (" + entityModelId + ") on element of FightTeamMemberEntityInformation.entityModelId.");
            }

            level = (short)reader.ReadVarUhShort();
            if (level < 1 || level > 200)
            {
                throw new System.Exception("Forbidden value (" + level + ") on element of FightTeamMemberEntityInformation.level.");
            }

            masterId = (double)reader.ReadDouble();
            if (masterId < -9.00719925474099E+15 || masterId > 9.00719925474099E+15)
            {
                throw new System.Exception("Forbidden value (" + masterId + ") on element of FightTeamMemberEntityInformation.masterId.");
            }

        }


    }
}








