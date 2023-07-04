using System.Collections.Generic;
using Giny.Core.IO.Interfaces;
using Giny.Protocol;
using Giny.Protocol.Enums;

namespace Giny.Protocol.Types
{
    public class IdentifiedEntityDispositionInformations : EntityDispositionInformations
    {
        public new const ushort Id = 8082;
        public override ushort TypeId => Id;

        public double id;

        public IdentifiedEntityDispositionInformations()
        {
        }
        public IdentifiedEntityDispositionInformations(double id, short cellId, byte direction)
        {
            this.id = id;
            this.cellId = cellId;
            this.direction = direction;
        }
        public override void Serialize(IDataWriter writer)
        {
            base.Serialize(writer);
            if (id < -9007199254740992 || id > 9007199254740992)
            {
                throw new System.Exception("Forbidden value (" + id + ") on element id.");
            }

            writer.WriteDouble((double)id);
        }
        public override void Deserialize(IDataReader reader)
        {
            base.Deserialize(reader);
            id = (double)reader.ReadDouble();
            if (id < -9007199254740992 || id > 9007199254740992)
            {
                throw new System.Exception("Forbidden value (" + id + ") on element of IdentifiedEntityDispositionInformations.id.");
            }

        }


    }
}


