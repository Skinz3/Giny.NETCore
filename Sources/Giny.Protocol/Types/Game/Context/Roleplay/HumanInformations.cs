using System.Collections.Generic;
using Giny.Core.IO.Interfaces;
using Giny.Protocol;
using Giny.Protocol.Enums;

namespace Giny.Protocol.Types
{ 
    public class HumanInformations  
    { 
        public const ushort Id = 4912;
        public virtual ushort TypeId => Id;

        public ActorRestrictionsInformations restrictions;
        public bool sex;
        public HumanOption[] options;

        public HumanInformations()
        {
        }
        public HumanInformations(ActorRestrictionsInformations restrictions,bool sex,HumanOption[] options)
        {
            this.restrictions = restrictions;
            this.sex = sex;
            this.options = options;
        }
        public virtual void Serialize(IDataWriter writer)
        {
            restrictions.Serialize(writer);
            writer.WriteBoolean((bool)sex);
            writer.WriteShort((short)options.Length);
            for (uint _i3 = 0;_i3 < options.Length;_i3++)
            {
                writer.WriteShort((short)(options[_i3] as HumanOption).TypeId);
                (options[_i3] as HumanOption).Serialize(writer);
            }

        }
        public virtual void Deserialize(IDataReader reader)
        {
            uint _id3 = 0;
            HumanOption _item3 = null;
            restrictions = new ActorRestrictionsInformations();
            restrictions.Deserialize(reader);
            sex = (bool)reader.ReadBoolean();
            uint _optionsLen = (uint)reader.ReadUShort();
            for (uint _i3 = 0;_i3 < _optionsLen;_i3++)
            {
                _id3 = (uint)reader.ReadUShort();
                _item3 = ProtocolTypeManager.GetInstance<HumanOption>((short)_id3);
                _item3.Deserialize(reader);
                options[_i3] = _item3;
            }

        }


    }
}








