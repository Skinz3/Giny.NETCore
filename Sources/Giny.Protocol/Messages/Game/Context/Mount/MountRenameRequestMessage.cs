using System.Collections.Generic;
using Giny.Core.Network.Messages;
using Giny.Protocol.Types;
using Giny.Core.IO.Interfaces;
using Giny.Protocol;
using Giny.Protocol.Enums;

namespace Giny.Protocol.Messages
{ 
    public class MountRenameRequestMessage : NetworkMessage  
    { 
        public  const ushort Id = 2190;
        public override ushort MessageId => Id;

        public string name;
        public int mountId;

        public MountRenameRequestMessage()
        {
        }
        public MountRenameRequestMessage(string name,int mountId)
        {
            this.name = name;
            this.mountId = mountId;
        }
        public override void Serialize(IDataWriter writer)
        {
            writer.WriteUTF((string)name);
            writer.WriteVarInt((int)mountId);
        }
        public override void Deserialize(IDataReader reader)
        {
            name = (string)reader.ReadUTF();
            mountId = (int)reader.ReadVarInt();
        }


    }
}








