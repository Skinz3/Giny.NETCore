using System.Collections.Generic;
using Giny.Core.Network.Messages;
using Giny.Protocol.Types;
using Giny.Core.IO.Interfaces;
using Giny.Protocol;
using Giny.Protocol.Enums;

namespace Giny.Protocol.Messages
{ 
    public class MountSterilizedMessage : NetworkMessage  
    { 
        public  const ushort Id = 9116;
        public override ushort MessageId => Id;

        public int mountId;

        public MountSterilizedMessage()
        {
        }
        public MountSterilizedMessage(int mountId)
        {
            this.mountId = mountId;
        }
        public override void Serialize(IDataWriter writer)
        {
            writer.WriteVarInt((int)mountId);
        }
        public override void Deserialize(IDataReader reader)
        {
            mountId = (int)reader.ReadVarInt();
        }


    }
}








