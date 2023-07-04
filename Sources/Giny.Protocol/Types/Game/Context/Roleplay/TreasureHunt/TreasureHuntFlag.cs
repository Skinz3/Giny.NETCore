using System.Collections.Generic;
using Giny.Core.IO.Interfaces;
using Giny.Protocol;
using Giny.Protocol.Enums;

namespace Giny.Protocol.Types
{
    public class TreasureHuntFlag
    {
        public const ushort Id = 6796;
        public virtual ushort TypeId => Id;

        public double mapId;
        public byte state;

        public TreasureHuntFlag()
        {
        }
        public TreasureHuntFlag(double mapId, byte state)
        {
            this.mapId = mapId;
            this.state = state;
        }
        public virtual void Serialize(IDataWriter writer)
        {
            if (mapId < 0 || mapId > 9007199254740992)
            {
                throw new System.Exception("Forbidden value (" + mapId + ") on element mapId.");
            }

            writer.WriteDouble((double)mapId);
            writer.WriteByte((byte)state);
        }
        public virtual void Deserialize(IDataReader reader)
        {
            mapId = (double)reader.ReadDouble();
            if (mapId < 0 || mapId > 9007199254740992)
            {
                throw new System.Exception("Forbidden value (" + mapId + ") on element of TreasureHuntFlag.mapId.");
            }

            state = (byte)reader.ReadByte();
            if (state < 0)
            {
                throw new System.Exception("Forbidden value (" + state + ") on element of TreasureHuntFlag.state.");
            }

        }


    }
}


