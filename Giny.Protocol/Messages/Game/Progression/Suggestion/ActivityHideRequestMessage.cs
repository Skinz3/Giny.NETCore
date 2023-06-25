using System.Collections.Generic;
using Giny.Core.Network.Messages;
using Giny.Protocol.Types;
using Giny.Core.IO.Interfaces;
using Giny.Protocol;
using Giny.Protocol.Enums;

namespace Giny.Protocol.Messages
{ 
    public class ActivityHideRequestMessage : NetworkMessage  
    { 
        public  const ushort Id = 4454;
        public override ushort MessageId => Id;

        public short activityId;

        public ActivityHideRequestMessage()
        {
        }
        public ActivityHideRequestMessage(short activityId)
        {
            this.activityId = activityId;
        }
        public override void Serialize(IDataWriter writer)
        {
            if (activityId < 0)
            {
                throw new System.Exception("Forbidden value (" + activityId + ") on element activityId.");
            }

            writer.WriteVarShort((short)activityId);
        }
        public override void Deserialize(IDataReader reader)
        {
            activityId = (short)reader.ReadVarUhShort();
            if (activityId < 0)
            {
                throw new System.Exception("Forbidden value (" + activityId + ") on element of ActivityHideRequestMessage.activityId.");
            }

        }


    }
}








