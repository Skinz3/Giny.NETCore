using System.Collections.Generic;
using Giny.Core.IO.Interfaces;
using Giny.Protocol;
using Giny.Protocol.Enums;

namespace Giny.Protocol.Types
{
    public class ObjectItemToSellInNpcShop : ObjectItemMinimalInformation
    {
        public new const ushort Id = 6501;
        public override ushort TypeId => Id;

        public long objectPrice;
        public string buyCriterion;

        public ObjectItemToSellInNpcShop()
        {
        }
        public ObjectItemToSellInNpcShop(long objectPrice, string buyCriterion, int objectGID, ObjectEffect[] effects)
        {
            this.objectPrice = objectPrice;
            this.buyCriterion = buyCriterion;
            this.objectGID = objectGID;
            this.effects = effects;
        }
        public override void Serialize(IDataWriter writer)
        {
            base.Serialize(writer);
            if (objectPrice < 0 || objectPrice > 9007199254740992)
            {
                throw new System.Exception("Forbidden value (" + objectPrice + ") on element objectPrice.");
            }

            writer.WriteVarLong((long)objectPrice);
            writer.WriteUTF((string)buyCriterion);
        }
        public override void Deserialize(IDataReader reader)
        {
            base.Deserialize(reader);
            objectPrice = (long)reader.ReadVarUhLong();
            if (objectPrice < 0 || objectPrice > 9007199254740992)
            {
                throw new System.Exception("Forbidden value (" + objectPrice + ") on element of ObjectItemToSellInNpcShop.objectPrice.");
            }

            buyCriterion = (string)reader.ReadUTF();
        }


    }
}


