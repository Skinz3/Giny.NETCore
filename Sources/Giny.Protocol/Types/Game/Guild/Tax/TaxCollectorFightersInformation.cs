using System.Collections.Generic;
using Giny.Core.IO.Interfaces;
using Giny.Protocol;
using Giny.Protocol.Enums;

namespace Giny.Protocol.Types
{ 
    public class TaxCollectorFightersInformation  
    { 
        public const ushort Id = 9834;
        public virtual ushort TypeId => Id;

        public double collectorId;
        public CharacterMinimalPlusLookInformations[] allyCharactersInformations;
        public CharacterMinimalPlusLookInformations[] enemyCharactersInformations;

        public TaxCollectorFightersInformation()
        {
        }
        public TaxCollectorFightersInformation(double collectorId,CharacterMinimalPlusLookInformations[] allyCharactersInformations,CharacterMinimalPlusLookInformations[] enemyCharactersInformations)
        {
            this.collectorId = collectorId;
            this.allyCharactersInformations = allyCharactersInformations;
            this.enemyCharactersInformations = enemyCharactersInformations;
        }
        public virtual void Serialize(IDataWriter writer)
        {
            if (collectorId < 0 || collectorId > 9.00719925474099E+15)
            {
                throw new System.Exception("Forbidden value (" + collectorId + ") on element collectorId.");
            }

            writer.WriteDouble((double)collectorId);
            writer.WriteShort((short)allyCharactersInformations.Length);
            for (uint _i2 = 0;_i2 < allyCharactersInformations.Length;_i2++)
            {
                writer.WriteShort((short)(allyCharactersInformations[_i2] as CharacterMinimalPlusLookInformations).TypeId);
                (allyCharactersInformations[_i2] as CharacterMinimalPlusLookInformations).Serialize(writer);
            }

            writer.WriteShort((short)enemyCharactersInformations.Length);
            for (uint _i3 = 0;_i3 < enemyCharactersInformations.Length;_i3++)
            {
                writer.WriteShort((short)(enemyCharactersInformations[_i3] as CharacterMinimalPlusLookInformations).TypeId);
                (enemyCharactersInformations[_i3] as CharacterMinimalPlusLookInformations).Serialize(writer);
            }

        }
        public virtual void Deserialize(IDataReader reader)
        {
            uint _id2 = 0;
            CharacterMinimalPlusLookInformations _item2 = null;
            uint _id3 = 0;
            CharacterMinimalPlusLookInformations _item3 = null;
            collectorId = (double)reader.ReadDouble();
            if (collectorId < 0 || collectorId > 9.00719925474099E+15)
            {
                throw new System.Exception("Forbidden value (" + collectorId + ") on element of TaxCollectorFightersInformation.collectorId.");
            }

            uint _allyCharactersInformationsLen = (uint)reader.ReadUShort();
            for (uint _i2 = 0;_i2 < _allyCharactersInformationsLen;_i2++)
            {
                _id2 = (uint)reader.ReadUShort();
                _item2 = ProtocolTypeManager.GetInstance<CharacterMinimalPlusLookInformations>((short)_id2);
                _item2.Deserialize(reader);
                allyCharactersInformations[_i2] = _item2;
            }

            uint _enemyCharactersInformationsLen = (uint)reader.ReadUShort();
            for (uint _i3 = 0;_i3 < _enemyCharactersInformationsLen;_i3++)
            {
                _id3 = (uint)reader.ReadUShort();
                _item3 = ProtocolTypeManager.GetInstance<CharacterMinimalPlusLookInformations>((short)_id3);
                _item3.Deserialize(reader);
                enemyCharactersInformations[_i3] = _item3;
            }

        }


    }
}








