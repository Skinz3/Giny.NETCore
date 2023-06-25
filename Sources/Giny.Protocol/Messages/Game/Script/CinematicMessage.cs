using System.Collections.Generic;
using Giny.Core.Network.Messages;
using Giny.Protocol.Types;
using Giny.Core.IO.Interfaces;
using Giny.Protocol;
using Giny.Protocol.Enums;

namespace Giny.Protocol.Messages
{ 
    public class CinematicMessage : NetworkMessage  
    { 
        public  const ushort Id = 7716;
        public override ushort MessageId => Id;

        public short cinematicId;

        public CinematicMessage()
        {
        }
        public CinematicMessage(short cinematicId)
        {
            this.cinematicId = cinematicId;
        }
        public override void Serialize(IDataWriter writer)
        {
            if (cinematicId < 0)
            {
                throw new System.Exception("Forbidden value (" + cinematicId + ") on element cinematicId.");
            }

            writer.WriteVarShort((short)cinematicId);
        }
        public override void Deserialize(IDataReader reader)
        {
            cinematicId = (short)reader.ReadVarUhShort();
            if (cinematicId < 0)
            {
                throw new System.Exception("Forbidden value (" + cinematicId + ") on element of CinematicMessage.cinematicId.");
            }

        }


    }
}








