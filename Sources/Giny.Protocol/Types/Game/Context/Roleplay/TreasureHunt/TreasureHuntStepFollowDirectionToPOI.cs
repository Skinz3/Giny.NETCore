using System.Collections.Generic;
using Giny.Core.IO.Interfaces;
using Giny.Protocol;
using Giny.Protocol.Enums;

namespace Giny.Protocol.Types
{ 
    public class TreasureHuntStepFollowDirectionToPOI : TreasureHuntStep  
    { 
        public new const ushort Id = 6869;
        public override ushort TypeId => Id;

        public byte direction;
        public short poiLabelId;

        public TreasureHuntStepFollowDirectionToPOI()
        {
        }
        public TreasureHuntStepFollowDirectionToPOI(byte direction,short poiLabelId)
        {
            this.direction = direction;
            this.poiLabelId = poiLabelId;
        }
        public override void Serialize(IDataWriter writer)
        {
            base.Serialize(writer);
            writer.WriteByte((byte)direction);
            if (poiLabelId < 0)
            {
                throw new System.Exception("Forbidden value (" + poiLabelId + ") on element poiLabelId.");
            }

            writer.WriteVarShort((short)poiLabelId);
        }
        public override void Deserialize(IDataReader reader)
        {
            base.Deserialize(reader);
            direction = (byte)reader.ReadByte();
            if (direction < 0)
            {
                throw new System.Exception("Forbidden value (" + direction + ") on element of TreasureHuntStepFollowDirectionToPOI.direction.");
            }

            poiLabelId = (short)reader.ReadVarUhShort();
            if (poiLabelId < 0)
            {
                throw new System.Exception("Forbidden value (" + poiLabelId + ") on element of TreasureHuntStepFollowDirectionToPOI.poiLabelId.");
            }

        }


    }
}








