using System.Collections.Generic;
using Giny.Core.Network.Messages;
using Giny.Protocol.Types;
using Giny.Core.IO.Interfaces;
using Giny.Protocol;
using Giny.Protocol.Enums;

namespace Giny.Protocol.Messages
{ 
    public class CharacterExperienceGainMessage : NetworkMessage  
    { 
        public  const ushort Id = 1383;
        public override ushort MessageId => Id;

        public long experienceCharacter;
        public long experienceMount;
        public long experienceGuild;
        public long experienceIncarnation;

        public CharacterExperienceGainMessage()
        {
        }
        public CharacterExperienceGainMessage(long experienceCharacter,long experienceMount,long experienceGuild,long experienceIncarnation)
        {
            this.experienceCharacter = experienceCharacter;
            this.experienceMount = experienceMount;
            this.experienceGuild = experienceGuild;
            this.experienceIncarnation = experienceIncarnation;
        }
        public override void Serialize(IDataWriter writer)
        {
            if (experienceCharacter < 0 || experienceCharacter > 9.00719925474099E+15)
            {
                throw new System.Exception("Forbidden value (" + experienceCharacter + ") on element experienceCharacter.");
            }

            writer.WriteVarLong((long)experienceCharacter);
            if (experienceMount < 0 || experienceMount > 9.00719925474099E+15)
            {
                throw new System.Exception("Forbidden value (" + experienceMount + ") on element experienceMount.");
            }

            writer.WriteVarLong((long)experienceMount);
            if (experienceGuild < 0 || experienceGuild > 9.00719925474099E+15)
            {
                throw new System.Exception("Forbidden value (" + experienceGuild + ") on element experienceGuild.");
            }

            writer.WriteVarLong((long)experienceGuild);
            if (experienceIncarnation < 0 || experienceIncarnation > 9.00719925474099E+15)
            {
                throw new System.Exception("Forbidden value (" + experienceIncarnation + ") on element experienceIncarnation.");
            }

            writer.WriteVarLong((long)experienceIncarnation);
        }
        public override void Deserialize(IDataReader reader)
        {
            experienceCharacter = (long)reader.ReadVarUhLong();
            if (experienceCharacter < 0 || experienceCharacter > 9.00719925474099E+15)
            {
                throw new System.Exception("Forbidden value (" + experienceCharacter + ") on element of CharacterExperienceGainMessage.experienceCharacter.");
            }

            experienceMount = (long)reader.ReadVarUhLong();
            if (experienceMount < 0 || experienceMount > 9.00719925474099E+15)
            {
                throw new System.Exception("Forbidden value (" + experienceMount + ") on element of CharacterExperienceGainMessage.experienceMount.");
            }

            experienceGuild = (long)reader.ReadVarUhLong();
            if (experienceGuild < 0 || experienceGuild > 9.00719925474099E+15)
            {
                throw new System.Exception("Forbidden value (" + experienceGuild + ") on element of CharacterExperienceGainMessage.experienceGuild.");
            }

            experienceIncarnation = (long)reader.ReadVarUhLong();
            if (experienceIncarnation < 0 || experienceIncarnation > 9.00719925474099E+15)
            {
                throw new System.Exception("Forbidden value (" + experienceIncarnation + ") on element of CharacterExperienceGainMessage.experienceIncarnation.");
            }

        }


    }
}








