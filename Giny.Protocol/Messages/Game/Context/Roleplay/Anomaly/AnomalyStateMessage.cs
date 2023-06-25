using System.Collections.Generic;
using Giny.Core.Network.Messages;
using Giny.Protocol.Types;
using Giny.Core.IO.Interfaces;
using Giny.Protocol;
using Giny.Protocol.Enums;

namespace Giny.Protocol.Messages
{ 
    public class AnomalyStateMessage : NetworkMessage  
    { 
        public  const ushort Id = 3282;
        public override ushort MessageId => Id;

        public short subAreaId;
        public bool open;
        public long closingTime;

        public AnomalyStateMessage()
        {
        }
        public AnomalyStateMessage(short subAreaId,bool open,long closingTime)
        {
            this.subAreaId = subAreaId;
            this.open = open;
            this.closingTime = closingTime;
        }
        public override void Serialize(IDataWriter writer)
        {
            if (subAreaId < 0)
            {
                throw new System.Exception("Forbidden value (" + subAreaId + ") on element subAreaId.");
            }

            writer.WriteVarShort((short)subAreaId);
            writer.WriteBoolean((bool)open);
            if (closingTime < 0 || closingTime > 9.00719925474099E+15)
            {
                throw new System.Exception("Forbidden value (" + closingTime + ") on element closingTime.");
            }

            writer.WriteVarLong((long)closingTime);
        }
        public override void Deserialize(IDataReader reader)
        {
            subAreaId = (short)reader.ReadVarUhShort();
            if (subAreaId < 0)
            {
                throw new System.Exception("Forbidden value (" + subAreaId + ") on element of AnomalyStateMessage.subAreaId.");
            }

            open = (bool)reader.ReadBoolean();
            closingTime = (long)reader.ReadVarUhLong();
            if (closingTime < 0 || closingTime > 9.00719925474099E+15)
            {
                throw new System.Exception("Forbidden value (" + closingTime + ") on element of AnomalyStateMessage.closingTime.");
            }

        }


    }
}








