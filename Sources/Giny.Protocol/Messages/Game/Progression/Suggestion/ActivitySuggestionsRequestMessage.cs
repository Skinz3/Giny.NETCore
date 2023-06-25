using System.Collections.Generic;
using Giny.Core.Network.Messages;
using Giny.Protocol.Types;
using Giny.Core.IO.Interfaces;
using Giny.Protocol;
using Giny.Protocol.Enums;

namespace Giny.Protocol.Messages
{ 
    public class ActivitySuggestionsRequestMessage : NetworkMessage  
    { 
        public  const ushort Id = 3475;
        public override ushort MessageId => Id;

        public short minLevel;
        public short maxLevel;
        public short areaId;
        public short activityCategoryId;
        public short nbCards;

        public ActivitySuggestionsRequestMessage()
        {
        }
        public ActivitySuggestionsRequestMessage(short minLevel,short maxLevel,short areaId,short activityCategoryId,short nbCards)
        {
            this.minLevel = minLevel;
            this.maxLevel = maxLevel;
            this.areaId = areaId;
            this.activityCategoryId = activityCategoryId;
            this.nbCards = nbCards;
        }
        public override void Serialize(IDataWriter writer)
        {
            if (minLevel < 0)
            {
                throw new System.Exception("Forbidden value (" + minLevel + ") on element minLevel.");
            }

            writer.WriteVarShort((short)minLevel);
            if (maxLevel < 0)
            {
                throw new System.Exception("Forbidden value (" + maxLevel + ") on element maxLevel.");
            }

            writer.WriteVarShort((short)maxLevel);
            if (areaId < 0)
            {
                throw new System.Exception("Forbidden value (" + areaId + ") on element areaId.");
            }

            writer.WriteVarShort((short)areaId);
            if (activityCategoryId < 0)
            {
                throw new System.Exception("Forbidden value (" + activityCategoryId + ") on element activityCategoryId.");
            }

            writer.WriteVarShort((short)activityCategoryId);
            if (nbCards < 0 || nbCards > 65535)
            {
                throw new System.Exception("Forbidden value (" + nbCards + ") on element nbCards.");
            }

            writer.WriteShort((short)nbCards);
        }
        public override void Deserialize(IDataReader reader)
        {
            minLevel = (short)reader.ReadVarUhShort();
            if (minLevel < 0)
            {
                throw new System.Exception("Forbidden value (" + minLevel + ") on element of ActivitySuggestionsRequestMessage.minLevel.");
            }

            maxLevel = (short)reader.ReadVarUhShort();
            if (maxLevel < 0)
            {
                throw new System.Exception("Forbidden value (" + maxLevel + ") on element of ActivitySuggestionsRequestMessage.maxLevel.");
            }

            areaId = (short)reader.ReadVarUhShort();
            if (areaId < 0)
            {
                throw new System.Exception("Forbidden value (" + areaId + ") on element of ActivitySuggestionsRequestMessage.areaId.");
            }

            activityCategoryId = (short)reader.ReadVarUhShort();
            if (activityCategoryId < 0)
            {
                throw new System.Exception("Forbidden value (" + activityCategoryId + ") on element of ActivitySuggestionsRequestMessage.activityCategoryId.");
            }

            nbCards = (short)reader.ReadUShort();
            if (nbCards < 0 || nbCards > 65535)
            {
                throw new System.Exception("Forbidden value (" + nbCards + ") on element of ActivitySuggestionsRequestMessage.nbCards.");
            }

        }


    }
}








