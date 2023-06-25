using System.Collections.Generic;
using Giny.Core.IO.Interfaces;
using Giny.Protocol;
using Giny.Protocol.Enums;

namespace Giny.Protocol.Types
{ 
    public class DungeonPartyFinderPlayer  
    { 
        public const ushort Id = 1848;
        public virtual ushort TypeId => Id;

        public long playerId;
        public string playerName;
        public byte breed;
        public bool sex;
        public short level;

        public DungeonPartyFinderPlayer()
        {
        }
        public DungeonPartyFinderPlayer(long playerId,string playerName,byte breed,bool sex,short level)
        {
            this.playerId = playerId;
            this.playerName = playerName;
            this.breed = breed;
            this.sex = sex;
            this.level = level;
        }
        public virtual void Serialize(IDataWriter writer)
        {
            if (playerId < 0 || playerId > 9.00719925474099E+15)
            {
                throw new System.Exception("Forbidden value (" + playerId + ") on element playerId.");
            }

            writer.WriteVarLong((long)playerId);
            writer.WriteUTF((string)playerName);
            writer.WriteByte((byte)breed);
            writer.WriteBoolean((bool)sex);
            if (level < 0)
            {
                throw new System.Exception("Forbidden value (" + level + ") on element level.");
            }

            writer.WriteVarShort((short)level);
        }
        public virtual void Deserialize(IDataReader reader)
        {
            playerId = (long)reader.ReadVarUhLong();
            if (playerId < 0 || playerId > 9.00719925474099E+15)
            {
                throw new System.Exception("Forbidden value (" + playerId + ") on element of DungeonPartyFinderPlayer.playerId.");
            }

            playerName = (string)reader.ReadUTF();
            breed = (byte)reader.ReadByte();
            if (breed < (byte)PlayableBreedEnum.Feca || breed > (byte)PlayableBreedEnum.Ouginak)
            {
                throw new System.Exception("Forbidden value (" + breed + ") on element of DungeonPartyFinderPlayer.breed.");
            }

            sex = (bool)reader.ReadBoolean();
            level = (short)reader.ReadVarUhShort();
            if (level < 0)
            {
                throw new System.Exception("Forbidden value (" + level + ") on element of DungeonPartyFinderPlayer.level.");
            }

        }


    }
}








