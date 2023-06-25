using System.Collections.Generic;
using Giny.Core.IO.Interfaces;
using Giny.Protocol;
using Giny.Protocol.Enums;

namespace Giny.Protocol.Types
{ 
    public class HumanOptionEmote : HumanOption  
    { 
        public new const ushort Id = 3948;
        public override ushort TypeId => Id;

        public short emoteId;
        public double emoteStartTime;

        public HumanOptionEmote()
        {
        }
        public HumanOptionEmote(short emoteId,double emoteStartTime)
        {
            this.emoteId = emoteId;
            this.emoteStartTime = emoteStartTime;
        }
        public override void Serialize(IDataWriter writer)
        {
            base.Serialize(writer);
            if (emoteId < 0 || emoteId > 65535)
            {
                throw new System.Exception("Forbidden value (" + emoteId + ") on element emoteId.");
            }

            writer.WriteShort((short)emoteId);
            if (emoteStartTime < -9.00719925474099E+15 || emoteStartTime > 9.00719925474099E+15)
            {
                throw new System.Exception("Forbidden value (" + emoteStartTime + ") on element emoteStartTime.");
            }

            writer.WriteDouble((double)emoteStartTime);
        }
        public override void Deserialize(IDataReader reader)
        {
            base.Deserialize(reader);
            emoteId = (short)reader.ReadUShort();
            if (emoteId < 0 || emoteId > 65535)
            {
                throw new System.Exception("Forbidden value (" + emoteId + ") on element of HumanOptionEmote.emoteId.");
            }

            emoteStartTime = (double)reader.ReadDouble();
            if (emoteStartTime < -9.00719925474099E+15 || emoteStartTime > 9.00719925474099E+15)
            {
                throw new System.Exception("Forbidden value (" + emoteStartTime + ") on element of HumanOptionEmote.emoteStartTime.");
            }

        }


    }
}








