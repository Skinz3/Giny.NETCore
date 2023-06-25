using System.Collections.Generic;
using Giny.Core.Network.Messages;
using Giny.Protocol.Types;
using Giny.Core.IO.Interfaces;
using Giny.Protocol;
using Giny.Protocol.Enums;

namespace Giny.Protocol.Messages
{ 
    public class ExchangeStartOkMountWithOutPaddockMessage : NetworkMessage  
    { 
        public  const ushort Id = 257;
        public override ushort MessageId => Id;

        public MountClientData[] stabledMountsDescription;

        public ExchangeStartOkMountWithOutPaddockMessage()
        {
        }
        public ExchangeStartOkMountWithOutPaddockMessage(MountClientData[] stabledMountsDescription)
        {
            this.stabledMountsDescription = stabledMountsDescription;
        }
        public override void Serialize(IDataWriter writer)
        {
            writer.WriteShort((short)stabledMountsDescription.Length);
            for (uint _i1 = 0;_i1 < stabledMountsDescription.Length;_i1++)
            {
                (stabledMountsDescription[_i1] as MountClientData).Serialize(writer);
            }

        }
        public override void Deserialize(IDataReader reader)
        {
            MountClientData _item1 = null;
            uint _stabledMountsDescriptionLen = (uint)reader.ReadUShort();
            for (uint _i1 = 0;_i1 < _stabledMountsDescriptionLen;_i1++)
            {
                _item1 = new MountClientData();
                _item1.Deserialize(reader);
                stabledMountsDescription[_i1] = _item1;
            }

        }


    }
}








