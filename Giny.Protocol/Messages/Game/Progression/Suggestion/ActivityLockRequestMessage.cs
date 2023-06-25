using System.Collections.Generic;
using Giny.Core.Network.Messages;
using Giny.Protocol.Types;
using Giny.Core.IO.Interfaces;
using Giny.Protocol;
using Giny.Protocol.Enums;

namespace Giny.Protocol.Messages
{ 
    public class ActivityLockRequestMessage : NetworkMessage  
    { 
        public  const ushort Id = 5583;
        public override ushort MessageId => Id;

        public short activityId;
        public bool @lock;

        public ActivityLockRequestMessage()
        {
        }
        public ActivityLockRequestMessage(short activityId,bool @lock)
        {
            this.activityId = activityId;
            this.@lock = @lock;
        }
        public override void Serialize(IDataWriter writer)
        {
            if (activityId < 0)
            {
                throw new System.Exception("Forbidden value (" + activityId + ") on element activityId.");
            }

            writer.WriteVarShort((short)activityId);
            writer.WriteBoolean((bool)@lock);
        }
        public override void Deserialize(IDataReader reader)
        {
            activityId = (short)reader.ReadVarUhShort();
            if (activityId < 0)
            {
                throw new System.Exception("Forbidden value (" + activityId + ") on element of ActivityLockRequestMessage.activityId.");
            }

            @lock = (bool)reader.ReadBoolean();
        }


    }
}








