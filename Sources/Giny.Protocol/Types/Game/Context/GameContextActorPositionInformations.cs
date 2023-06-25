using System.Collections.Generic;
using Giny.Core.IO.Interfaces;
using Giny.Protocol;
using Giny.Protocol.Enums;

namespace Giny.Protocol.Types
{ 
    public class GameContextActorPositionInformations  
    { 
        public const ushort Id = 261;
        public virtual ushort TypeId => Id;

        public double contextualId;
        public EntityDispositionInformations disposition;

        public GameContextActorPositionInformations()
        {
        }
        public GameContextActorPositionInformations(double contextualId,EntityDispositionInformations disposition)
        {
            this.contextualId = contextualId;
            this.disposition = disposition;
        }
        public virtual void Serialize(IDataWriter writer)
        {
            if (contextualId < -9.00719925474099E+15 || contextualId > 9.00719925474099E+15)
            {
                throw new System.Exception("Forbidden value (" + contextualId + ") on element contextualId.");
            }

            writer.WriteDouble((double)contextualId);
            writer.WriteShort((short)disposition.TypeId);
            disposition.Serialize(writer);
        }
        public virtual void Deserialize(IDataReader reader)
        {
            contextualId = (double)reader.ReadDouble();
            if (contextualId < -9.00719925474099E+15 || contextualId > 9.00719925474099E+15)
            {
                throw new System.Exception("Forbidden value (" + contextualId + ") on element of GameContextActorPositionInformations.contextualId.");
            }

            uint _id2 = (uint)reader.ReadUShort();
            disposition = ProtocolTypeManager.GetInstance<EntityDispositionInformations>((short)_id2);
            disposition.Deserialize(reader);
        }


    }
}








