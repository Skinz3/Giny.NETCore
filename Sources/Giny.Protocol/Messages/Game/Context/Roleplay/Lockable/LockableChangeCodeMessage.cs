using System.Collections.Generic;
using Giny.Core.Network.Messages;
using Giny.Protocol.Types;
using Giny.Core.IO.Interfaces;
using Giny.Protocol;
using Giny.Protocol.Enums;

namespace Giny.Protocol.Messages
{ 
    public class LockableChangeCodeMessage : NetworkMessage  
    { 
        public  const ushort Id = 1788;
        public override ushort MessageId => Id;

        public string code;

        public LockableChangeCodeMessage()
        {
        }
        public LockableChangeCodeMessage(string code)
        {
            this.code = code;
        }
        public override void Serialize(IDataWriter writer)
        {
            writer.WriteUTF((string)code);
        }
        public override void Deserialize(IDataReader reader)
        {
            code = (string)reader.ReadUTF();
        }


    }
}








