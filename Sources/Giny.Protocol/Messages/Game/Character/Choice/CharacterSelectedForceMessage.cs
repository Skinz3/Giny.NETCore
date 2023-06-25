using System.Collections.Generic;
using Giny.Core.Network.Messages;
using Giny.Protocol.Types;
using Giny.Core.IO.Interfaces;
using Giny.Protocol;
using Giny.Protocol.Enums;

namespace Giny.Protocol.Messages
{ 
    public class CharacterSelectedForceMessage : NetworkMessage  
    { 
        public  const ushort Id = 3302;
        public override ushort MessageId => Id;

        public int id;

        public CharacterSelectedForceMessage()
        {
        }
        public CharacterSelectedForceMessage(int id)
        {
            this.id = id;
        }
        public override void Serialize(IDataWriter writer)
        {
            if (id < 1 || id > 2147483647)
            {
                throw new System.Exception("Forbidden value (" + id + ") on element id.");
            }

            writer.WriteInt((int)id);
        }
        public override void Deserialize(IDataReader reader)
        {
            id = (int)reader.ReadInt();
            if (id < 1 || id > 2147483647)
            {
                throw new System.Exception("Forbidden value (" + id + ") on element of CharacterSelectedForceMessage.id.");
            }

        }


    }
}








