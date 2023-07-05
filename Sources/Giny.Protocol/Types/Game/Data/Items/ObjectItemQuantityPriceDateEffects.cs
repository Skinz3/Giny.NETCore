using System.Collections.Generic;
using Giny.Core.IO.Interfaces;
using Giny.Protocol;
using Giny.Protocol.Enums;

namespace Giny.Protocol.Types
{
    public class ObjectItemQuantityPriceDateEffects : ObjectItemGenericQuantity
    {
        public new const ushort Id = 7856;
        public override ushort TypeId => Id;

        public long price;
        public ObjectEffects effects;
        public int date;

        public ObjectItemQuantityPriceDateEffects()
        {
        }
        public ObjectItemQuantityPriceDateEffects(long price, ObjectEffects effects, int date, int objectGID, int quantity)
        {
            this.price = price;
            this.effects = effects;
            this.date = date;
            this.objectGID = objectGID;
            this.quantity = quantity;
        }
        public override void Serialize(IDataWriter writer)
        {
            base.Serialize(writer);
            if (price < 0 || price > 9007199254740992)
            {
                throw new System.Exception("Forbidden value (" + price + ") on element price.");
            }

            writer.WriteVarLong((long)price);
            effects.Serialize(writer);
            if (date < 0)
            {
                throw new System.Exception("Forbidden value (" + date + ") on element date.");
            }

            writer.WriteInt((int)date);
        }
        public override void Deserialize(IDataReader reader)
        {
            base.Deserialize(reader);
            price = (long)reader.ReadVarUhLong();
            if (price < 0 || price > 9007199254740992)
            {
                throw new System.Exception("Forbidden value (" + price + ") on element of ObjectItemQuantityPriceDateEffects.price.");
            }

            effects = new ObjectEffects();
            effects.Deserialize(reader);
            date = (int)reader.ReadInt();
            if (date < 0)
            {
                throw new System.Exception("Forbidden value (" + date + ") on element of ObjectItemQuantityPriceDateEffects.date.");
            }

        }


    }
}


