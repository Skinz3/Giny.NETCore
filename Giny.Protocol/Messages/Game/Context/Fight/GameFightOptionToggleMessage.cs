using System.Collections.Generic;
using Giny.Core.Network.Messages;
using Giny.Protocol.Types;
using Giny.Core.IO.Interfaces;
using Giny.Protocol;
using Giny.Protocol.Enums;

namespace Giny.Protocol.Messages
{ 
    public class GameFightOptionToggleMessage : NetworkMessage  
    { 
        public  const ushort Id = 1236;
        public override ushort MessageId => Id;

        public byte option;

        public GameFightOptionToggleMessage()
        {
        }
        public GameFightOptionToggleMessage(byte option)
        {
            this.option = option;
        }
        public override void Serialize(IDataWriter writer)
        {
            writer.WriteByte((byte)option);
        }
        public override void Deserialize(IDataReader reader)
        {
            option = (byte)reader.ReadByte();
            if (option < 0)
            {
                throw new System.Exception("Forbidden value (" + option + ") on element of GameFightOptionToggleMessage.option.");
            }

        }


    }
}








