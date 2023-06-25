using System.Collections.Generic;
using Giny.Core.Network.Messages;
using Giny.Protocol.Types;
using Giny.Core.IO.Interfaces;
using Giny.Protocol;
using Giny.Protocol.Enums;

namespace Giny.Protocol.Messages
{ 
    public class GuildPaddockRemovedMessage : NetworkMessage  
    { 
        public  const ushort Id = 3096;
        public override ushort MessageId => Id;

        public double paddockId;

        public GuildPaddockRemovedMessage()
        {
        }
        public GuildPaddockRemovedMessage(double paddockId)
        {
            this.paddockId = paddockId;
        }
        public override void Serialize(IDataWriter writer)
        {
            if (paddockId < 0 || paddockId > 9.00719925474099E+15)
            {
                throw new System.Exception("Forbidden value (" + paddockId + ") on element paddockId.");
            }

            writer.WriteDouble((double)paddockId);
        }
        public override void Deserialize(IDataReader reader)
        {
            paddockId = (double)reader.ReadDouble();
            if (paddockId < 0 || paddockId > 9.00719925474099E+15)
            {
                throw new System.Exception("Forbidden value (" + paddockId + ") on element of GuildPaddockRemovedMessage.paddockId.");
            }

        }


    }
}








