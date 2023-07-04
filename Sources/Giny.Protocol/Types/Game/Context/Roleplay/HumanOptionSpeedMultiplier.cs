using System.Collections.Generic;
using Giny.Core.IO.Interfaces;
using Giny.Protocol;
using Giny.Protocol.Enums;

namespace Giny.Protocol.Types
{
    public class HumanOptionSpeedMultiplier : HumanOption
    {
        public new const ushort Id = 3553;
        public override ushort TypeId => Id;

        public int speedMultiplier;

        public HumanOptionSpeedMultiplier()
        {
        }
        public HumanOptionSpeedMultiplier(int speedMultiplier)
        {
            this.speedMultiplier = speedMultiplier;
        }
        public override void Serialize(IDataWriter writer)
        {
            base.Serialize(writer);
            if (speedMultiplier < 0)
            {
                throw new System.Exception("Forbidden value (" + speedMultiplier + ") on element speedMultiplier.");
            }

            writer.WriteVarInt((int)speedMultiplier);
        }
        public override void Deserialize(IDataReader reader)
        {
            base.Deserialize(reader);
            speedMultiplier = (int)reader.ReadVarUhInt();
            if (speedMultiplier < 0)
            {
                throw new System.Exception("Forbidden value (" + speedMultiplier + ") on element of HumanOptionSpeedMultiplier.speedMultiplier.");
            }

        }


    }
}


