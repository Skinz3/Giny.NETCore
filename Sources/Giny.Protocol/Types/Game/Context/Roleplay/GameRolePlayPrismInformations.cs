using System.Collections.Generic;
using Giny.Core.IO.Interfaces;
using Giny.Protocol;
using Giny.Protocol.Enums;

namespace Giny.Protocol.Types
{ 
    public class GameRolePlayPrismInformations : GameRolePlayActorInformations  
    { 
        public new const ushort Id = 882;
        public override ushort TypeId => Id;

        public PrismInformation prism;

        public GameRolePlayPrismInformations()
        {
        }
        public GameRolePlayPrismInformations(PrismInformation prism,double contextualId,EntityDispositionInformations disposition,EntityLook look)
        {
            this.prism = prism;
            this.contextualId = contextualId;
            this.disposition = disposition;
            this.look = look;
        }
        public override void Serialize(IDataWriter writer)
        {
            base.Serialize(writer);
            writer.WriteShort((short)prism.TypeId);
            prism.Serialize(writer);
        }
        public override void Deserialize(IDataReader reader)
        {
            base.Deserialize(reader);
            uint _id1 = (uint)reader.ReadUShort();
            prism = ProtocolTypeManager.GetInstance<PrismInformation>((short)_id1);
            prism.Deserialize(reader);
        }


    }
}








