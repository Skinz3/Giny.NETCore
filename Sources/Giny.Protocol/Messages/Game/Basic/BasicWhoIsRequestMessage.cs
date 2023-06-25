using System.Collections.Generic;
using Giny.Core.Network.Messages;
using Giny.Protocol.Types;
using Giny.Core.IO.Interfaces;
using Giny.Protocol;
using Giny.Protocol.Enums;

namespace Giny.Protocol.Messages
{ 
    public class BasicWhoIsRequestMessage : NetworkMessage  
    { 
        public  const ushort Id = 3721;
        public override ushort MessageId => Id;

        public bool verbose;
        public AbstractPlayerSearchInformation target;

        public BasicWhoIsRequestMessage()
        {
        }
        public BasicWhoIsRequestMessage(bool verbose,AbstractPlayerSearchInformation target)
        {
            this.verbose = verbose;
            this.target = target;
        }
        public override void Serialize(IDataWriter writer)
        {
            writer.WriteBoolean((bool)verbose);
            writer.WriteShort((short)target.TypeId);
            target.Serialize(writer);
        }
        public override void Deserialize(IDataReader reader)
        {
            verbose = (bool)reader.ReadBoolean();
            uint _id2 = (uint)reader.ReadUShort();
            target = ProtocolTypeManager.GetInstance<AbstractPlayerSearchInformation>((short)_id2);
            target.Deserialize(reader);
        }


    }
}








