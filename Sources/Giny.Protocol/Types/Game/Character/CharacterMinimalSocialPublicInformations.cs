using System.Collections.Generic;
using Giny.Core.IO.Interfaces;
using Giny.Protocol;
using Giny.Protocol.Enums;

namespace Giny.Protocol.Types
{
    public class CharacterMinimalSocialPublicInformations : CharacterMinimalInformations
    {
        public new const ushort Id = 4700;
        public override ushort TypeId => Id;

        public RankPublicInformation rank;

        public CharacterMinimalSocialPublicInformations()
        {
        }
        public CharacterMinimalSocialPublicInformations(RankPublicInformation rank, long id, string name, short level)
        {
            this.rank = rank;
            this.id = id;
            this.name = name;
            this.level = level;
        }
        public override void Serialize(IDataWriter writer)
        {
            base.Serialize(writer);
            rank.Serialize(writer);
        }
        public override void Deserialize(IDataReader reader)
        {
            base.Deserialize(reader);
            rank = new RankPublicInformation();
            rank.Deserialize(reader);
        }


    }
}


