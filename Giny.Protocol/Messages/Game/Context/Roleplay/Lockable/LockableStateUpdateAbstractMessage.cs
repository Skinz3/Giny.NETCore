using System.Collections.Generic;
using Giny.Core.Network.Messages;
using Giny.Protocol.Types;
using Giny.Core.IO.Interfaces;
using Giny.Protocol;
using Giny.Protocol.Enums;

namespace Giny.Protocol.Messages
{ 
    public class LockableStateUpdateAbstractMessage : NetworkMessage  
    { 
        public  const ushort Id = 8803;
        public override ushort MessageId => Id;

        public bool locked;

        public LockableStateUpdateAbstractMessage()
        {
        }
        public LockableStateUpdateAbstractMessage(bool locked)
        {
            this.locked = locked;
        }
        public override void Serialize(IDataWriter writer)
        {
            writer.WriteBoolean((bool)locked);
        }
        public override void Deserialize(IDataReader reader)
        {
            locked = (bool)reader.ReadBoolean();
        }


    }
}








