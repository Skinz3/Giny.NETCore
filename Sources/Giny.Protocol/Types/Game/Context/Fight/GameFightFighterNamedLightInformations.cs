using System.Collections.Generic;
using Giny.Core.IO.Interfaces;
using Giny.Protocol;
using Giny.Protocol.Enums;

namespace Giny.Protocol.Types
{ 
    public class GameFightFighterNamedLightInformations : GameFightFighterLightInformations  
    { 
        public new const ushort Id = 7053;
        public override ushort TypeId => Id;

        public string name;

        public GameFightFighterNamedLightInformations()
        {
        }
        public GameFightFighterNamedLightInformations(string name,double id,byte wave,short level,byte breed,bool sex,bool alive)
        {
            this.name = name;
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
            writer.WriteUTF((string)name);
        }
        public override void Deserialize(IDataReader reader)
        {
            base.Deserialize(reader);
            name = (string)reader.ReadUTF();
        }


    }
}








