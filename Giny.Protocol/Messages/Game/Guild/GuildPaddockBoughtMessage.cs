using System.Collections.Generic;
using Giny.Core.Network.Messages;
using Giny.Protocol.Types;
using Giny.Core.IO.Interfaces;
using Giny.Protocol;
using Giny.Protocol.Enums;

namespace Giny.Protocol.Messages
{ 
    public class GuildPaddockBoughtMessage : NetworkMessage  
    { 
        public  const ushort Id = 5835;
        public override ushort MessageId => Id;

        public PaddockContentInformations paddockInfo;

        public GuildPaddockBoughtMessage()
        {
        }
        public GuildPaddockBoughtMessage(PaddockContentInformations paddockInfo)
        {
            this.paddockInfo = paddockInfo;
        }
        public override void Serialize(IDataWriter writer)
        {
            paddockInfo.Serialize(writer);
        }
        public override void Deserialize(IDataReader reader)
        {
            paddockInfo = new PaddockContentInformations();
            paddockInfo.Deserialize(reader);
        }


    }
}








