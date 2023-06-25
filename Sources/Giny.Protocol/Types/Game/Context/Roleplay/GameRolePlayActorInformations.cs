using System.Collections.Generic;
using Giny.Core.IO.Interfaces;
using Giny.Protocol;
using Giny.Protocol.Enums;

namespace Giny.Protocol.Types
{ 
    public class GameRolePlayActorInformations : GameContextActorInformations  
    { 
        public new const ushort Id = 1036;
        public override ushort TypeId => Id;


        public GameRolePlayActorInformations()
        {
        }
        public GameRolePlayActorInformations(double contextualId,EntityDispositionInformations disposition,EntityLook look)
        {
            this.contextualId = contextualId;
            this.disposition = disposition;
            this.look = look;
        }
        public override void Serialize(IDataWriter writer)
        {
            base.Serialize(writer);
        }
        public override void Deserialize(IDataReader reader)
        {
            base.Deserialize(reader);
        }


    }
}








