using System.Collections.Generic;
using Giny.Core.Network.Messages;
using Giny.Protocol.Types;
using Giny.Core.IO.Interfaces;
using Giny.Protocol;
using Giny.Protocol.Enums;

namespace Giny.Protocol.Messages
{ 
    public class ExchangeMountsStableAddMessage : NetworkMessage  
    { 
        public  const ushort Id = 3365;
        public override ushort MessageId => Id;

        public MountClientData[] mountDescription;

        public ExchangeMountsStableAddMessage()
        {
        }
        public ExchangeMountsStableAddMessage(MountClientData[] mountDescription)
        {
            this.mountDescription = mountDescription;
        }
        public override void Serialize(IDataWriter writer)
        {
            writer.WriteShort((short)mountDescription.Length);
            for (uint _i1 = 0;_i1 < mountDescription.Length;_i1++)
            {
                (mountDescription[_i1] as MountClientData).Serialize(writer);
            }

        }
        public override void Deserialize(IDataReader reader)
        {
            MountClientData _item1 = null;
            uint _mountDescriptionLen = (uint)reader.ReadUShort();
            for (uint _i1 = 0;_i1 < _mountDescriptionLen;_i1++)
            {
                _item1 = new MountClientData();
                _item1.Deserialize(reader);
                mountDescription[_i1] = _item1;
            }

        }


    }
}








