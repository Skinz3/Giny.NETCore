using System.Collections.Generic;
using Giny.Core.IO.Interfaces;
using Giny.Protocol;
using Giny.Protocol.Enums;

namespace Giny.Protocol.Types
{
    public class FightLootObject
    {
        public const ushort Id = 4940;
        public virtual ushort TypeId => Id;

        public int objectId;
        public int quantity;
        public int priorityHint;

        public FightLootObject()
        {
        }
        public FightLootObject(int objectId, int quantity, int priorityHint)
        {
            this.objectId = objectId;
            this.quantity = quantity;
            this.priorityHint = priorityHint;
        }
        public virtual void Serialize(IDataWriter writer)
        {
            writer.WriteInt((int)objectId);
            writer.WriteInt((int)quantity);
            writer.WriteInt((int)priorityHint);
        }
        public virtual void Deserialize(IDataReader reader)
        {
            objectId = (int)reader.ReadInt();
            quantity = (int)reader.ReadInt();
            priorityHint = (int)reader.ReadInt();
        }


    }
}


