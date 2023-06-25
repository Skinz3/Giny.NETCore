using System.Collections.Generic;
using Giny.Core.Network.Messages;
using Giny.Protocol.Types;
using Giny.Core.IO.Interfaces;
using Giny.Protocol;
using Giny.Protocol.Enums;

namespace Giny.Protocol.Messages
{ 
    public class ChatClientPrivateMessage : ChatAbstractClientMessage  
    { 
        public new const ushort Id = 9337;
        public override ushort MessageId => Id;

        public AbstractPlayerSearchInformation receiver;

        public ChatClientPrivateMessage()
        {
        }
        public ChatClientPrivateMessage(AbstractPlayerSearchInformation receiver,string content)
        {
            this.receiver = receiver;
            this.content = content;
        }
        public override void Serialize(IDataWriter writer)
        {
            base.Serialize(writer);
            writer.WriteShort((short)receiver.TypeId);
            receiver.Serialize(writer);
        }
        public override void Deserialize(IDataReader reader)
        {
            base.Deserialize(reader);
            uint _id1 = (uint)reader.ReadUShort();
            receiver = ProtocolTypeManager.GetInstance<AbstractPlayerSearchInformation>((short)_id1);
            receiver.Deserialize(reader);
        }


    }
}








