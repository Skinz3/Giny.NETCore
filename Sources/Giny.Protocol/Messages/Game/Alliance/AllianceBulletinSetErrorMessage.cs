using System.Collections.Generic;
using Giny.Core.Network.Messages;
using Giny.Protocol.Types;
using Giny.Core.IO.Interfaces;
using Giny.Protocol;
using Giny.Protocol.Enums;

namespace Giny.Protocol.Messages
{ 
    public class AllianceBulletinSetErrorMessage : SocialNoticeSetErrorMessage  
    { 
        public new const ushort Id = 3851;
        public override ushort MessageId => Id;


        public AllianceBulletinSetErrorMessage()
        {
        }
        public AllianceBulletinSetErrorMessage(byte reason)
        {
            this.reason = reason;
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








