using System.Collections.Generic;
using Giny.Core.IO.Interfaces;
using Giny.Protocol;
using Giny.Protocol.Enums;

namespace Giny.Protocol.Types
{ 
    public class GameFightFighterEntityLightInformation : GameFightFighterLightInformations  
    { 
        public new const ushort Id = 8680;
        public override ushort TypeId => Id;

        public byte entityModelId;
        public double masterId;

        public GameFightFighterEntityLightInformation()
        {
        }
        public GameFightFighterEntityLightInformation(byte entityModelId,double masterId,double id,byte wave,short level,byte breed,bool sex,bool alive)
        {
            this.entityModelId = entityModelId;
            this.masterId = masterId;
            this.id = id;
            this.wave = wave;
            this.level = level;
            this.breed = breed;
            this.sex = sex;
            this.alive = alive;
        }
        public override void Serialize(IDataWriter writer)
        {
            base.Serialize(writer);
            if (entityModelId < 0)
            {
                throw new System.Exception("Forbidden value (" + entityModelId + ") on element entityModelId.");
            }

            writer.WriteByte((byte)entityModelId);
            if (masterId < -9.00719925474099E+15 || masterId > 9.00719925474099E+15)
            {
                throw new System.Exception("Forbidden value (" + masterId + ") on element masterId.");
            }

            writer.WriteDouble((double)masterId);
        }
        public override void Deserialize(IDataReader reader)
        {
            base.Deserialize(reader);
            entityModelId = (byte)reader.ReadByte();
            if (entityModelId < 0)
            {
                throw new System.Exception("Forbidden value (" + entityModelId + ") on element of GameFightFighterEntityLightInformation.entityModelId.");
            }

            masterId = (double)reader.ReadDouble();
            if (masterId < -9.00719925474099E+15 || masterId > 9.00719925474099E+15)
            {
                throw new System.Exception("Forbidden value (" + masterId + ") on element of GameFightFighterEntityLightInformation.masterId.");
            }

        }


    }
}








