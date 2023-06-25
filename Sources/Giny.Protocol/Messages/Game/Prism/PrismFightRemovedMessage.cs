using System.Collections.Generic;
using Giny.Core.Network.Messages;
using Giny.Protocol.Types;
using Giny.Core.IO.Interfaces;
using Giny.Protocol;
using Giny.Protocol.Enums;

namespace Giny.Protocol.Messages
{ 
    public class PrismFightRemovedMessage : NetworkMessage  
    { 
        public  const ushort Id = 4052;
        public override ushort MessageId => Id;

        public short subAreaId;

        public PrismFightRemovedMessage()
        {
        }
        public PrismFightRemovedMessage(short subAreaId)
        {
            this.subAreaId = subAreaId;
        }
        public override void Serialize(IDataWriter writer)
        {
            if (subAreaId < 0)
            {
                throw new System.Exception("Forbidden value (" + subAreaId + ") on element subAreaId.");
            }

            writer.WriteVarShort((short)subAreaId);
        }
        public override void Deserialize(IDataReader reader)
        {
            subAreaId = (short)reader.ReadVarUhShort();
            if (subAreaId < 0)
            {
                throw new System.Exception("Forbidden value (" + subAreaId + ") on element of PrismFightRemovedMessage.subAreaId.");
            }

        }


    }
}








