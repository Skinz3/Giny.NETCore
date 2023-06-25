using System.Collections.Generic;
using Giny.Core.Network.Messages;
using Giny.Protocol.Types;
using Giny.Core.IO.Interfaces;
using Giny.Protocol;
using Giny.Protocol.Enums;

namespace Giny.Protocol.Messages
{ 
    public class StatsUpgradeRequestMessage : NetworkMessage  
    { 
        public  const ushort Id = 7822;
        public override ushort MessageId => Id;

        public bool useAdditionnal;
        public byte statId;
        public short boostPoint;

        public StatsUpgradeRequestMessage()
        {
        }
        public StatsUpgradeRequestMessage(bool useAdditionnal,byte statId,short boostPoint)
        {
            this.useAdditionnal = useAdditionnal;
            this.statId = statId;
            this.boostPoint = boostPoint;
        }
        public override void Serialize(IDataWriter writer)
        {
            writer.WriteBoolean((bool)useAdditionnal);
            writer.WriteByte((byte)statId);
            if (boostPoint < 0)
            {
                throw new System.Exception("Forbidden value (" + boostPoint + ") on element boostPoint.");
            }

            writer.WriteVarShort((short)boostPoint);
        }
        public override void Deserialize(IDataReader reader)
        {
            useAdditionnal = (bool)reader.ReadBoolean();
            statId = (byte)reader.ReadByte();
            if (statId < 0)
            {
                throw new System.Exception("Forbidden value (" + statId + ") on element of StatsUpgradeRequestMessage.statId.");
            }

            boostPoint = (short)reader.ReadVarUhShort();
            if (boostPoint < 0)
            {
                throw new System.Exception("Forbidden value (" + boostPoint + ") on element of StatsUpgradeRequestMessage.boostPoint.");
            }

        }


    }
}








