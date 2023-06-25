using System.Collections.Generic;
using Giny.Core.IO.Interfaces;
using Giny.Protocol;
using Giny.Protocol.Enums;

namespace Giny.Protocol.Types
{ 
    public class PlayerSearchTagInformation : AbstractPlayerSearchInformation  
    { 
        public new const ushort Id = 9839;
        public override ushort TypeId => Id;

        public AccountTagInformation tag;

        public PlayerSearchTagInformation()
        {
        }
        public PlayerSearchTagInformation(AccountTagInformation tag)
        {
            this.tag = tag;
        }
        public override void Serialize(IDataWriter writer)
        {
            base.Serialize(writer);
            tag.Serialize(writer);
        }
        public override void Deserialize(IDataReader reader)
        {
            base.Deserialize(reader);
            tag = new AccountTagInformation();
            tag.Deserialize(reader);
        }


    }
}








