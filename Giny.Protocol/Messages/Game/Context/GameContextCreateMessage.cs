using System.Collections.Generic;
using Giny.Core.Network.Messages;
using Giny.Protocol.Types;
using Giny.Core.IO.Interfaces;
using Giny.Protocol;
using Giny.Protocol.Enums;

namespace Giny.Protocol.Messages
{ 
    public class GameContextCreateMessage : NetworkMessage  
    { 
        public  const ushort Id = 4452;
        public override ushort MessageId => Id;

        public byte context;

        public GameContextCreateMessage()
        {
        }
        public GameContextCreateMessage(byte context)
        {
            this.context = context;
        }
        public override void Serialize(IDataWriter writer)
        {
            writer.WriteByte((byte)context);
        }
        public override void Deserialize(IDataReader reader)
        {
            context = (byte)reader.ReadByte();
            if (context < 0)
            {
                throw new System.Exception("Forbidden value (" + context + ") on element of GameContextCreateMessage.context.");
            }

        }


    }
}








