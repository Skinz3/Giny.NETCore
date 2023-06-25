using System.Collections.Generic;
using Giny.Core.IO.Interfaces;
using Giny.Protocol;
using Giny.Protocol.Enums;

namespace Giny.Protocol.Types
{ 
    public class GameFightCharacteristics  
    { 
        public const ushort Id = 3807;
        public virtual ushort TypeId => Id;

        public CharacterCharacteristics characteristics;
        public double summoner;
        public bool summoned;
        public byte invisibilityState;

        public GameFightCharacteristics()
        {
        }
        public GameFightCharacteristics(CharacterCharacteristics characteristics,double summoner,bool summoned,byte invisibilityState)
        {
            this.characteristics = characteristics;
            this.summoner = summoner;
            this.summoned = summoned;
            this.invisibilityState = invisibilityState;
        }
        public virtual void Serialize(IDataWriter writer)
        {
            characteristics.Serialize(writer);
            if (summoner < -9.00719925474099E+15 || summoner > 9.00719925474099E+15)
            {
                throw new System.Exception("Forbidden value (" + summoner + ") on element summoner.");
            }

            writer.WriteDouble((double)summoner);
            writer.WriteBoolean((bool)summoned);
            writer.WriteByte((byte)invisibilityState);
        }
        public virtual void Deserialize(IDataReader reader)
        {
            characteristics = new CharacterCharacteristics();
            characteristics.Deserialize(reader);
            summoner = (double)reader.ReadDouble();
            if (summoner < -9.00719925474099E+15 || summoner > 9.00719925474099E+15)
            {
                throw new System.Exception("Forbidden value (" + summoner + ") on element of GameFightCharacteristics.summoner.");
            }

            summoned = (bool)reader.ReadBoolean();
            invisibilityState = (byte)reader.ReadByte();
            if (invisibilityState < 0)
            {
                throw new System.Exception("Forbidden value (" + invisibilityState + ") on element of GameFightCharacteristics.invisibilityState.");
            }

        }


    }
}








