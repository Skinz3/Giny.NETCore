using System.Collections.Generic;
using Giny.Core.Network.Messages;
using Giny.Protocol.Types;
using Giny.Core.IO.Interfaces;
using Giny.Protocol;
using Giny.Protocol.Enums;

namespace Giny.Protocol.Messages
{ 
    public class TaxCollectorDialogQuestionBasicMessage : NetworkMessage  
    { 
        public  const ushort Id = 2837;
        public override ushort MessageId => Id;

        public BasicGuildInformations guildInfo;

        public TaxCollectorDialogQuestionBasicMessage()
        {
        }
        public TaxCollectorDialogQuestionBasicMessage(BasicGuildInformations guildInfo)
        {
            this.guildInfo = guildInfo;
        }
        public override void Serialize(IDataWriter writer)
        {
            guildInfo.Serialize(writer);
        }
        public override void Deserialize(IDataReader reader)
        {
            guildInfo = new BasicGuildInformations();
            guildInfo.Deserialize(reader);
        }


    }
}








