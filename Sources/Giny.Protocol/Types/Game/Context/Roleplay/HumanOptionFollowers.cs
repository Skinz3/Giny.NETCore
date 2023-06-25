using System.Collections.Generic;
using Giny.Core.IO.Interfaces;
using Giny.Protocol;
using Giny.Protocol.Enums;

namespace Giny.Protocol.Types
{ 
    public class HumanOptionFollowers : HumanOption  
    { 
        public new const ushort Id = 6006;
        public override ushort TypeId => Id;

        public IndexedEntityLook[] followingCharactersLook;

        public HumanOptionFollowers()
        {
        }
        public HumanOptionFollowers(IndexedEntityLook[] followingCharactersLook)
        {
            this.followingCharactersLook = followingCharactersLook;
        }
        public override void Serialize(IDataWriter writer)
        {
            base.Serialize(writer);
            writer.WriteShort((short)followingCharactersLook.Length);
            for (uint _i1 = 0;_i1 < followingCharactersLook.Length;_i1++)
            {
                (followingCharactersLook[_i1] as IndexedEntityLook).Serialize(writer);
            }

        }
        public override void Deserialize(IDataReader reader)
        {
            IndexedEntityLook _item1 = null;
            base.Deserialize(reader);
            uint _followingCharactersLookLen = (uint)reader.ReadUShort();
            for (uint _i1 = 0;_i1 < _followingCharactersLookLen;_i1++)
            {
                _item1 = new IndexedEntityLook();
                _item1.Deserialize(reader);
                followingCharactersLook[_i1] = _item1;
            }

        }


    }
}








