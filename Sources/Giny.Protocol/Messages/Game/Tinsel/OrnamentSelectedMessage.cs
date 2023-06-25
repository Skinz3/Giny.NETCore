using System.Collections.Generic;
using Giny.Core.Network.Messages;
using Giny.Protocol.Types;
using Giny.Core.IO.Interfaces;
using Giny.Protocol;
using Giny.Protocol.Enums;

namespace Giny.Protocol.Messages
{ 
    public class OrnamentSelectedMessage : NetworkMessage  
    { 
        public  const ushort Id = 7210;
        public override ushort MessageId => Id;

        public short ornamentId;

        public OrnamentSelectedMessage()
        {
        }
        public OrnamentSelectedMessage(short ornamentId)
        {
            this.ornamentId = ornamentId;
        }
        public override void Serialize(IDataWriter writer)
        {
            if (ornamentId < 0)
            {
                throw new System.Exception("Forbidden value (" + ornamentId + ") on element ornamentId.");
            }

            writer.WriteVarShort((short)ornamentId);
        }
        public override void Deserialize(IDataReader reader)
        {
            ornamentId = (short)reader.ReadVarUhShort();
            if (ornamentId < 0)
            {
                throw new System.Exception("Forbidden value (" + ornamentId + ") on element of OrnamentSelectedMessage.ornamentId.");
            }

        }


    }
}








