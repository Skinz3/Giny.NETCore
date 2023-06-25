using System.Collections.Generic;
using Giny.Core.Network.Messages;
using Giny.Protocol.Types;
using Giny.Core.IO.Interfaces;
using Giny.Protocol;
using Giny.Protocol.Enums;

namespace Giny.Protocol.Messages
{ 
    public class ExchangeCraftResultMagicWithObjectDescMessage : ExchangeCraftResultWithObjectDescMessage  
    { 
        public new const ushort Id = 2638;
        public override ushort MessageId => Id;

        public byte magicPoolStatus;

        public ExchangeCraftResultMagicWithObjectDescMessage()
        {
        }
        public ExchangeCraftResultMagicWithObjectDescMessage(byte magicPoolStatus,byte craftResult,ObjectItemNotInContainer objectInfo)
        {
            this.magicPoolStatus = magicPoolStatus;
            this.craftResult = craftResult;
            this.objectInfo = objectInfo;
        }
        public override void Serialize(IDataWriter writer)
        {
            base.Serialize(writer);
            writer.WriteByte((byte)magicPoolStatus);
        }
        public override void Deserialize(IDataReader reader)
        {
            base.Deserialize(reader);
            magicPoolStatus = (byte)reader.ReadByte();
        }


    }
}








