using System.Collections.Generic;
using Giny.Core.IO.Interfaces;
using Giny.Protocol;
using Giny.Protocol.Enums;

namespace Giny.Protocol.Types
{ 
    public class GameContextSummonsInformation  
    { 
        public const ushort Id = 663;
        public virtual ushort TypeId => Id;

        public SpawnInformation spawnInformation;
        public byte wave;
        public EntityLook look;
        public GameFightCharacteristics stats;
        public GameContextBasicSpawnInformation[] summons;

        public GameContextSummonsInformation()
        {
        }
        public GameContextSummonsInformation(SpawnInformation spawnInformation,byte wave,EntityLook look,GameFightCharacteristics stats,GameContextBasicSpawnInformation[] summons)
        {
            this.spawnInformation = spawnInformation;
            this.wave = wave;
            this.look = look;
            this.stats = stats;
            this.summons = summons;
        }
        public virtual void Serialize(IDataWriter writer)
        {
            writer.WriteShort((short)spawnInformation.TypeId);
            spawnInformation.Serialize(writer);
            if (wave < 0)
            {
                throw new System.Exception("Forbidden value (" + wave + ") on element wave.");
            }

            writer.WriteByte((byte)wave);
            look.Serialize(writer);
            writer.WriteShort((short)stats.TypeId);
            stats.Serialize(writer);
            writer.WriteShort((short)summons.Length);
            for (uint _i5 = 0;_i5 < summons.Length;_i5++)
            {
                writer.WriteShort((short)(summons[_i5] as GameContextBasicSpawnInformation).TypeId);
                (summons[_i5] as GameContextBasicSpawnInformation).Serialize(writer);
            }

        }
        public virtual void Deserialize(IDataReader reader)
        {
            uint _id5 = 0;
            GameContextBasicSpawnInformation _item5 = null;
            uint _id1 = (uint)reader.ReadUShort();
            spawnInformation = ProtocolTypeManager.GetInstance<SpawnInformation>((short)_id1);
            spawnInformation.Deserialize(reader);
            wave = (byte)reader.ReadByte();
            if (wave < 0)
            {
                throw new System.Exception("Forbidden value (" + wave + ") on element of GameContextSummonsInformation.wave.");
            }

            look = new EntityLook();
            look.Deserialize(reader);
            uint _id4 = (uint)reader.ReadUShort();
            stats = ProtocolTypeManager.GetInstance<GameFightCharacteristics>((short)_id4);
            stats.Deserialize(reader);
            uint _summonsLen = (uint)reader.ReadUShort();
            for (uint _i5 = 0;_i5 < _summonsLen;_i5++)
            {
                _id5 = (uint)reader.ReadUShort();
                _item5 = ProtocolTypeManager.GetInstance<GameContextBasicSpawnInformation>((short)_id5);
                _item5.Deserialize(reader);
                summons[_i5] = _item5;
            }

        }


    }
}








