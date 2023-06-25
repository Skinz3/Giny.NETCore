using System.Collections.Generic;
using Giny.Core.Network.Messages;
using Giny.Protocol.Types;
using Giny.Core.IO.Interfaces;
using Giny.Protocol;
using Giny.Protocol.Enums;

namespace Giny.Protocol.Messages
{ 
    public class SlaveNoLongerControledMessage : NetworkMessage  
    { 
        public  const ushort Id = 9600;
        public override ushort MessageId => Id;

        public double masterId;
        public double slaveId;

        public SlaveNoLongerControledMessage()
        {
        }
        public SlaveNoLongerControledMessage(double masterId,double slaveId)
        {
            this.masterId = masterId;
            this.slaveId = slaveId;
        }
        public override void Serialize(IDataWriter writer)
        {
            if (masterId < -9.00719925474099E+15 || masterId > 9.00719925474099E+15)
            {
                throw new System.Exception("Forbidden value (" + masterId + ") on element masterId.");
            }

            writer.WriteDouble((double)masterId);
            if (slaveId < -9.00719925474099E+15 || slaveId > 9.00719925474099E+15)
            {
                throw new System.Exception("Forbidden value (" + slaveId + ") on element slaveId.");
            }

            writer.WriteDouble((double)slaveId);
        }
        public override void Deserialize(IDataReader reader)
        {
            masterId = (double)reader.ReadDouble();
            if (masterId < -9.00719925474099E+15 || masterId > 9.00719925474099E+15)
            {
                throw new System.Exception("Forbidden value (" + masterId + ") on element of SlaveNoLongerControledMessage.masterId.");
            }

            slaveId = (double)reader.ReadDouble();
            if (slaveId < -9.00719925474099E+15 || slaveId > 9.00719925474099E+15)
            {
                throw new System.Exception("Forbidden value (" + slaveId + ") on element of SlaveNoLongerControledMessage.slaveId.");
            }

        }


    }
}








