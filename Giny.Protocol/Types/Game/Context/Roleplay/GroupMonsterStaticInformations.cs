using System.Collections.Generic;
using Giny.Core.IO.Interfaces;
using Giny.Protocol;
using Giny.Protocol.Enums;

namespace Giny.Protocol.Types
{ 
    public class GroupMonsterStaticInformations  
    { 
        public const ushort Id = 1594;
        public virtual ushort TypeId => Id;

        public MonsterInGroupLightInformations mainCreatureLightInfos;
        public MonsterInGroupInformations[] underlings;

        public GroupMonsterStaticInformations()
        {
        }
        public GroupMonsterStaticInformations(MonsterInGroupLightInformations mainCreatureLightInfos,MonsterInGroupInformations[] underlings)
        {
            this.mainCreatureLightInfos = mainCreatureLightInfos;
            this.underlings = underlings;
        }
        public virtual void Serialize(IDataWriter writer)
        {
            mainCreatureLightInfos.Serialize(writer);
            writer.WriteShort((short)underlings.Length);
            for (uint _i2 = 0;_i2 < underlings.Length;_i2++)
            {
                (underlings[_i2] as MonsterInGroupInformations).Serialize(writer);
            }

        }
        public virtual void Deserialize(IDataReader reader)
        {
            MonsterInGroupInformations _item2 = null;
            mainCreatureLightInfos = new MonsterInGroupLightInformations();
            mainCreatureLightInfos.Deserialize(reader);
            uint _underlingsLen = (uint)reader.ReadUShort();
            for (uint _i2 = 0;_i2 < _underlingsLen;_i2++)
            {
                _item2 = new MonsterInGroupInformations();
                _item2.Deserialize(reader);
                underlings[_i2] = _item2;
            }

        }


    }
}








