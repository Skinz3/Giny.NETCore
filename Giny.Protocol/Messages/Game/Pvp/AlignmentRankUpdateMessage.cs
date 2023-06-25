using System.Collections.Generic;
using Giny.Core.Network.Messages;
using Giny.Protocol.Types;
using Giny.Core.IO.Interfaces;
using Giny.Protocol;
using Giny.Protocol.Enums;

namespace Giny.Protocol.Messages
{ 
    public class AlignmentRankUpdateMessage : NetworkMessage  
    { 
        public  const ushort Id = 953;
        public override ushort MessageId => Id;

        public byte alignmentRank;
        public bool verbose;

        public AlignmentRankUpdateMessage()
        {
        }
        public AlignmentRankUpdateMessage(byte alignmentRank,bool verbose)
        {
            this.alignmentRank = alignmentRank;
            this.verbose = verbose;
        }
        public override void Serialize(IDataWriter writer)
        {
            if (alignmentRank < 0)
            {
                throw new System.Exception("Forbidden value (" + alignmentRank + ") on element alignmentRank.");
            }

            writer.WriteByte((byte)alignmentRank);
            writer.WriteBoolean((bool)verbose);
        }
        public override void Deserialize(IDataReader reader)
        {
            alignmentRank = (byte)reader.ReadByte();
            if (alignmentRank < 0)
            {
                throw new System.Exception("Forbidden value (" + alignmentRank + ") on element of AlignmentRankUpdateMessage.alignmentRank.");
            }

            verbose = (bool)reader.ReadBoolean();
        }


    }
}








