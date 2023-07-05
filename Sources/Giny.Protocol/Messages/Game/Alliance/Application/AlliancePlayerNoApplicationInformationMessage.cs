using System.Collections.Generic;
using Giny.Core.Network.Messages;
using Giny.Protocol.Types;
using Giny.Core.IO.Interfaces;
using Giny.Protocol;
using Giny.Protocol.Enums;

namespace Giny.Protocol.Messages
{
    public class AlliancePlayerNoApplicationInformationMessage : AlliancePlayerApplicationAbstractMessage
    {
        public new const ushort Id = 971;
        public override ushort MessageId => Id;


        public AlliancePlayerNoApplicationInformationMessage()
        {
        }
        public override void Serialize(IDataWriter writer)
        {
        }
        public override void Deserialize(IDataReader reader)
        {
        }

    }
}


