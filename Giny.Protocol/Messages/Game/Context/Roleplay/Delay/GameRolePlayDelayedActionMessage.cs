using System.Collections.Generic;
using Giny.Core.Network.Messages;
using Giny.Protocol.Types;
using Giny.Core.IO.Interfaces;
using Giny.Protocol;
using Giny.Protocol.Enums;

namespace Giny.Protocol.Messages
{ 
    public class GameRolePlayDelayedActionMessage : NetworkMessage  
    { 
        public  const ushort Id = 7263;
        public override ushort MessageId => Id;

        public double delayedCharacterId;
        public byte delayTypeId;
        public double delayEndTime;

        public GameRolePlayDelayedActionMessage()
        {
        }
        public GameRolePlayDelayedActionMessage(double delayedCharacterId,byte delayTypeId,double delayEndTime)
        {
            this.delayedCharacterId = delayedCharacterId;
            this.delayTypeId = delayTypeId;
            this.delayEndTime = delayEndTime;
        }
        public override void Serialize(IDataWriter writer)
        {
            if (delayedCharacterId < -9.00719925474099E+15 || delayedCharacterId > 9.00719925474099E+15)
            {
                throw new System.Exception("Forbidden value (" + delayedCharacterId + ") on element delayedCharacterId.");
            }

            writer.WriteDouble((double)delayedCharacterId);
            writer.WriteByte((byte)delayTypeId);
            if (delayEndTime < 0 || delayEndTime > 9.00719925474099E+15)
            {
                throw new System.Exception("Forbidden value (" + delayEndTime + ") on element delayEndTime.");
            }

            writer.WriteDouble((double)delayEndTime);
        }
        public override void Deserialize(IDataReader reader)
        {
            delayedCharacterId = (double)reader.ReadDouble();
            if (delayedCharacterId < -9.00719925474099E+15 || delayedCharacterId > 9.00719925474099E+15)
            {
                throw new System.Exception("Forbidden value (" + delayedCharacterId + ") on element of GameRolePlayDelayedActionMessage.delayedCharacterId.");
            }

            delayTypeId = (byte)reader.ReadByte();
            if (delayTypeId < 0)
            {
                throw new System.Exception("Forbidden value (" + delayTypeId + ") on element of GameRolePlayDelayedActionMessage.delayTypeId.");
            }

            delayEndTime = (double)reader.ReadDouble();
            if (delayEndTime < 0 || delayEndTime > 9.00719925474099E+15)
            {
                throw new System.Exception("Forbidden value (" + delayEndTime + ") on element of GameRolePlayDelayedActionMessage.delayEndTime.");
            }

        }


    }
}








