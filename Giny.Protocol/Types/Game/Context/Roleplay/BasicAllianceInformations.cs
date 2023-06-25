using System.Collections.Generic;
using Giny.Core.IO.Interfaces;
using Giny.Protocol;
using Giny.Protocol.Enums;

namespace Giny.Protocol.Types
{ 
    public class BasicAllianceInformations : AbstractSocialGroupInfos  
    { 
        public new const ushort Id = 5732;
        public override ushort TypeId => Id;

        public int allianceId;
        public string allianceTag;

        public BasicAllianceInformations()
        {
        }
        public BasicAllianceInformations(int allianceId,string allianceTag)
        {
            this.allianceId = allianceId;
            this.allianceTag = allianceTag;
        }
        public override void Serialize(IDataWriter writer)
        {
            base.Serialize(writer);
            if (allianceId < 0)
            {
                throw new System.Exception("Forbidden value (" + allianceId + ") on element allianceId.");
            }

            writer.WriteVarInt((int)allianceId);
            writer.WriteUTF((string)allianceTag);
        }
        public override void Deserialize(IDataReader reader)
        {
            base.Deserialize(reader);
            allianceId = (int)reader.ReadVarUhInt();
            if (allianceId < 0)
            {
                throw new System.Exception("Forbidden value (" + allianceId + ") on element of BasicAllianceInformations.allianceId.");
            }

            allianceTag = (string)reader.ReadUTF();
        }


    }
}








