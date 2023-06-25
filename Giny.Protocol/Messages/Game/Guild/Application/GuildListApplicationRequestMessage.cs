using System.Collections.Generic;
using Giny.Core.Network.Messages;
using Giny.Protocol.Types;
using Giny.Core.IO.Interfaces;
using Giny.Protocol;
using Giny.Protocol.Enums;

namespace Giny.Protocol.Messages
{ 
    public class GuildListApplicationRequestMessage : PaginationRequestAbstractMessage  
    { 
        public new const ushort Id = 9014;
        public override ushort MessageId => Id;


        public GuildListApplicationRequestMessage()
        {
        }
        public GuildListApplicationRequestMessage(double offset,uint count)
        {
            this.offset = offset;
            this.count = count;
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








