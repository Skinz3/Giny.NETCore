using System.Collections.Generic;
using Giny.Core.Network.Messages;
using Giny.Protocol.Types;
using Giny.Core.IO.Interfaces;
using Giny.Protocol;
using Giny.Protocol.Enums;

namespace Giny.Protocol.Messages
{ 
    public class MountEmoteIconUsedOkMessage : NetworkMessage  
    { 
        public  const ushort Id = 6579;
        public override ushort MessageId => Id;

        public int mountId;
        public byte reactionType;

        public MountEmoteIconUsedOkMessage()
        {
        }
        public MountEmoteIconUsedOkMessage(int mountId,byte reactionType)
        {
            this.mountId = mountId;
            this.reactionType = reactionType;
        }
        public override void Serialize(IDataWriter writer)
        {
            writer.WriteVarInt((int)mountId);
            if (reactionType < 0)
            {
                throw new System.Exception("Forbidden value (" + reactionType + ") on element reactionType.");
            }

            writer.WriteByte((byte)reactionType);
        }
        public override void Deserialize(IDataReader reader)
        {
            mountId = (int)reader.ReadVarInt();
            reactionType = (byte)reader.ReadByte();
            if (reactionType < 0)
            {
                throw new System.Exception("Forbidden value (" + reactionType + ") on element of MountEmoteIconUsedOkMessage.reactionType.");
            }

        }


    }
}








