using System.Collections.Generic;
using Giny.Core.Network.Messages;
using Giny.Protocol.Types;
using Giny.Core.IO.Interfaces;
using Giny.Protocol;
using Giny.Protocol.Enums;

namespace Giny.Protocol.Messages
{ 
    public class PrismInfoJoinLeaveRequestMessage : NetworkMessage  
    { 
        public  const ushort Id = 2253;
        public override ushort MessageId => Id;

        public bool join;

        public PrismInfoJoinLeaveRequestMessage()
        {
        }
        public PrismInfoJoinLeaveRequestMessage(bool join)
        {
            this.join = join;
        }
        public override void Serialize(IDataWriter writer)
        {
            writer.WriteBoolean((bool)join);
        }
        public override void Deserialize(IDataReader reader)
        {
            join = (bool)reader.ReadBoolean();
        }


    }
}








