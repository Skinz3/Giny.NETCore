using System.Collections.Generic;
using Giny.Core.IO.Interfaces;
using Giny.Protocol;
using Giny.Protocol.Enums;

namespace Giny.Protocol.Types
{
    public class SocialFight
    {
        public const ushort Id = 7744;
        public virtual ushort TypeId => Id;

        public SocialFightInfo socialFightInfo;
        public CharacterMinimalPlusLookInformations[] attackers;
        public CharacterMinimalPlusLookInformations[] defenders;
        public FightPhase phase;

        public SocialFight()
        {
        }
        public SocialFight(SocialFightInfo socialFightInfo, CharacterMinimalPlusLookInformations[] attackers, CharacterMinimalPlusLookInformations[] defenders, FightPhase phase)
        {
            this.socialFightInfo = socialFightInfo;
            this.attackers = attackers;
            this.defenders = defenders;
            this.phase = phase;
        }
        public virtual void Serialize(IDataWriter writer)
        {
            socialFightInfo.Serialize(writer);
            writer.WriteShort((short)attackers.Length);
            for (uint _i2 = 0; _i2 < attackers.Length; _i2++)
            {
                (attackers[_i2] as CharacterMinimalPlusLookInformations).Serialize(writer);
            }

            writer.WriteShort((short)defenders.Length);
            for (uint _i3 = 0; _i3 < defenders.Length; _i3++)
            {
                (defenders[_i3] as CharacterMinimalPlusLookInformations).Serialize(writer);
            }

            phase.Serialize(writer);
        }
        public virtual void Deserialize(IDataReader reader)
        {
            CharacterMinimalPlusLookInformations _item2 = null;
            CharacterMinimalPlusLookInformations _item3 = null;
            socialFightInfo = new SocialFightInfo();
            socialFightInfo.Deserialize(reader);
            uint _attackersLen = (uint)reader.ReadUShort();
            for (uint _i2 = 0; _i2 < _attackersLen; _i2++)
            {
                _item2 = new CharacterMinimalPlusLookInformations();
                _item2.Deserialize(reader);
                attackers[_i2] = _item2;
            }

            uint _defendersLen = (uint)reader.ReadUShort();
            for (uint _i3 = 0; _i3 < _defendersLen; _i3++)
            {
                _item3 = new CharacterMinimalPlusLookInformations();
                _item3.Deserialize(reader);
                defenders[_i3] = _item3;
            }

            phase = new FightPhase();
            phase.Deserialize(reader);
        }


    }
}


