using System.Collections.Generic;
using Giny.Core.Network.Messages;
using Giny.Protocol.Types;
using Giny.Core.IO.Interfaces;
using Giny.Protocol;
using Giny.Protocol.Enums;

namespace Giny.Protocol.Messages
{ 
    public class ClientYouAreDrunkMessage : DebugInClientMessage  
    { 
        public new const ushort Id = 367;
        public override ushort MessageId => Id;


        public ClientYouAreDrunkMessage()
        {
        }
        public ClientYouAreDrunkMessage(byte level,string message)
        {
            this.level = level;
            this.message = message;
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








