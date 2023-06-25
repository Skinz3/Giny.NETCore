using System.Collections.Generic;
using Giny.Core.Network.Messages;
using Giny.Protocol.Types;
using Giny.Core.IO.Interfaces;
using Giny.Protocol;
using Giny.Protocol.Enums;

namespace Giny.Protocol.Messages
{ 
    public class PrismFightJoinLeaveRequestMessage : NetworkMessage  
    { 
        public  const ushort Id = 8516;
        public override ushort MessageId => Id;

        public short subAreaId;
        public bool join;

        public PrismFightJoinLeaveRequestMessage()
        {
        }
        public PrismFightJoinLeaveRequestMessage(short subAreaId,bool join)
        {
            this.subAreaId = subAreaId;
            this.join = join;
        }
        public override void Serialize(IDataWriter writer)
        {
            if (subAreaId < 0)
            {
                throw new System.Exception("Forbidden value (" + subAreaId + ") on element subAreaId.");
            }

            writer.WriteVarShort((short)subAreaId);
            writer.WriteBoolean((bool)join);
        }
        public override void Deserialize(IDataReader reader)
        {
            subAreaId = (short)reader.ReadVarUhShort();
            if (subAreaId < 0)
            {
                throw new System.Exception("Forbidden value (" + subAreaId + ") on element of PrismFightJoinLeaveRequestMessage.subAreaId.");
            }

            join = (bool)reader.ReadBoolean();
        }


    }
}








