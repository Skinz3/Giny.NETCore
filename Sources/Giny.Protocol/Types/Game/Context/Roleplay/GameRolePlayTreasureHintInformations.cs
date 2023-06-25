using System.Collections.Generic;
using Giny.Core.IO.Interfaces;
using Giny.Protocol;
using Giny.Protocol.Enums;

namespace Giny.Protocol.Types
{ 
    public class GameRolePlayTreasureHintInformations : GameRolePlayActorInformations  
    { 
        public new const ushort Id = 8293;
        public override ushort TypeId => Id;

        public short npcId;

        public GameRolePlayTreasureHintInformations()
        {
        }
        public GameRolePlayTreasureHintInformations(short npcId,double contextualId,EntityDispositionInformations disposition,EntityLook look)
        {
            this.npcId = npcId;
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
        }
        public override void Deserialize(IDataReader reader)
        {
            base.Deserialize(reader);
            npcId = (short)reader.ReadVarUhShort();
            if (npcId < 0)
            {
                throw new System.Exception("Forbidden value (" + npcId + ") on element of GameRolePlayTreasureHintInformations.npcId.");
            }

        }


    }
}








