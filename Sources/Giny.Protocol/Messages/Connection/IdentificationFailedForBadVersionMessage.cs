using System.Collections.Generic;
using Giny.Core.Network.Messages;
using Giny.Protocol.Types;
using Giny.Core.IO.Interfaces;
using Giny.Protocol;
using Giny.Protocol.Enums;
using Version = Giny.Protocol.Types.Version;

namespace Giny.Protocol.Messages
{ 
    public class IdentificationFailedForBadVersionMessage : IdentificationFailedMessage  
    { 
        public new const ushort Id = 6103;
        public override ushort MessageId => Id;

        public Version requiredVersion;

        public IdentificationFailedForBadVersionMessage()
        {
        }
        public IdentificationFailedForBadVersionMessage(Version requiredVersion,byte reason)
        {
            this.requiredVersion = requiredVersion;
            this.reason = reason;
        }
        public override void Serialize(IDataWriter writer)
        {
            base.Serialize(writer);
            requiredVersion.Serialize(writer);
        }
        public override void Deserialize(IDataReader reader)
        {
            base.Deserialize(reader);
            requiredVersion = new Version();
            requiredVersion.Deserialize(reader);
        }


    }
}








