using System.Collections.Generic;
using Giny.Core.IO.Interfaces;
using Giny.Protocol;
using Giny.Protocol.Enums;

namespace Giny.Protocol.Types
{ 
    public class AccountTagInformation  
    { 
        public const ushort Id = 7235;
        public virtual ushort TypeId => Id;

        public string nickname;
        public string tagNumber;

        public AccountTagInformation()
        {
        }
        public AccountTagInformation(string nickname,string tagNumber)
        {
            this.nickname = nickname;
            this.tagNumber = tagNumber;
        }
        public virtual void Serialize(IDataWriter writer)
        {
            writer.WriteUTF((string)nickname);
            writer.WriteUTF((string)tagNumber);
        }
        public virtual void Deserialize(IDataReader reader)
        {
            nickname = (string)reader.ReadUTF();
            tagNumber = (string)reader.ReadUTF();
        }


    }
}








