using System.Collections.Generic;
using Giny.Core.Network.Messages;
using Giny.Protocol.Types;
using Giny.Core.IO.Interfaces;
using Giny.Protocol;
using Giny.Protocol.Enums;

namespace Giny.Protocol.Messages
{ 
    public class MountRidingMessage : NetworkMessage  
    { 
        public  const ushort Id = 8146;
        public override ushort MessageId => Id;

        public bool isRiding;
        public bool isAutopilot;

        public MountRidingMessage()
        {
        }
        public MountRidingMessage(bool isRiding,bool isAutopilot)
        {
            this.isRiding = isRiding;
            this.isAutopilot = isAutopilot;
        }
        public override void Serialize(IDataWriter writer)
        {
            byte _box0 = 0;
            _box0 = BooleanByteWrapper.SetFlag(_box0,0,isRiding);
            _box0 = BooleanByteWrapper.SetFlag(_box0,1,isAutopilot);
            writer.WriteByte((byte)_box0);
        }
        public override void Deserialize(IDataReader reader)
        {
            byte _box0 = reader.ReadByte();
            isRiding = BooleanByteWrapper.GetFlag(_box0,0);
            isAutopilot = BooleanByteWrapper.GetFlag(_box0,1);
        }


    }
}








