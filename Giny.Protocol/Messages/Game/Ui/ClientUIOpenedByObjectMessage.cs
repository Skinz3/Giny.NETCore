using System.Collections.Generic;
using Giny.Core.Network.Messages;
using Giny.Protocol.Types;
using Giny.Core.IO.Interfaces;
using Giny.Protocol;
using Giny.Protocol.Enums;

namespace Giny.Protocol.Messages
{ 
    public class ClientUIOpenedByObjectMessage : ClientUIOpenedMessage  
    { 
        public new const ushort Id = 9520;
        public override ushort MessageId => Id;

        public int uid;

        public ClientUIOpenedByObjectMessage()
        {
        }
        public ClientUIOpenedByObjectMessage(int uid,byte type)
        {
            this.uid = uid;
            this.type = type;
        }
        public override void Serialize(IDataWriter writer)
        {
            base.Serialize(writer);
            if (uid < 0)
            {
                throw new System.Exception("Forbidden value (" + uid + ") on element uid.");
            }

            writer.WriteVarInt((int)uid);
        }
        public override void Deserialize(IDataReader reader)
        {
            base.Deserialize(reader);
            uid = (int)reader.ReadVarUhInt();
            if (uid < 0)
            {
                throw new System.Exception("Forbidden value (" + uid + ") on element of ClientUIOpenedByObjectMessage.uid.");
            }

        }


    }
}








