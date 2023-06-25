using System.Collections.Generic;
using Giny.Core.Network.Messages;
using Giny.Protocol.Types;
using Giny.Core.IO.Interfaces;
using Giny.Protocol;
using Giny.Protocol.Enums;

namespace Giny.Protocol.Messages
{ 
    public class ShortcutBarSwapErrorMessage : NetworkMessage  
    { 
        public  const ushort Id = 9976;
        public override ushort MessageId => Id;

        public byte error;

        public ShortcutBarSwapErrorMessage()
        {
        }
        public ShortcutBarSwapErrorMessage(byte error)
        {
            this.error = error;
        }
        public override void Serialize(IDataWriter writer)
        {
            writer.WriteByte((byte)error);
        }
        public override void Deserialize(IDataReader reader)
        {
            error = (byte)reader.ReadByte();
            if (error < 0)
            {
                throw new System.Exception("Forbidden value (" + error + ") on element of ShortcutBarSwapSystem.ExceptionMessage.error.");
            }

        }


    }
}








