using System.Collections.Generic;
using Giny.Core.Network.Messages;
using Giny.Protocol.Types;
using Giny.Core.IO.Interfaces;
using Giny.Protocol;
using Giny.Protocol.Enums;

namespace Giny.Protocol.Messages
{ 
    public class RemoveListenerOnSynchronizedStorageMessage : NetworkMessage  
    { 
        public  const ushort Id = 107;
        public override ushort MessageId => Id;

        public string player;

        public RemoveListenerOnSynchronizedStorageMessage()
        {
        }
        public RemoveListenerOnSynchronizedStorageMessage(string player)
        {
            this.player = player;
        }
        public override void Serialize(IDataWriter writer)
        {
            writer.WriteUTF((string)player);
        }
        public override void Deserialize(IDataReader reader)
        {
            player = (string)reader.ReadUTF();
        }


    }
}








