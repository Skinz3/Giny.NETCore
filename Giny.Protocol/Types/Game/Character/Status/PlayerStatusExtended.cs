using System.Collections.Generic;
using Giny.Core.IO.Interfaces;
using Giny.Protocol;
using Giny.Protocol.Enums;

namespace Giny.Protocol.Types
{ 
    public class PlayerStatusExtended : PlayerStatus  
    { 
        public new const ushort Id = 1136;
        public override ushort TypeId => Id;

        public string message;

        public PlayerStatusExtended()
        {
        }
        public PlayerStatusExtended(string message,byte statusId)
        {
            this.message = message;
            this.statusId = statusId;
        }
        public override void Serialize(IDataWriter writer)
        {
            base.Serialize(writer);
            writer.WriteUTF((string)message);
        }
        public override void Deserialize(IDataReader reader)
        {
            base.Deserialize(reader);
            message = (string)reader.ReadUTF();
        }


    }
}








