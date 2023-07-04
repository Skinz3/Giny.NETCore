using System.Collections.Generic;
using Giny.Core.IO.Interfaces;
using Giny.Protocol;
using Giny.Protocol.Enums;

namespace Giny.Protocol.Types
{
    public class uuid
    {
        public const ushort Id = 8744;
        public virtual ushort TypeId => Id;

        public string uuidString;

        public uuid()
        {
        }
        public uuid(string uuidString)
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


