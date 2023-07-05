using System.Collections.Generic;
using Giny.Core.IO.Interfaces;
using Giny.Protocol;
using Giny.Protocol.Enums;

namespace Giny.Protocol.Types
{
    public class Uuid
    {
        public const ushort Id = 8529;
        public virtual ushort TypeId => Id;

        public string uuidString;

        public Uuid()
        {
        }
        public Uuid(string uuidString)
        {
            this.uuidString = uuidString;
        }
        public virtual void Serialize(IDataWriter writer)
        {
            writer.WriteUTF((string)uuidString);
        }
        public virtual void Deserialize(IDataReader reader)
        {
            uuidString = (string)reader.ReadUTF();
        }


    }
}


