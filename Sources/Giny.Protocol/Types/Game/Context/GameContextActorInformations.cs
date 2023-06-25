using System.Collections.Generic;
using Giny.Core.IO.Interfaces;
using Giny.Protocol;
using Giny.Protocol.Enums;

namespace Giny.Protocol.Types
{ 
    public class GameContextActorInformations : GameContextActorPositionInformations  
    { 
        public new const ushort Id = 7125;
        public override ushort TypeId => Id;

        public EntityLook look;

        public GameContextActorInformations()
        {
        }
        public GameContextActorInformations(EntityLook look,double contextualId,EntityDispositionInformations disposition)
        {
            this.look = look;
            this.contextualId = contextualId;
            this.disposition = disposition;
        }
        public override void Serialize(IDataWriter writer)
        {
            base.Serialize(writer);
            look.Serialize(writer);
        }
        public override void Deserialize(IDataReader reader)
        {
            base.Deserialize(reader);
            look = new EntityLook();
            look.Deserialize(reader);
        }


    }
}








