using System.Collections.Generic;
using Giny.Core.Network.Messages;
using Giny.Protocol.Types;
using Giny.Core.IO.Interfaces;
using Giny.Protocol;
using Giny.Protocol.Enums;

namespace Giny.Protocol.Messages
{ 
    public class CreateGuildRankRequestMessage : NetworkMessage  
    { 
        public  const ushort Id = 6955;
        public override ushort MessageId => Id;

        public int parentRankId;
        public int gfxId;
        public string name;

        public CreateGuildRankRequestMessage()
        {
        }
        public CreateGuildRankRequestMessage(int parentRankId,int gfxId,string name)
        {
            this.parentRankId = parentRankId;
            this.gfxId = gfxId;
            this.name = name;
        }
        public override void Serialize(IDataWriter writer)
        {
            if (parentRankId < 0)
            {
                throw new System.Exception("Forbidden value (" + parentRankId + ") on element parentRankId.");
            }

            writer.WriteVarInt((int)parentRankId);
            if (gfxId < 0)
            {
                throw new System.Exception("Forbidden value (" + gfxId + ") on element gfxId.");
            }

            writer.WriteVarInt((int)gfxId);
            writer.WriteUTF((string)name);
        }
        public override void Deserialize(IDataReader reader)
        {
            parentRankId = (int)reader.ReadVarUhInt();
            if (parentRankId < 0)
            {
                throw new System.Exception("Forbidden value (" + parentRankId + ") on element of CreateGuildRankRequestMessage.parentRankId.");
            }

            gfxId = (int)reader.ReadVarUhInt();
            if (gfxId < 0)
            {
                throw new System.Exception("Forbidden value (" + gfxId + ") on element of CreateGuildRankRequestMessage.gfxId.");
            }

            name = (string)reader.ReadUTF();
        }


    }
}








