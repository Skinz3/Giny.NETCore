using System.Collections.Generic;
using Giny.Core.Network.Messages;
using Giny.Protocol.Types;
using Giny.Core.IO.Interfaces;
using Giny.Protocol;
using Giny.Protocol.Enums;

namespace Giny.Protocol.Messages
{ 
    public class HouseLockFromInsideRequestMessage : LockableChangeCodeMessage  
    { 
        public new const ushort Id = 9332;
        public override ushort MessageId => Id;


        public HouseLockFromInsideRequestMessage()
        {
        }
        public HouseLockFromInsideRequestMessage(string code)
        {
            this.code = code;
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








