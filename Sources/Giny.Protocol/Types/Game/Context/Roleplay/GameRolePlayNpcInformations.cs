using System.Collections.Generic;
using Giny.Core.IO.Interfaces;
using Giny.Protocol;
using Giny.Protocol.Enums;

namespace Giny.Protocol.Types
{ 
    public class GameRolePlayNpcInformations : GameRolePlayActorInformations  
    { 
        public new const ushort Id = 9237;
        public override ushort TypeId => Id;

        public short npcId;
        public bool sex;
        public short specialArtworkId;

        public GameRolePlayNpcInformations()
        {
        }
        public GameRolePlayNpcInformations(short npcId,bool sex,short specialArtworkId,double contextualId,EntityDispositionInformations disposition,EntityLook look)
        {
            this.npcId = npcId;
            this.sex = sex;
            this.specialArtworkId = specialArtworkId;
            this.contextualId = contextualId;
            this.disposition = disposition;
            this.look = look;
        }
        public override void Serialize(IDataWriter writer)
        {
            base.Serialize(writer);
            if (npcId < 0)
            {
                throw new System.Exception("Forbidden value (" + npcId + ") on element npcId.");
            }

            writer.WriteVarShort((short)npcId);
            writer.WriteBoolean((bool)sex);
            if (specialArtworkId < 0)
            {
                throw new System.Exception("Forbidden value (" + specialArtworkId + ") on element specialArtworkId.");
            }

            writer.WriteVarShort((short)specialArtworkId);
        }
        public override void Deserialize(IDataReader reader)
        {
            base.Deserialize(reader);
            npcId = (short)reader.ReadVarUhShort();
            if (npcId < 0)
            {
                throw new System.Exception("Forbidden value (" + npcId + ") on element of GameRolePlayNpcInformations.npcId.");
            }

            sex = (bool)reader.ReadBoolean();
            specialArtworkId = (short)reader.ReadVarUhShort();
            if (specialArtworkId < 0)
            {
                throw new System.Exception("Forbidden value (" + specialArtworkId + ") on element of GameRolePlayNpcInformations.specialArtworkId.");
            }

        }


    }
}








