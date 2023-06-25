using System.Collections.Generic;
using Giny.Core.IO.Interfaces;
using Giny.Protocol;
using Giny.Protocol.Enums;

namespace Giny.Protocol.Types
{ 
    public class HumanOptionAlliance : HumanOption  
    { 
        public new const ushort Id = 6829;
        public override ushort TypeId => Id;

        public AllianceInformations allianceInformations;
        public byte aggressable;

        public HumanOptionAlliance()
        {
        }
        public HumanOptionAlliance(AllianceInformations allianceInformations,byte aggressable)
        {
            this.allianceInformations = allianceInformations;
            this.aggressable = aggressable;
        }
        public override void Serialize(IDataWriter writer)
        {
            base.Serialize(writer);
            allianceInformations.Serialize(writer);
            writer.WriteByte((byte)aggressable);
        }
        public override void Deserialize(IDataReader reader)
        {
            base.Deserialize(reader);
            allianceInformations = new AllianceInformations();
            allianceInformations.Deserialize(reader);
            aggressable = (byte)reader.ReadByte();
            if (aggressable < 0)
            {
                throw new System.Exception("Forbidden value (" + aggressable + ") on element of HumanOptionAlliance.aggressable.");
            }

        }


    }
}








