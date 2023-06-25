using System.Collections.Generic;
using Giny.Core.Network.Messages;
using Giny.Protocol.Types;
using Giny.Core.IO.Interfaces;
using Giny.Protocol;
using Giny.Protocol.Enums;

namespace Giny.Protocol.Messages
{ 
    public class IgnoredAddRequestMessage : NetworkMessage  
    { 
        public  const ushort Id = 5826;
        public override ushort MessageId => Id;

        public AbstractPlayerSearchInformation target;
        public bool session;

        public IgnoredAddRequestMessage()
        {
        }
        public IgnoredAddRequestMessage(AbstractPlayerSearchInformation target,bool session)
        {
            this.target = target;
            this.session = session;
        }
        public override void Serialize(IDataWriter writer)
        {
            writer.WriteShort((short)target.TypeId);
            target.Serialize(writer);
            writer.WriteBoolean((bool)session);
        }
        public override void Deserialize(IDataReader reader)
        {
            uint _id1 = (uint)reader.ReadUShort();
            target = ProtocolTypeManager.GetInstance<AbstractPlayerSearchInformation>((short)_id1);
            target.Deserialize(reader);
            session = (bool)reader.ReadBoolean();
        }


    }
}








