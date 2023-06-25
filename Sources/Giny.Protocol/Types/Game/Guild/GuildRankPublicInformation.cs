using System.Collections.Generic;
using Giny.Core.IO.Interfaces;
using Giny.Protocol;
using Giny.Protocol.Enums;

namespace Giny.Protocol.Types
{ 
    public class GuildRankPublicInformation : GuildRankMinimalInformation  
    { 
        public new const ushort Id = 5645;
        public override ushort TypeId => Id;

        public int order;
        public int gfxId;

        public GuildRankPublicInformation()
        {
        }
        public GuildRankPublicInformation(int order,int gfxId,int id,string name)
        {
            this.order = order;
            this.gfxId = gfxId;
            this.id = id;
            this.name = name;
        }
        public override void Serialize(IDataWriter writer)
        {
            base.Serialize(writer);
            if (order < 0)
            {
                throw new System.Exception("Forbidden value (" + order + ") on element order.");
            }

            writer.WriteVarInt((int)order);
            if (gfxId < 0)
            {
                throw new System.Exception("Forbidden value (" + gfxId + ") on element gfxId.");
            }

            writer.WriteVarInt((int)gfxId);
        }
        public override void Deserialize(IDataReader reader)
        {
            base.Deserialize(reader);
            order = (int)reader.ReadVarUhInt();
            if (order < 0)
            {
                throw new System.Exception("Forbidden value (" + order + ") on element of GuildRankPublicInformation.order.");
            }

            gfxId = (int)reader.ReadVarUhInt();
            if (gfxId < 0)
            {
                throw new System.Exception("Forbidden value (" + gfxId + ") on element of GuildRankPublicInformation.gfxId.");
            }

        }


    }
}








