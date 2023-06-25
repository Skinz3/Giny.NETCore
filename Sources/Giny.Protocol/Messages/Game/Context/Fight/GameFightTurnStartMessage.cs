using System.Collections.Generic;
using Giny.Core.Network.Messages;
using Giny.Protocol.Types;
using Giny.Core.IO.Interfaces;
using Giny.Protocol;
using Giny.Protocol.Enums;

namespace Giny.Protocol.Messages
{ 
    public class GameFightTurnStartMessage : NetworkMessage  
    { 
        public  const ushort Id = 575;
        public override ushort MessageId => Id;

        public double id;
        public int waitTime;

        public GameFightTurnStartMessage()
        {
        }
        public GameFightTurnStartMessage(double id,int waitTime)
        {
            this.id = id;
            this.waitTime = waitTime;
        }
        public override void Serialize(IDataWriter writer)
        {
            if (id < -9.00719925474099E+15 || id > 9.00719925474099E+15)
            {
                throw new System.Exception("Forbidden value (" + id + ") on element id.");
            }

            writer.WriteDouble((double)id);
            if (waitTime < 0)
            {
                throw new System.Exception("Forbidden value (" + waitTime + ") on element waitTime.");
            }

            writer.WriteVarInt((int)waitTime);
        }
        public override void Deserialize(IDataReader reader)
        {
            id = (double)reader.ReadDouble();
            if (id < -9.00719925474099E+15 || id > 9.00719925474099E+15)
            {
                throw new System.Exception("Forbidden value (" + id + ") on element of GameFightTurnStartMessage.id.");
            }

            waitTime = (int)reader.ReadVarUhInt();
            if (waitTime < 0)
            {
                throw new System.Exception("Forbidden value (" + waitTime + ") on element of GameFightTurnStartMessage.waitTime.");
            }

        }


    }
}








