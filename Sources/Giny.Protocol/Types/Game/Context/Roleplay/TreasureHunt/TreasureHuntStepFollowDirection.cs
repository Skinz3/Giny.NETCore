using System.Collections.Generic;
using Giny.Core.IO.Interfaces;
using Giny.Protocol;
using Giny.Protocol.Enums;

namespace Giny.Protocol.Types
{ 
    public class TreasureHuntStepFollowDirection : TreasureHuntStep  
    { 
        public new const ushort Id = 437;
        public override ushort TypeId => Id;

        public byte direction;
        public short mapCount;

        public TreasureHuntStepFollowDirection()
        {
        }
        public TreasureHuntStepFollowDirection(byte direction,short mapCount)
        {
            this.direction = direction;
            this.mapCount = mapCount;
        }
        public override void Serialize(IDataWriter writer)
        {
            base.Serialize(writer);
            writer.WriteByte((byte)direction);
            if (mapCount < 0)
            {
                throw new System.Exception("Forbidden value (" + mapCount + ") on element mapCount.");
            }

            writer.WriteVarShort((short)mapCount);
        }
        public override void Deserialize(IDataReader reader)
        {
            base.Deserialize(reader);
            direction = (byte)reader.ReadByte();
            if (direction < 0)
            {
                throw new System.Exception("Forbidden value (" + direction + ") on element of TreasureHuntStepFollowDirection.direction.");
            }

            mapCount = (short)reader.ReadVarUhShort();
            if (mapCount < 0)
            {
                throw new System.Exception("Forbidden value (" + mapCount + ") on element of TreasureHuntStepFollowDirection.mapCount.");
            }

        }


    }
}








