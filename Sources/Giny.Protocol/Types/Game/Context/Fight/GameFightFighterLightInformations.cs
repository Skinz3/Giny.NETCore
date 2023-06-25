using System.Collections.Generic;
using Giny.Core.IO.Interfaces;
using Giny.Protocol;
using Giny.Protocol.Enums;

namespace Giny.Protocol.Types
{ 
    public class GameFightFighterLightInformations  
    { 
        public const ushort Id = 3196;
        public virtual ushort TypeId => Id;

        public double id;
        public byte wave;
        public short level;
        public byte breed;
        public bool sex;
        public bool alive;

        public GameFightFighterLightInformations()
        {
        }
        public GameFightFighterLightInformations(double id,byte wave,short level,byte breed,bool sex,bool alive)
        {
            this.id = id;
            this.wave = wave;
            this.level = level;
            this.breed = breed;
            this.sex = sex;
            this.alive = alive;
        }
        public virtual void Serialize(IDataWriter writer)
        {
            byte _box0 = 0;
            _box0 = BooleanByteWrapper.SetFlag(_box0,0,sex);
            _box0 = BooleanByteWrapper.SetFlag(_box0,1,alive);
            writer.WriteByte((byte)_box0);
            if (id < -9.00719925474099E+15 || id > 9.00719925474099E+15)
            {
                throw new System.Exception("Forbidden value (" + id + ") on element id.");
            }

            writer.WriteDouble((double)id);
            if (wave < 0)
            {
                throw new System.Exception("Forbidden value (" + wave + ") on element wave.");
            }

            writer.WriteByte((byte)wave);
            if (level < 0)
            {
                throw new System.Exception("Forbidden value (" + level + ") on element level.");
            }

            writer.WriteVarShort((short)level);
            writer.WriteByte((byte)breed);
        }
        public virtual void Deserialize(IDataReader reader)
        {
            byte _box0 = reader.ReadByte();
            sex = BooleanByteWrapper.GetFlag(_box0,0);
            alive = BooleanByteWrapper.GetFlag(_box0,1);
            id = (double)reader.ReadDouble();
            if (id < -9.00719925474099E+15 || id > 9.00719925474099E+15)
            {
                throw new System.Exception("Forbidden value (" + id + ") on element of GameFightFighterLightInformations.id.");
            }

            wave = (byte)reader.ReadByte();
            if (wave < 0)
            {
                throw new System.Exception("Forbidden value (" + wave + ") on element of GameFightFighterLightInformations.wave.");
            }

            level = (short)reader.ReadVarUhShort();
            if (level < 0)
            {
                throw new System.Exception("Forbidden value (" + level + ") on element of GameFightFighterLightInformations.level.");
            }

            breed = (byte)reader.ReadByte();
        }


    }
}








