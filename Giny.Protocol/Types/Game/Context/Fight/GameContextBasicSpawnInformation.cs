using System.Collections.Generic;
using Giny.Core.IO.Interfaces;
using Giny.Protocol;
using Giny.Protocol.Enums;

namespace Giny.Protocol.Types
{ 
    public class GameContextBasicSpawnInformation  
    { 
        public const ushort Id = 7887;
        public virtual ushort TypeId => Id;

        public byte teamId;
        public bool alive;
        public GameContextActorPositionInformations informations;

        public GameContextBasicSpawnInformation()
        {
        }
        public GameContextBasicSpawnInformation(byte teamId,bool alive,GameContextActorPositionInformations informations)
        {
            this.teamId = teamId;
            this.alive = alive;
            this.informations = informations;
        }
        public virtual void Serialize(IDataWriter writer)
        {
            writer.WriteByte((byte)teamId);
            writer.WriteBoolean((bool)alive);
            writer.WriteShort((short)informations.TypeId);
            informations.Serialize(writer);
        }
        public virtual void Deserialize(IDataReader reader)
        {
            teamId = (byte)reader.ReadByte();
            if (teamId < 0)
            {
                throw new System.Exception("Forbidden value (" + teamId + ") on element of GameContextBasicSpawnInformation.teamId.");
            }

            alive = (bool)reader.ReadBoolean();
            uint _id3 = (uint)reader.ReadUShort();
            informations = ProtocolTypeManager.GetInstance<GameContextActorPositionInformations>((short)_id3);
            informations.Deserialize(reader);
        }


    }
}








