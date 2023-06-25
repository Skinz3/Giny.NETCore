using System.Collections.Generic;
using Giny.Core.IO.Interfaces;
using Giny.Protocol;
using Giny.Protocol.Enums;

namespace Giny.Protocol.Types
{ 
    public class HumanOptionOrnament : HumanOption  
    { 
        public new const ushort Id = 3007;
        public override ushort TypeId => Id;

        public short ornamentId;
        public short level;
        public short leagueId;
        public int ladderPosition;

        public HumanOptionOrnament()
        {
        }
        public HumanOptionOrnament(short ornamentId,short level,short leagueId,int ladderPosition)
        {
            this.ornamentId = ornamentId;
            this.level = level;
            this.leagueId = leagueId;
            this.ladderPosition = ladderPosition;
        }
        public override void Serialize(IDataWriter writer)
        {
            base.Serialize(writer);
            if (ornamentId < 0)
            {
                throw new System.Exception("Forbidden value (" + ornamentId + ") on element ornamentId.");
            }

            writer.WriteVarShort((short)ornamentId);
            if (level < 0)
            {
                throw new System.Exception("Forbidden value (" + level + ") on element level.");
            }

            writer.WriteVarShort((short)level);
            writer.WriteVarShort((short)leagueId);
            writer.WriteInt((int)ladderPosition);
        }
        public override void Deserialize(IDataReader reader)
        {
            base.Deserialize(reader);
            ornamentId = (short)reader.ReadVarUhShort();
            if (ornamentId < 0)
            {
                throw new System.Exception("Forbidden value (" + ornamentId + ") on element of HumanOptionOrnament.ornamentId.");
            }

            level = (short)reader.ReadVarUhShort();
            if (level < 0)
            {
                throw new System.Exception("Forbidden value (" + level + ") on element of HumanOptionOrnament.level.");
            }

            leagueId = (short)reader.ReadVarShort();
            ladderPosition = (int)reader.ReadInt();
        }


    }
}








