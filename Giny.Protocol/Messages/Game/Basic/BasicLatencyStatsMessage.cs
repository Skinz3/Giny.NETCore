using System.Collections.Generic;
using Giny.Core.Network.Messages;
using Giny.Protocol.Types;
using Giny.Core.IO.Interfaces;
using Giny.Protocol;
using Giny.Protocol.Enums;

namespace Giny.Protocol.Messages
{ 
    public class BasicLatencyStatsMessage : NetworkMessage  
    { 
        public  const ushort Id = 7345;
        public override ushort MessageId => Id;

        public short latency;
        public short sampleCount;
        public short max;

        public BasicLatencyStatsMessage()
        {
        }
        public BasicLatencyStatsMessage(short latency,short sampleCount,short max)
        {
            this.latency = latency;
            this.sampleCount = sampleCount;
            this.max = max;
        }
        public override void Serialize(IDataWriter writer)
        {
            if (latency < 0 || latency > 65535)
            {
                throw new System.Exception("Forbidden value (" + latency + ") on element latency.");
            }

            writer.WriteShort((short)latency);
            if (sampleCount < 0)
            {
                throw new System.Exception("Forbidden value (" + sampleCount + ") on element sampleCount.");
            }

            writer.WriteVarShort((short)sampleCount);
            if (max < 0)
            {
                throw new System.Exception("Forbidden value (" + max + ") on element max.");
            }

            writer.WriteVarShort((short)max);
        }
        public override void Deserialize(IDataReader reader)
        {
            latency = (short)reader.ReadUShort();
            if (latency < 0 || latency > 65535)
            {
                throw new System.Exception("Forbidden value (" + latency + ") on element of BasicLatencyStatsMessage.latency.");
            }

            sampleCount = (short)reader.ReadVarUhShort();
            if (sampleCount < 0)
            {
                throw new System.Exception("Forbidden value (" + sampleCount + ") on element of BasicLatencyStatsMessage.sampleCount.");
            }

            max = (short)reader.ReadVarUhShort();
            if (max < 0)
            {
                throw new System.Exception("Forbidden value (" + max + ") on element of BasicLatencyStatsMessage.max.");
            }

        }


    }
}








