using System.Collections.Generic;
using Giny.Core.Network.Messages;
using Giny.Protocol.Types;
using Giny.Core.IO.Interfaces;
using Giny.Protocol;
using Giny.Protocol.Enums;

namespace Giny.Protocol.Messages
{ 
    public class IgnoredAddedMessage : NetworkMessage  
    { 
        public  const ushort Id = 4502;
        public override ushort MessageId => Id;

        public IgnoredInformations ignoreAdded;
        public bool session;

        public IgnoredAddedMessage()
        {
        }
        public IgnoredAddedMessage(IgnoredInformations ignoreAdded,bool session)
        {
            this.ignoreAdded = ignoreAdded;
            this.session = session;
        }
        public override void Serialize(IDataWriter writer)
        {
            writer.WriteShort((short)ignoreAdded.TypeId);
            ignoreAdded.Serialize(writer);
            writer.WriteBoolean((bool)session);
        }
        public override void Deserialize(IDataReader reader)
        {
            uint _id1 = (uint)reader.ReadUShort();
            ignoreAdded = ProtocolTypeManager.GetInstance<IgnoredInformations>((short)_id1);
            ignoreAdded.Deserialize(reader);
            session = (bool)reader.ReadBoolean();
        }


    }
}








