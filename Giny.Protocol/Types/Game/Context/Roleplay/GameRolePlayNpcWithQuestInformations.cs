using System.Collections.Generic;
using Giny.Core.IO.Interfaces;
using Giny.Protocol;
using Giny.Protocol.Enums;

namespace Giny.Protocol.Types
{ 
    public class GameRolePlayNpcWithQuestInformations : GameRolePlayNpcInformations  
    { 
        public new const ushort Id = 3464;
        public override ushort TypeId => Id;

        public GameRolePlayNpcQuestFlag questFlag;

        public GameRolePlayNpcWithQuestInformations()
        {
        }
        public GameRolePlayNpcWithQuestInformations(GameRolePlayNpcQuestFlag questFlag,double contextualId,EntityDispositionInformations disposition,EntityLook look,short npcId,bool sex,short specialArtworkId)
        {
            this.questFlag = questFlag;
            this.contextualId = contextualId;
            this.disposition = disposition;
            this.look = look;
            this.npcId = npcId;
            this.sex = sex;
            this.specialArtworkId = specialArtworkId;
        }
        public override void Serialize(IDataWriter writer)
        {
            base.Serialize(writer);
            questFlag.Serialize(writer);
        }
        public override void Deserialize(IDataReader reader)
        {
            base.Deserialize(reader);
            questFlag = new GameRolePlayNpcQuestFlag();
            questFlag.Deserialize(reader);
        }


    }
}








