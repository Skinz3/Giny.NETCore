using System.Collections.Generic;
using Giny.Core.Network.Messages;
using Giny.Protocol.Types;
using Giny.Core.IO.Interfaces;
using Giny.Protocol;
using Giny.Protocol.Enums;

namespace Giny.Protocol.Messages
{ 
    public class AlliancePartialListMessage : AllianceListMessage  
    { 
        public new const ushort Id = 822;
        public override ushort MessageId => Id;


        public AlliancePartialListMessage()
        {
        }
        public AlliancePartialListMessage(AllianceFactSheetInformations[] alliances)
        {
            this.alliances = alliances;
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








