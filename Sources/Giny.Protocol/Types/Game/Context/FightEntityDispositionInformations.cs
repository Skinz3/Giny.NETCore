using System.Collections.Generic;
using Giny.Core.IO.Interfaces;
using Giny.Protocol;
using Giny.Protocol.Enums;

namespace Giny.Protocol.Types
{
    public class FightEntityDispositionInformations : EntityDispositionInformations
    {
        public new const ushort Id = 8497;
        public override ushort TypeId => Id;

        public double carryingCharacterId;

        public FightEntityDispositionInformations()
        {
        }
        public FightEntityDispositionInformations(double carryingCharacterId, short cellId, byte direction)
        {
            this.carryingCharacterId = carryingCharacterId;
            this.cellId = cellId;
            this.direction = direction;
        }
        public override void Serialize(IDataWriter writer)
        {
            base.Serialize(writer);
            if (carryingCharacterId < -9007199254740992 || carryingCharacterId > 9007199254740992)
            {
                throw new System.Exception("Forbidden value (" + carryingCharacterId + ") on element carryingCharacterId.");
            }

            writer.WriteDouble((double)carryingCharacterId);
        }
        public override void Deserialize(IDataReader reader)
        {
            base.Deserialize(reader);
            carryingCharacterId = (double)reader.ReadDouble();
            if (carryingCharacterId < -9007199254740992 || carryingCharacterId > 9007199254740992)
            {
                throw new System.Exception("Forbidden value (" + carryingCharacterId + ") on element of FightEntityDispositionInformations.carryingCharacterId.");
            }

        }


    }
}


