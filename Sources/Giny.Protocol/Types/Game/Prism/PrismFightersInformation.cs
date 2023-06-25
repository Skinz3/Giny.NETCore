using System.Collections.Generic;
using Giny.Core.IO.Interfaces;
using Giny.Protocol;
using Giny.Protocol.Enums;

namespace Giny.Protocol.Types
{ 
    public class PrismFightersInformation  
    { 
        public const ushort Id = 9984;
        public virtual ushort TypeId => Id;

        public short subAreaId;
        public ProtectedEntityWaitingForHelpInfo waitingForHelpInfo;
        public CharacterMinimalPlusLookInformations[] allyCharactersInformations;
        public CharacterMinimalPlusLookInformations[] enemyCharactersInformations;

        public PrismFightersInformation()
        {
        }
        public PrismFightersInformation(short subAreaId,ProtectedEntityWaitingForHelpInfo waitingForHelpInfo,CharacterMinimalPlusLookInformations[] allyCharactersInformations,CharacterMinimalPlusLookInformations[] enemyCharactersInformations)
        {
            this.subAreaId = subAreaId;
            this.waitingForHelpInfo = waitingForHelpInfo;
            this.allyCharactersInformations = allyCharactersInformations;
            this.enemyCharactersInformations = enemyCharactersInformations;
        }
        public virtual void Serialize(IDataWriter writer)
        {
            if (subAreaId < 0)
            {
                throw new System.Exception("Forbidden value (" + subAreaId + ") on element subAreaId.");
            }

            writer.WriteVarShort((short)subAreaId);
            waitingForHelpInfo.Serialize(writer);
            writer.WriteShort((short)allyCharactersInformations.Length);
            for (uint _i3 = 0;_i3 < allyCharactersInformations.Length;_i3++)
            {
                writer.WriteShort((short)(allyCharactersInformations[_i3] as CharacterMinimalPlusLookInformations).TypeId);
                (allyCharactersInformations[_i3] as CharacterMinimalPlusLookInformations).Serialize(writer);
            }

            writer.WriteShort((short)enemyCharactersInformations.Length);
            for (uint _i4 = 0;_i4 < enemyCharactersInformations.Length;_i4++)
            {
                writer.WriteShort((short)(enemyCharactersInformations[_i4] as CharacterMinimalPlusLookInformations).TypeId);
                (enemyCharactersInformations[_i4] as CharacterMinimalPlusLookInformations).Serialize(writer);
            }

        }
        public virtual void Deserialize(IDataReader reader)
        {
            uint _id3 = 0;
            CharacterMinimalPlusLookInformations _item3 = null;
            uint _id4 = 0;
            CharacterMinimalPlusLookInformations _item4 = null;
            subAreaId = (short)reader.ReadVarUhShort();
            if (subAreaId < 0)
            {
                throw new System.Exception("Forbidden value (" + subAreaId + ") on element of PrismFightersInformation.subAreaId.");
            }

            waitingForHelpInfo = new ProtectedEntityWaitingForHelpInfo();
            waitingForHelpInfo.Deserialize(reader);
            uint _allyCharactersInformationsLen = (uint)reader.ReadUShort();
            for (uint _i3 = 0;_i3 < _allyCharactersInformationsLen;_i3++)
            {
                _id3 = (uint)reader.ReadUShort();
                _item3 = ProtocolTypeManager.GetInstance<CharacterMinimalPlusLookInformations>((short)_id3);
                _item3.Deserialize(reader);
                allyCharactersInformations[_i3] = _item3;
            }

            uint _enemyCharactersInformationsLen = (uint)reader.ReadUShort();
            for (uint _i4 = 0;_i4 < _enemyCharactersInformationsLen;_i4++)
            {
                _id4 = (uint)reader.ReadUShort();
                _item4 = ProtocolTypeManager.GetInstance<CharacterMinimalPlusLookInformations>((short)_id4);
                _item4.Deserialize(reader);
                enemyCharactersInformations[_i4] = _item4;
            }

        }


    }
}








