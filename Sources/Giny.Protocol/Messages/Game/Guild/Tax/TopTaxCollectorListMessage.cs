using System.Collections.Generic;
using Giny.Core.Network.Messages;
using Giny.Protocol.Types;
using Giny.Core.IO.Interfaces;
using Giny.Protocol;
using Giny.Protocol.Enums;

namespace Giny.Protocol.Messages
{ 
    public class TopTaxCollectorListMessage : AbstractTaxCollectorListMessage  
    { 
        public new const ushort Id = 448;
        public override ushort MessageId => Id;

        public bool isDungeon;

        public TopTaxCollectorListMessage()
        {
        }
        public TopTaxCollectorListMessage(bool isDungeon,TaxCollectorInformations[] informations)
        {
            this.isDungeon = isDungeon;
            this.informations = informations;
        }
        public override void Serialize(IDataWriter writer)
        {
            base.Serialize(writer);
            writer.WriteBoolean((bool)isDungeon);
        }
        public override void Deserialize(IDataReader reader)
        {
            base.Deserialize(reader);
            isDungeon = (bool)reader.ReadBoolean();
        }


    }
}








