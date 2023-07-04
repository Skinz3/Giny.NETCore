using System.Collections.Generic;
using Giny.Core.IO.Interfaces;
using Giny.Protocol;
using Giny.Protocol.Enums;

namespace Giny.Protocol.Types
{
    public class SocialApplicationInformation
    {
        public const ushort Id = 2237;
        public virtual ushort TypeId => Id;

        public ApplicationPlayerInformation playerInfo;
        public string applyText;
        public double creationDate;

        public SocialApplicationInformation()
        {
        }
        public SocialApplicationInformation(ApplicationPlayerInformation playerInfo, string applyText, double creationDate)
        {
            this.playerInfo = playerInfo;
            this.applyText = applyText;
            this.creationDate = creationDate;
        }
        public virtual void Serialize(IDataWriter writer)
        {
            playerInfo.Serialize(writer);
            writer.WriteUTF((string)applyText);
            if (creationDate < -9007199254740992 || creationDate > 9007199254740992)
            {
                throw new System.Exception("Forbidden value (" + creationDate + ") on element creationDate.");
            }

            writer.WriteDouble((double)creationDate);
        }
        public virtual void Deserialize(IDataReader reader)
        {
            playerInfo = new ApplicationPlayerInformation();
            playerInfo.Deserialize(reader);
            applyText = (string)reader.ReadUTF();
            creationDate = (double)reader.ReadDouble();
            if (creationDate < -9007199254740992 || creationDate > 9007199254740992)
            {
                throw new System.Exception("Forbidden value (" + creationDate + ") on element of SocialApplicationInformation.creationDate.");
            }

        }


    }
}


