using System.Collections.Generic;
using Giny.Core.IO.Interfaces;
using Giny.Protocol;
using Giny.Protocol.Enums;

namespace Giny.Protocol.Types
{ 
    public class TreasureHuntStepFollowDirectionToHint : TreasureHuntStep  
    { 
        public new const ushort Id = 895;
        public override ushort TypeId => Id;

        public byte direction;
        public short npcId;

        public TreasureHuntStepFollowDirectionToHint()
        {
        }
        public TreasureHuntStepFollowDirectionToHint(byte direction,short npcId)
        {
            this.direction = direction;
            this.npcId = npcId;
        }
        public override void Serialize(IDataWriter writer)
        {
            base.Serialize(writer);
            writer.WriteByte((byte)direction);
            if (npcId < 0)
            {
                throw new System.Exception("Forbidden value (" + npcId + ") on element npcId.");
            }

            writer.WriteVarShort((short)npcId);
        }
        public override void Deserialize(IDataReader reader)
        {
            base.Deserialize(reader);
            direction = (byte)reader.ReadByte();
            if (direction < 0)
            {
                throw new System.Exception("Forbidden value (" + direction + ") on element of TreasureHuntStepFollowDirectionToHint.direction.");
            }

            npcId = (short)reader.ReadVarUhShort();
            if (npcId < 0)
            {
                throw new System.Exception("Forbidden value (" + npcId + ") on element of TreasureHuntStepFollowDirectionToHint.npcId.");
            }

        }


    }
}








