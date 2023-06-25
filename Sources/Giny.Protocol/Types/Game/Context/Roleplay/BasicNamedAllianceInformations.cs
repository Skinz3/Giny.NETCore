using System.Collections.Generic;
using Giny.Core.IO.Interfaces;
using Giny.Protocol;
using Giny.Protocol.Enums;

namespace Giny.Protocol.Types
{ 
    public class BasicNamedAllianceInformations : BasicAllianceInformations  
    { 
        public new const ushort Id = 1173;
        public override ushort TypeId => Id;

        public string allianceName;

        public BasicNamedAllianceInformations()
        {
        }
        public BasicNamedAllianceInformations(string allianceName,int allianceId,string allianceTag)
        {
            this.allianceName = allianceName;
            this.allianceId = allianceId;
            this.allianceTag = allianceTag;
        }
        public override void Serialize(IDataWriter writer)
        {
            base.Serialize(writer);
            writer.WriteUTF((string)allianceName);
        }
        public override void Deserialize(IDataReader reader)
        {
            base.Deserialize(reader);
            allianceName = (string)reader.ReadUTF();
        }


    }
}








