using System.Collections.Generic;
using Giny.Core.IO.Interfaces;
using Giny.Protocol;
using Giny.Protocol.Enums;

namespace Giny.Protocol.Types
{ 
    public class GuildRankInformation : GuildRankMinimalInformation  
    { 
        public new const ushort Id = 742;
        public override ushort TypeId => Id;

        public int order;
        public int gfxId;
        public bool modifiable;
        public int[] rights;

        public GuildRankInformation()
        {
        }
        public GuildRankInformation(int order,int gfxId,bool modifiable,int[] rights,int id,string name)
        {
            this.order = order;
            this.gfxId = gfxId;
            this.modifiable = modifiable;
            this.rights = rights;
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
            writer.WriteBoolean((bool)modifiable);
            writer.WriteShort((short)rights.Length);
            for (uint _i4 = 0;_i4 < rights.Length;_i4++)
            {
                if (rights[_i4] < 0)
                {
                    throw new System.Exception("Forbidden value (" + rights[_i4] + ") on element 4 (starting at 1) of rights.");
                }

                writer.WriteVarInt((int)rights[_i4]);
            }

        }
        public override void Deserialize(IDataReader reader)
        {
            uint _val4 = 0;
            base.Deserialize(reader);
            order = (int)reader.ReadVarUhInt();
            if (order < 0)
            {
                throw new System.Exception("Forbidden value (" + order + ") on element of GuildRankInformation.order.");
            }

            gfxId = (int)reader.ReadVarUhInt();
            if (gfxId < 0)
            {
                throw new System.Exception("Forbidden value (" + gfxId + ") on element of GuildRankInformation.gfxId.");
            }

            modifiable = (bool)reader.ReadBoolean();
            uint _rightsLen = (uint)reader.ReadUShort();
            rights = new int[_rightsLen];
            for (uint _i4 = 0;_i4 < _rightsLen;_i4++)
            {
                _val4 = (uint)reader.ReadVarUhInt();
                if (_val4 < 0)
                {
                    throw new System.Exception("Forbidden value (" + _val4 + ") on elements of rights.");
                }

                rights[_i4] = (int)_val4;
            }

        }


    }
}








