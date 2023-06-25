using System.Collections.Generic;
using Giny.Core.Network.Messages;
using Giny.Protocol.Types;
using Giny.Core.IO.Interfaces;
using Giny.Protocol;
using Giny.Protocol.Enums;

namespace Giny.Protocol.Messages
{ 
    public class AnomalySubareaInformationResponseMessage : NetworkMessage  
    { 
        public  const ushort Id = 8497;
        public override ushort MessageId => Id;

        public AnomalySubareaInformation[] subareas;

        public AnomalySubareaInformationResponseMessage()
        {
        }
        public AnomalySubareaInformationResponseMessage(AnomalySubareaInformation[] subareas)
        {
            this.subareas = subareas;
        }
        public override void Serialize(IDataWriter writer)
        {
            writer.WriteShort((short)subareas.Length);
            for (uint _i1 = 0;_i1 < subareas.Length;_i1++)
            {
                (subareas[_i1] as AnomalySubareaInformation).Serialize(writer);
            }

        }
        public override void Deserialize(IDataReader reader)
        {
            AnomalySubareaInformation _item1 = null;
            uint _subareasLen = (uint)reader.ReadUShort();
            for (uint _i1 = 0;_i1 < _subareasLen;_i1++)
            {
                _item1 = new AnomalySubareaInformation();
                _item1.Deserialize(reader);
                subareas[_i1] = _item1;
            }

        }


    }
}








