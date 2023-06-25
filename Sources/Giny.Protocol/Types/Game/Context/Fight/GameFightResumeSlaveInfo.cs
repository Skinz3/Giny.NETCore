using System.Collections.Generic;
using Giny.Core.IO.Interfaces;
using Giny.Protocol;
using Giny.Protocol.Enums;

namespace Giny.Protocol.Types
{ 
    public class GameFightResumeSlaveInfo  
    { 
        public const ushort Id = 7784;
        public virtual ushort TypeId => Id;

        public double slaveId;
        public GameFightSpellCooldown[] spellCooldowns;
        public byte summonCount;
        public byte bombCount;

        public GameFightResumeSlaveInfo()
        {
        }
        public GameFightResumeSlaveInfo(double slaveId,GameFightSpellCooldown[] spellCooldowns,byte summonCount,byte bombCount)
        {
            this.slaveId = slaveId;
            this.spellCooldowns = spellCooldowns;
            this.summonCount = summonCount;
            this.bombCount = bombCount;
        }
        public virtual void Serialize(IDataWriter writer)
        {
            if (slaveId < -9.00719925474099E+15 || slaveId > 9.00719925474099E+15)
            {
                throw new System.Exception("Forbidden value (" + slaveId + ") on element slaveId.");
            }

            writer.WriteDouble((double)slaveId);
            writer.WriteShort((short)spellCooldowns.Length);
            for (uint _i2 = 0;_i2 < spellCooldowns.Length;_i2++)
            {
                (spellCooldowns[_i2] as GameFightSpellCooldown).Serialize(writer);
            }

            if (summonCount < 0)
            {
                throw new System.Exception("Forbidden value (" + summonCount + ") on element summonCount.");
            }

            writer.WriteByte((byte)summonCount);
            if (bombCount < 0)
            {
                throw new System.Exception("Forbidden value (" + bombCount + ") on element bombCount.");
            }

            writer.WriteByte((byte)bombCount);
        }
        public virtual void Deserialize(IDataReader reader)
        {
            GameFightSpellCooldown _item2 = null;
            slaveId = (double)reader.ReadDouble();
            if (slaveId < -9.00719925474099E+15 || slaveId > 9.00719925474099E+15)
            {
                throw new System.Exception("Forbidden value (" + slaveId + ") on element of GameFightResumeSlaveInfo.slaveId.");
            }

            uint _spellCooldownsLen = (uint)reader.ReadUShort();
            for (uint _i2 = 0;_i2 < _spellCooldownsLen;_i2++)
            {
                _item2 = new GameFightSpellCooldown();
                _item2.Deserialize(reader);
                spellCooldowns[_i2] = _item2;
            }

            summonCount = (byte)reader.ReadByte();
            if (summonCount < 0)
            {
                throw new System.Exception("Forbidden value (" + summonCount + ") on element of GameFightResumeSlaveInfo.summonCount.");
            }

            bombCount = (byte)reader.ReadByte();
            if (bombCount < 0)
            {
                throw new System.Exception("Forbidden value (" + bombCount + ") on element of GameFightResumeSlaveInfo.bombCount.");
            }

        }


    }
}








