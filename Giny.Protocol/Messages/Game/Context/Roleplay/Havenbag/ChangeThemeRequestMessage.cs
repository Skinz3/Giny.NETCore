using System.Collections.Generic;
using Giny.Core.Network.Messages;
using Giny.Protocol.Types;
using Giny.Core.IO.Interfaces;
using Giny.Protocol;
using Giny.Protocol.Enums;

namespace Giny.Protocol.Messages
{ 
    public class ChangeThemeRequestMessage : NetworkMessage  
    { 
        public  const ushort Id = 4904;
        public override ushort MessageId => Id;

        public byte theme;

        public ChangeThemeRequestMessage()
        {
        }
        public ChangeThemeRequestMessage(byte theme)
        {
            this.theme = theme;
        }
        public override void Serialize(IDataWriter writer)
        {
            writer.WriteByte((byte)theme);
        }
        public override void Deserialize(IDataReader reader)
        {
            theme = (byte)reader.ReadByte();
        }


    }
}








