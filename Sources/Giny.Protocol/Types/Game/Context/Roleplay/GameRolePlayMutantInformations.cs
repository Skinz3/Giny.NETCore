using System.Collections.Generic;
using Giny.Core.IO.Interfaces;
using Giny.Protocol;
using Giny.Protocol.Enums;

namespace Giny.Protocol.Types
{ 
    public class GameRolePlayMutantInformations : GameRolePlayHumanoidInformations  
    { 
        public new const ushort Id = 3496;
        public override ushort TypeId => Id;

        public short monsterId;
        public byte powerLevel;

        public GameRolePlayMutantInformations()
        {
        }
        public GameRolePlayMutantInformations(short monsterId,byte powerLevel,double contextualId,EntityDispositionInformations disposition,EntityLook look,string name,HumanInformations humanoidInfo,int accountId)
        {
            this.monsterId = monsterId;
            this.powerLevel = powerLevel;
            this.contextualId = contextualId;
            this.disposition = disposition;
            this.look = look;
            this.name = name;
            this.humanoidInfo = humanoidInfo;
            this.accountId = accountId;
        }
        public override void Serialize(IDataWriter writer)
        {
            base.Serialize(writer);
            if (monsterId < 0)
            {
                throw new System.Exception("Forbidden value (" + monsterId + ") on element monsterId.");
            }

            writer.WriteVarShort((short)monsterId);
            writer.WriteByte((byte)powerLevel);
        }
        public override void Deserialize(IDataReader reader)
        {
            base.Deserialize(reader);
            monsterId = (short)reader.ReadVarUhShort();
            if (monsterId < 0)
            {
                throw new System.Exception("Forbidden value (" + monsterId + ") on element of GameRolePlayMutantInformations.monsterId.");
            }

            powerLevel = (byte)reader.ReadByte();
        }


    }
}








