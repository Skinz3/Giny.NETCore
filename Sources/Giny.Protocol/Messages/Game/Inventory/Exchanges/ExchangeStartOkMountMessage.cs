using System.Collections.Generic;
using Giny.Core.Network.Messages;
using Giny.Protocol.Types;
using Giny.Core.IO.Interfaces;
using Giny.Protocol;
using Giny.Protocol.Enums;

namespace Giny.Protocol.Messages
{ 
    public class ExchangeStartOkMountMessage : ExchangeStartOkMountWithOutPaddockMessage  
    { 
        public new const ushort Id = 1351;
        public override ushort MessageId => Id;

        public MountClientData[] paddockedMountsDescription;

        public ExchangeStartOkMountMessage()
        {
        }
        public ExchangeStartOkMountMessage(MountClientData[] paddockedMountsDescription,MountClientData[] stabledMountsDescription)
        {
            this.paddockedMountsDescription = paddockedMountsDescription;
            this.stabledMountsDescription = stabledMountsDescription;
        }
        public override void Serialize(IDataWriter writer)
        {
            base.Serialize(writer);
            writer.WriteShort((short)paddockedMountsDescription.Length);
            for (uint _i1 = 0;_i1 < paddockedMountsDescription.Length;_i1++)
            {
                (paddockedMountsDescription[_i1] as MountClientData).Serialize(writer);
            }

        }
        public override void Deserialize(IDataReader reader)
        {
            MountClientData _item1 = null;
            base.Deserialize(reader);
            uint _paddockedMountsDescriptionLen = (uint)reader.ReadUShort();
            for (uint _i1 = 0;_i1 < _paddockedMountsDescriptionLen;_i1++)
            {
                _item1 = new MountClientData();
                _item1.Deserialize(reader);
                paddockedMountsDescription[_i1] = _item1;
            }

        }


    }
}








