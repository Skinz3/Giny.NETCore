using System.Collections.Generic;
using Giny.Core.IO.Interfaces;
using Giny.Protocol;
using Giny.Protocol.Enums;

namespace Giny.Protocol.Types
{ 
    public class GuildRankMinimalInformation  
    { 
        public const ushort Id = 362;
        public virtual ushort TypeId => Id;

        public int id;
        public string name;

        public GuildRankMinimalInformation()
        {
        }
        public GuildRankMinimalInformation(int id,string name)
        {
            this.id = id;
            this.name = name;
        }
        public virtual void Serialize(IDataWriter writer)
        {
            if (id < 0)
            {
                throw new System.Exception("Forbidden value (" + id + ") on element id.");
            }

            writer.WriteVarInt((int)id);
            writer.WriteUTF((string)name);
        }
        public virtual void Deserialize(IDataReader reader)
        {
            id = (int)reader.ReadVarUhInt();
            if (id < 0)
            {
                throw new System.Exception("Forbidden value (" + id + ") on element of GuildRankMinimalInformation.id.");
            }

            name = (string)reader.ReadUTF();
        }


    }
}








