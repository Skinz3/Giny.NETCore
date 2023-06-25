using System.Collections.Generic;
using Giny.Core.IO.Interfaces;
using Giny.Protocol;
using Giny.Protocol.Enums;

namespace Giny.Protocol.Types
{ 
    public class TaxCollectorGuildInformations : TaxCollectorComplementaryInformations  
    { 
        public new const ushort Id = 1367;
        public override ushort TypeId => Id;

        public BasicGuildInformations guild;

        public TaxCollectorGuildInformations()
        {
        }
        public TaxCollectorGuildInformations(BasicGuildInformations guild)
        {
            this.guild = guild;
        }
        public override void Serialize(IDataWriter writer)
        {
            base.Serialize(writer);
            guild.Serialize(writer);
        }
        public override void Deserialize(IDataReader reader)
        {
            base.Deserialize(reader);
            guild = new BasicGuildInformations();
            guild.Deserialize(reader);
        }


    }
}








