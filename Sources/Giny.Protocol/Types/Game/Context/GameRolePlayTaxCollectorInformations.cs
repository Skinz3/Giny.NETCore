using System.Collections.Generic;
using Giny.Core.IO.Interfaces;
using Giny.Protocol;
using Giny.Protocol.Enums;

namespace Giny.Protocol.Types
{
    public class GameRolePlayTaxCollectorInformations : GameRolePlayActorInformations
    {
        public new const ushort Id = 7225;
        public override ushort TypeId => Id;

        public TaxCollectorStaticInformations identification;
        public int taxCollectorAttack;

        public GameRolePlayTaxCollectorInformations()
        {
        }
        public GameRolePlayTaxCollectorInformations(TaxCollectorStaticInformations identification, int taxCollectorAttack, double contextualId, EntityDispositionInformations disposition, EntityLook look)
        {
            this.identification = identification;
            this.taxCollectorAttack = taxCollectorAttack;
            this.contextualId = contextualId;
            this.disposition = disposition;
            this.look = look;
        }
        public override void Serialize(IDataWriter writer)
        {
            base.Serialize(writer);
            writer.WriteShort((short)identification.TypeId);
            identification.Serialize(writer);
            writer.WriteInt((int)taxCollectorAttack);
        }
        public override void Deserialize(IDataReader reader)
        {
            base.Deserialize(reader);
            uint _id1 = (uint)reader.ReadUShort();
            identification = ProtocolTypeManager.GetInstance<TaxCollectorStaticInformations>((short)_id1);
            identification.Deserialize(reader);
            taxCollectorAttack = (int)reader.ReadInt();
        }


    }
}


