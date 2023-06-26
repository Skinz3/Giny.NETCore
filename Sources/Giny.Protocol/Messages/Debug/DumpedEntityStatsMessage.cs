using System.Collections.Generic;
using Giny.Core.Network.Messages;
using Giny.Protocol.Types;
using Giny.Core.IO.Interfaces;
using Giny.Protocol;
using Giny.Protocol.Enums;

namespace Giny.Protocol.Messages
{ 
    public class DumpedEntityStatsMessage : NetworkMessage  
    { 
        public  const ushort Id = 120;
        public override ushort MessageId => Id;

        public double actorId;
        public CharacterCharacteristics stats;

        public DumpedEntityStatsMessage()
        {
        }
        public DumpedEntityStatsMessage(double actorId,CharacterCharacteristics stats)
        {
            this.actorId = actorId;
            this.stats = stats;
        }
        public override void Serialize(IDataWriter writer)
        {
            if (actorId < -9.00719925474099E+15 || actorId > 9.00719925474099E+15)
            {
                throw new System.Exception("Forbidden value (" + actorId + ") on element actorId.");
            }

            writer.WriteDouble((double)actorId);
            stats.Serialize(writer);
        }
        public override void Deserialize(IDataReader reader)
        {
            actorId = (double)reader.ReadDouble();
            if (actorId < -9.00719925474099E+15 || actorId > 9.00719925474099E+15)
            {
                throw new System.Exception("Forbidden value (" + actorId + ") on element of DumpedEntityStatsMessage.actorId.");
            }

            stats = new CharacterCharacteristics();
            stats.Deserialize(reader);
        }


    }
}








