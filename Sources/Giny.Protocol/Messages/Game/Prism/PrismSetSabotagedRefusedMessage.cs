using System.Collections.Generic;
using Giny.Core.Network.Messages;
using Giny.Protocol.Types;
using Giny.Core.IO.Interfaces;
using Giny.Protocol;
using Giny.Protocol.Enums;

namespace Giny.Protocol.Messages
{ 
    public class PrismSetSabotagedRefusedMessage : NetworkMessage  
    { 
        public  const ushort Id = 4808;
        public override ushort MessageId => Id;

        public short subAreaId;
        public byte reason;

        public PrismSetSabotagedRefusedMessage()
        {
        }
        public PrismSetSabotagedRefusedMessage(short subAreaId,byte reason)
        {
            this.subAreaId = subAreaId;
            this.reason = reason;
        }
        public override void Serialize(IDataWriter writer)
        {
            if (subAreaId < 0)
            {
                throw new System.Exception("Forbidden value (" + subAreaId + ") on element subAreaId.");
            }

            writer.WriteVarShort((short)subAreaId);
            writer.WriteByte((byte)reason);
        }
        public override void Deserialize(IDataReader reader)
        {
            subAreaId = (short)reader.ReadVarUhShort();
            if (subAreaId < 0)
            {
                throw new System.Exception("Forbidden value (" + subAreaId + ") on element of PrismSetSabotagedRefusedMessage.subAreaId.");
            }

            reason = (byte)reader.ReadByte();
        }


    }
}








