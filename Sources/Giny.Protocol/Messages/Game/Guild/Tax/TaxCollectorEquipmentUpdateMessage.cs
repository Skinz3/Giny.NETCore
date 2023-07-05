using System.Collections.Generic;
using Giny.Core.Network.Messages;
using Giny.Protocol.Types;
using Giny.Core.IO.Interfaces;
using Giny.Protocol;
using Giny.Protocol.Enums;

namespace Giny.Protocol.Messages
{
    public class TaxCollectorEquipmentUpdateMessage : NetworkMessage
    {
        public const ushort Id = 5895;
        public override ushort MessageId => Id;

        public double uniqueId;
        public ObjectItem @object;
        public bool added;
        public CharacterCharacteristics characteristics;

        public TaxCollectorEquipmentUpdateMessage()
        {
        }
        public TaxCollectorEquipmentUpdateMessage(double uniqueId, ObjectItem @object, bool added, CharacterCharacteristics characteristics)
        {
            this.uniqueId = uniqueId;
            this.@object = @object;
            this.added = added;
            this.characteristics = characteristics;
        }
        public override void Serialize(IDataWriter writer)
        {
            if (uniqueId < 0 || uniqueId > 9007199254740992)
            {
                throw new System.Exception("Forbidden value (" + uniqueId + ") on element uniqueId.");
            }

            writer.WriteDouble((double)uniqueId);
            @object.Serialize(writer);
            writer.WriteBoolean((bool)added);
            characteristics.Serialize(writer);
        }
        public override void Deserialize(IDataReader reader)
        {
            uniqueId = (double)reader.ReadDouble();
            if (uniqueId < 0 || uniqueId > 9007199254740992)
            {
                throw new System.Exception("Forbidden value (" + uniqueId + ") on element of TaxCollectorEquipmentUpdateMessage.uniqueId.");
            }

            @object = new ObjectItem();
            @object.Deserialize(reader);
            added = (bool)reader.ReadBoolean();
            characteristics = new CharacterCharacteristics();
            characteristics.Deserialize(reader);
        }

    }
}


