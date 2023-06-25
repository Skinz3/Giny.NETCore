using System.Collections.Generic;
using Giny.Core.IO.Interfaces;
using Giny.Protocol;
using Giny.Protocol.Enums;

namespace Giny.Protocol.Types
{ 
    public class GameRolePlayCharacterInformations : GameRolePlayHumanoidInformations  
    { 
        public new const ushort Id = 3465;
        public override ushort TypeId => Id;

        public ActorAlignmentInformations alignmentInfos;

        public GameRolePlayCharacterInformations()
        {
        }
        public GameRolePlayCharacterInformations(ActorAlignmentInformations alignmentInfos,double contextualId,EntityDispositionInformations disposition,EntityLook look,string name,HumanInformations humanoidInfo,int accountId)
        {
            this.alignmentInfos = alignmentInfos;
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
            alignmentInfos.Serialize(writer);
        }
        public override void Deserialize(IDataReader reader)
        {
            base.Deserialize(reader);
            alignmentInfos = new ActorAlignmentInformations();
            alignmentInfos.Deserialize(reader);
        }


    }
}








