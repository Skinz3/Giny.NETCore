using System.Collections.Generic;
using Giny.Core.IO.Interfaces;
using Giny.Protocol;
using Giny.Protocol.Enums;

namespace Giny.Protocol.Types
{
    public class AllianceInsiderPrismInformation : PrismInformation
    {
        public new const ushort Id = 4112;
        public override ushort TypeId => Id;

        public ObjectItem moduleObject;
        public int moduleType;
        public ObjectItem cristalObject;
        public int cristalType;
        public int cristalNumberLeft;

        public AllianceInsiderPrismInformation()
        {
        }
        public AllianceInsiderPrismInformation(ObjectItem moduleObject, int moduleType, ObjectItem cristalObject, int cristalType, int cristalNumberLeft, byte state, int placementDate, int nuggetsCount, int durability, double nextEvolutionDate)
        {
            this.moduleObject = moduleObject;
            this.moduleType = moduleType;
            this.cristalObject = cristalObject;
            this.cristalType = cristalType;
            this.cristalNumberLeft = cristalNumberLeft;
            this.state = state;
            this.placementDate = placementDate;
            this.nuggetsCount = nuggetsCount;
            this.durability = durability;
            this.nextEvolutionDate = nextEvolutionDate;
        }
        public override void Serialize(IDataWriter writer)
        {
            base.Serialize(writer);
            moduleObject.Serialize(writer);
            writer.WriteInt((int)moduleType);
            cristalObject.Serialize(writer);
            writer.WriteInt((int)cristalType);
            if (cristalNumberLeft < 0)
            {
                throw new System.Exception("Forbidden value (" + cristalNumberLeft + ") on element cristalNumberLeft.");
            }

            writer.WriteInt((int)cristalNumberLeft);
        }
        public override void Deserialize(IDataReader reader)
        {
            base.Deserialize(reader);
            moduleObject = new ObjectItem();
            moduleObject.Deserialize(reader);
            moduleType = (int)reader.ReadInt();
            cristalObject = new ObjectItem();
            cristalObject.Deserialize(reader);
            cristalType = (int)reader.ReadInt();
            cristalNumberLeft = (int)reader.ReadInt();
            if (cristalNumberLeft < 0)
            {
                throw new System.Exception("Forbidden value (" + cristalNumberLeft + ") on element of AllianceInsiderPrismInformation.cristalNumberLeft.");
            }

        }


    }
}


