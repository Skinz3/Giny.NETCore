using System.Collections.Generic;
using Giny.Core.IO.Interfaces;
using Giny.Protocol;
using Giny.Protocol.Enums;

namespace Giny.Protocol.Types
{
    public class HumanOptionAlliance : HumanOption
    {
        public new const ushort Id = 5811;
        public override ushort TypeId => Id;

        public AllianceInformation allianceInformation;
        public byte aggressable;

        public HumanOptionAlliance()
        {
        }
        public HumanOptionAlliance(AllianceInformation allianceInformation, byte aggressable)
        {
            this.allianceInformation = allianceInformation;
            this.aggressable = aggressable;
        }
        public override void Serialize(IDataWriter writer)
        {
            base.Serialize(writer);
            allianceInformation.Serialize(writer);
            writer.WriteByte((byte)aggressable);
        }
        public override void Deserialize(IDataReader reader)
        {
            base.Deserialize(reader);
            allianceInformation = new AllianceInformation();
            allianceInformation.Deserialize(reader);
            aggressable = (byte)reader.ReadByte();
            if (aggressable < 0)
            {
                throw new System.Exception("Forbidden value (" + aggressable + ") on element of HumanOptionAlliance.aggressable.");
            }

        }


    }
}


