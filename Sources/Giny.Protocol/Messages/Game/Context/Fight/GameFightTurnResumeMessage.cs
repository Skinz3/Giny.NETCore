using System.Collections.Generic;
using Giny.Core.Network.Messages;
using Giny.Protocol.Types;
using Giny.Core.IO.Interfaces;
using Giny.Protocol;
using Giny.Protocol.Enums;

namespace Giny.Protocol.Messages
{ 
    public class GameFightTurnResumeMessage : GameFightTurnStartMessage  
    { 
        public new const ushort Id = 1695;
        public override ushort MessageId => Id;

        public int remainingTime;

        public GameFightTurnResumeMessage()
        {
        }
        public GameFightTurnResumeMessage(int remainingTime,double id,int waitTime)
        {
            this.remainingTime = remainingTime;
            this.id = id;
            this.waitTime = waitTime;
        }
        public override void Serialize(IDataWriter writer)
        {
            base.Serialize(writer);
            if (remainingTime < 0)
            {
                throw new System.Exception("Forbidden value (" + remainingTime + ") on element remainingTime.");
            }

            writer.WriteVarInt((int)remainingTime);
        }
        public override void Deserialize(IDataReader reader)
        {
            base.Deserialize(reader);
            remainingTime = (int)reader.ReadVarUhInt();
            if (remainingTime < 0)
            {
                throw new System.Exception("Forbidden value (" + remainingTime + ") on element of GameFightTurnResumeMessage.remainingTime.");
            }

        }


    }
}








