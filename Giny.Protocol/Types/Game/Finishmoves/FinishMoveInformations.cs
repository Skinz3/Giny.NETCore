using System.Collections.Generic;
using Giny.Core.IO.Interfaces;
using Giny.Protocol;
using Giny.Protocol.Enums;

namespace Giny.Protocol.Types
{ 
    public class FinishMoveInformations  
    { 
        public const ushort Id = 9080;
        public virtual ushort TypeId => Id;

        public int finishMoveId;
        public bool finishMoveState;

        public FinishMoveInformations()
        {
        }
        public FinishMoveInformations(int finishMoveId,bool finishMoveState)
        {
            this.finishMoveId = finishMoveId;
            this.finishMoveState = finishMoveState;
        }
        public virtual void Serialize(IDataWriter writer)
        {
            if (finishMoveId < 0)
            {
                throw new System.Exception("Forbidden value (" + finishMoveId + ") on element finishMoveId.");
            }

            writer.WriteInt((int)finishMoveId);
            writer.WriteBoolean((bool)finishMoveState);
        }
        public virtual void Deserialize(IDataReader reader)
        {
            finishMoveId = (int)reader.ReadInt();
            if (finishMoveId < 0)
            {
                throw new System.Exception("Forbidden value (" + finishMoveId + ") on element of FinishMoveInformations.finishMoveId.");
            }

            finishMoveState = (bool)reader.ReadBoolean();
        }


    }
}








