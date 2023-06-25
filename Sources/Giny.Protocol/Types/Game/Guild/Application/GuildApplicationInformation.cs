using System.Collections.Generic;
using Giny.Core.IO.Interfaces;
using Giny.Protocol;
using Giny.Protocol.Enums;

namespace Giny.Protocol.Types
{ 
    public class GuildApplicationInformation  
    { 
        public const ushort Id = 7531;
        public virtual ushort TypeId => Id;

        public ApplicationPlayerInformation playerInfo;
        public string applyText;
        public double creationDate;

        public GuildApplicationInformation()
        {
        }
        public GuildApplicationInformation(ApplicationPlayerInformation playerInfo,string applyText,double creationDate)
        {
            this.playerInfo = playerInfo;
            this.applyText = applyText;
            this.creationDate = creationDate;
        }
        public virtual void Serialize(IDataWriter writer)
        {
            playerInfo.Serialize(writer);
            writer.WriteUTF((string)applyText);
            if (creationDate < -9.00719925474099E+15 || creationDate > 9.00719925474099E+15)
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
            if (creationDate < -9.00719925474099E+15 || creationDate > 9.00719925474099E+15)
            {
                throw new System.Exception("Forbidden value (" + creationDate + ") on element of GuildApplicationInformation.creationDate.");
            }

        }


    }
}








