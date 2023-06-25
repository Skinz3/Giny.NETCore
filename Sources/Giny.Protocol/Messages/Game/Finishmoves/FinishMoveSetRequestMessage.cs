using System.Collections.Generic;
using Giny.Core.Network.Messages;
using Giny.Protocol.Types;
using Giny.Core.IO.Interfaces;
using Giny.Protocol;
using Giny.Protocol.Enums;

namespace Giny.Protocol.Messages
{ 
    public class FinishMoveSetRequestMessage : NetworkMessage  
    { 
        public  const ushort Id = 7408;
        public override ushort MessageId => Id;

        public int finishMoveId;
        public bool finishMoveState;

        public FinishMoveSetRequestMessage()
        {
        }
        public FinishMoveSetRequestMessage(int finishMoveId,bool finishMoveState)
        {
            this.finishMoveId = finishMoveId;
            this.finishMoveState = finishMoveState;
        }
        public override void Serialize(IDataWriter writer)
        {
            if (finishMoveId < 0)
            {
                throw new System.Exception("Forbidden value (" + finishMoveId + ") on element finishMoveId.");
            }

            writer.WriteInt((int)finishMoveId);
            writer.WriteBoolean((bool)finishMoveState);
        }
        public override void Deserialize(IDataReader reader)
        {
            finishMoveId = (int)reader.ReadInt();
            if (finishMoveId < 0)
            {
                throw new System.Exception("Forbidden value (" + finishMoveId + ") on element of FinishMoveSetRequestMessage.finishMoveId.");
            }

            finishMoveState = (bool)reader.ReadBoolean();
        }


    }
}








