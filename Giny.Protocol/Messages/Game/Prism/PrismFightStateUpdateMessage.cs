using System.Collections.Generic;
using Giny.Core.Network.Messages;
using Giny.Protocol.Types;
using Giny.Core.IO.Interfaces;
using Giny.Protocol;
using Giny.Protocol.Enums;

namespace Giny.Protocol.Messages
{ 
    public class PrismFightStateUpdateMessage : NetworkMessage  
    { 
        public  const ushort Id = 9539;
        public override ushort MessageId => Id;

        public byte state;

        public PrismFightStateUpdateMessage()
        {
        }
        public PrismFightStateUpdateMessage(byte state)
        {
            this.state = state;
        }
        public override void Serialize(IDataWriter writer)
        {
            if (state < 0)
            {
                throw new System.Exception("Forbidden value (" + state + ") on element state.");
            }

            writer.WriteByte((byte)state);
        }
        public override void Deserialize(IDataReader reader)
        {
            state = (byte)reader.ReadByte();
            if (state < 0)
            {
                throw new System.Exception("Forbidden value (" + state + ") on element of PrismFightStateUpdateMessage.state.");
            }

        }


    }
}








